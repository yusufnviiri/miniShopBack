using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    internal static class ProductQueryBuilder
    {
        // Field weights — tuned for product search.
        // Hits in the product name matter ~5x more than hits in description.
        // Tweak as you observe real query patterns.
        private const float BoostProductName = 5.0f;
        private const float BoostCategory = 3.0f;
        private const float BoostSubCategory = 2.5f;
        private const float BoostSeller = 2.0f;
        private const float BoostDescription = 1.0f;

        // Prefix-match boost — lower than exact match, so "watch" still beats "watching".
        private const float BoostPrefix = 0.3f;

        public static (Query Query, Sort? Sort) Build(
            ProductSearchRequest req,
            Analyzer analyzer)
        {
            var bq = new BooleanQuery();
            var hasAnyClause = false;

            // ── Free-text search ────────────────────────────────────────
            if (!string.IsNullOrWhiteSpace(req.Query))
            {
                var textQuery = BuildTextQuery(req.Query, analyzer);
                if (textQuery is not null)
                {
                    bq.Add(textQuery, Occur.MUST);
                    hasAnyClause = true;
                }
            }

            // ── Filters (unchanged from before) ─────────────────────────
            if (req.CategoryId is int catId)
                AddIntFilter(bq, ProductIndexFields.CategoryId, catId, ref hasAnyClause);
            if (req.SubCategoryId is int subId)
                AddIntFilter(bq, ProductIndexFields.SubCategoryId, subId, ref hasAnyClause);
            if (req.SubCategoryCategoryId is int sscId)
                AddIntFilter(bq, ProductIndexFields.SubCategoryCategoryId, sscId, ref hasAnyClause);
            if (req.CommodityClassId is int ccId)
                AddIntFilter(bq, ProductIndexFields.CommodityClassId, ccId, ref hasAnyClause);

            if (!string.IsNullOrWhiteSpace(req.Condition))
            {
                bq.Add(new TermQuery(new Term(ProductIndexFields.Condition, req.Condition)),
                    Occur.MUST);
                hasAnyClause = true;
            }

            if (req.MinPrice is not null || req.MaxPrice is not null)
            {
                bq.Add(NumericRangeQuery.NewInt64Range(
                    ProductIndexFields.PriceMinor, req.MinPrice, req.MaxPrice, true, true),
                    Occur.MUST);
                hasAnyClause = true;
            }

            if (req.SellerProfileId is Guid sellerId)
            {
                bq.Add(new TermQuery(new Term(
                        ProductIndexFields.SellerProfileId, sellerId.ToString("N"))),
                    Occur.MUST);
                hasAnyClause = true;
            }

            if (req.IsFeatured == true)
                AddBoolFilter(bq, ProductIndexFields.IsFeatured, true, ref hasAnyClause);
            if (req.VerifiedSellerOnly == true)
                AddBoolFilter(bq, ProductIndexFields.IsVerified, true, ref hasAnyClause);
            if (req.WithImageOnly == true)
                AddBoolFilter(bq, ProductIndexFields.HasImage, true, ref hasAnyClause);

            Query finalQuery = hasAnyClause ? bq : new MatchAllDocsQuery();

            // ── Sort (unchanged) ────────────────────────────────────────
            Sort? sort = req.Sort switch
            {
                ProductSortMode.Relevance => null,
                ProductSortMode.NewestFirst =>
                    new Sort(new SortField(ProductIndexFields.CreatedAtTicks, SortFieldType.INT64, true)),
                ProductSortMode.PriceLowToHigh =>
                    new Sort(new SortField(ProductIndexFields.PriceMinor, SortFieldType.INT64, false)),
                ProductSortMode.PriceHighToLow =>
                    new Sort(new SortField(ProductIndexFields.PriceMinor, SortFieldType.INT64, true)),
                ProductSortMode.HighestRated =>
                    new Sort(
                        new SortField(ProductIndexFields.AverageRating, SortFieldType.SINGLE, true),
                        new SortField(ProductIndexFields.ReviewCount, SortFieldType.INT32, true)),
                _ => null,
            };

            return (finalQuery, sort);
        }

        /// <summary>
        /// Builds the free-text portion of the query.
        /// For each user-typed token, builds a SHOULD clause that matches the token
        /// against multiple fields with different boosts, plus a low-boost prefix
        /// match for partial typing.
        /// </summary>
        private static Query? BuildTextQuery(string userInput, Analyzer analyzer)
        {
            var stemmedTokens = AnalyzeToTokens(analyzer, ProductIndexFields.ProductName, userInput);
            if (stemmedTokens.Count == 0) return null;

            var rawTokens = userInput
                .ToLowerInvariant()
                .Split(new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(t => t.Length > 0)
                .ToList();

            var outer = new BooleanQuery();

            for (var i = 0; i < stemmedTokens.Count; i++)
            {
                var stemmed = stemmedTokens[i];
                var raw = i < rawTokens.Count ? rawTokens[i] : null;

                var perToken = new BooleanQuery();

                AddBoosted(perToken, ProductIndexFields.ProductName, stemmed, BoostProductName);
                AddBoosted(perToken, ProductIndexFields.CategoryName, stemmed, BoostCategory);
                AddBoosted(perToken, ProductIndexFields.SubCategoryName, stemmed, BoostSubCategory);
                AddBoosted(perToken, ProductIndexFields.SubCategoryCategoryName, stemmed, BoostSubCategory);
                AddBoosted(perToken, ProductIndexFields.SellerName, stemmed, BoostSeller);
                AddBoosted(perToken, ProductIndexFields.Description, stemmed, BoostDescription);

                if (!string.IsNullOrEmpty(raw))
                {
                    var prefix = new PrefixQuery(new Term(ProductIndexFields.ProductNamePrefix, raw))
                    {
                        Boost = BoostPrefix
                    };
                    perToken.Add(prefix, Occur.SHOULD);
                }

                perToken.MinimumNumberShouldMatch = 1;
                outer.Add(perToken, Occur.MUST);
            }

            return outer;
        }
        private static void AddBoosted(BooleanQuery bq, string field, string token, float boost)
        {
            var tq = new TermQuery(new Term(field, token)) { Boost = boost };
            bq.Add(tq, Occur.SHOULD);
        }

        /// <summary>
        /// Pushes user input through the configured analyzer and collects the tokens.
        /// This applies the same stemming, lowercasing, and stop-word removal that
        /// happened at index time, so query tokens align with stored tokens.
        /// </summary>
        private static List<string> AnalyzeToTokens(Analyzer analyzer, string field, string text)
        {
            var tokens = new List<string>();

            using var reader = new System.IO.StringReader(text);
            using var stream = analyzer.GetTokenStream(field, reader);
            var termAttr = stream.AddAttribute<ICharTermAttribute>();

            stream.Reset();
            while (stream.IncrementToken())
            {
                var token = termAttr.ToString();
                if (!string.IsNullOrEmpty(token)) tokens.Add(token);
            }
            stream.End();

            return tokens;
        }

        private static void AddIntFilter(BooleanQuery bq, string field, int value, ref bool hasAny)
        {
            bq.Add(NumericRangeQuery.NewInt32Range(field, value, value, true, true), Occur.MUST);
            hasAny = true;
        }

        private static void AddBoolFilter(BooleanQuery bq, string field, bool value, ref bool hasAny)
        {
            bq.Add(new TermQuery(new Term(field, value ? "1" : "0")), Occur.MUST);
            hasAny = true;
        }
    }
}