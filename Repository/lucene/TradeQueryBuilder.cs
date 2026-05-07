using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.lucene
{
    internal static class TradeQueryBuilder
    {
        // Field-level relevance boosts.
        private const float BoostTradeName = 5.0f;
        private const float BoostCategory = 3.0f;
        private const float BoostSubCategory = 2.5f;
        private const float BoostSeller = 2.0f;
        private const float BoostDescription = 1.0f;

        // Prefix matches score lower than exact-stem matches so "weld" still
        // outranks a partial-typing match against the same document.
        private const float BoostPrefix = 0.3f;

        public static (Query Query, Sort? Sort) Build(
            TradeSearchRequest req,
            Analyzer analyzer)
        {
            var bq = new BooleanQuery();
            var hasAnyClause = false;

            if (!string.IsNullOrWhiteSpace(req.Query))
            {
                var textQuery = BuildTextQuery(req.Query, analyzer);
                if (textQuery is not null)
                {
                    bq.Add(textQuery, Occur.MUST);
                    hasAnyClause = true;
                }
            }

            if (req.CategoryId is int catId)
                AddIntFilter(bq, TradeIndexFields.CategoryId, catId, ref hasAnyClause);
            if (req.SubCategoryId is int subId)
                AddIntFilter(bq, TradeIndexFields.SubCategoryId, subId, ref hasAnyClause);
            if (req.SubCategoryCategoryId is int sscId)
                AddIntFilter(bq, TradeIndexFields.SubCategoryCategoryId, sscId, ref hasAnyClause);
            if (req.CommodityClassId is int ccId)
                AddIntFilter(bq, TradeIndexFields.CommodityClassId, ccId, ref hasAnyClause);

            if (req.SellerProfileId is Guid sellerId)
            {
                bq.Add(new TermQuery(new Term(
                        TradeIndexFields.SellerProfileId, sellerId.ToString("N"))),
                    Occur.MUST);
                hasAnyClause = true;
            }

            if (req.IsFeatured == true)
                AddBoolFilter(bq, TradeIndexFields.IsFeatured, true, ref hasAnyClause);
            if (req.VerifiedSellerOnly == true)
                AddBoolFilter(bq, TradeIndexFields.IsVerified, true, ref hasAnyClause);
            if (req.WithImageOnly == true)
                AddBoolFilter(bq, TradeIndexFields.HasImage, true, ref hasAnyClause);

            Query finalQuery = hasAnyClause ? bq : new MatchAllDocsQuery();

            Sort? sort = req.Sort switch
            {
                TradeSortMode.Relevance => null,
                TradeSortMode.NewestFirst =>
                    new Sort(new SortField(TradeIndexFields.CreatedAtTicks, SortFieldType.INT64, true)),
                TradeSortMode.HighestRated =>
                    new Sort(
                        new SortField(TradeIndexFields.AverageRating, SortFieldType.SINGLE, true),
                        new SortField(TradeIndexFields.ReviewCount, SortFieldType.INT32, true)),
                _ => null,
            };

            return (finalQuery, sort);
        }

        /// <summary>
        /// Builds the free-text portion of the query.
        ///
        /// For each user-typed token we emit two layers of matching:
        ///   1. STEM match against analyzed fields — handles "shoes ↔ shoe",
        ///      "running ↔ run", at full-word level.
        ///   2. PREFIX match against the unstemmed TradeName_prefix field —
        ///      handles partial typing like "weldi" → "welding" while the
        ///      user is still mid-keystroke.
        ///
        /// The two layers cover stem variation AND incremental typing without
        /// either eating the other's accuracy.
        /// </summary>
        private static Query? BuildTextQuery(string userInput, Analyzer analyzer)
        {
            // Stemmed tokens — used for normal field matches.
            var stemmedTokens = AnalyzeToTokens(analyzer, TradeIndexFields.TradeName, userInput);
            if (stemmedTokens.Count == 0) return null;

            // Raw tokens — used for prefix matches.
            // Lowercased and split on whitespace, but NOT stemmed.
            var rawTokens = userInput
                .ToLowerInvariant()
                .Split(new[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(t => t.Length > 0)
                .ToList();

            var outer = new BooleanQuery();

            // Pair stemmed tokens with raw tokens by position. They typically
            // align; when stop words drop out during stemming they may not, in
            // which case the prefix branch for that position is simply skipped.
            for (var i = 0; i < stemmedTokens.Count; i++)
            {
                var stemmed = stemmedTokens[i];
                var raw = i < rawTokens.Count ? rawTokens[i] : null;

                var perToken = new BooleanQuery();

                // Stem-based exact-match across all searchable fields.
                AddBoosted(perToken, TradeIndexFields.TradeName, stemmed, BoostTradeName);
                AddBoosted(perToken, TradeIndexFields.CategoryName, stemmed, BoostCategory);
                AddBoosted(perToken, TradeIndexFields.SubCategoryName, stemmed, BoostSubCategory);
                AddBoosted(perToken, TradeIndexFields.SubCategoryCategoryName, stemmed, BoostSubCategory);
                AddBoosted(perToken, TradeIndexFields.SellerName, stemmed, BoostSeller);
                AddBoosted(perToken, TradeIndexFields.Description, stemmed, BoostDescription);

                // Prefix on the unstemmed name field — only meaningful when we
                // have a raw token (we should always; the guard is defensive).
                if (!string.IsNullOrEmpty(raw))
                {
                    var prefix = new PrefixQuery(new Term(TradeIndexFields.TradeNamePrefix, raw))
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