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
        public static (Query Query, Sort? Sort) Build(ProductSearchRequest req)
        {
            var bq = new BooleanQuery();
            var hasAnyClause = false;

            // ── Free-text search ────────────────────────────────────────
            if (!string.IsNullOrWhiteSpace(req.Query))
            {
                // Tokenize the user input on whitespace. For each token we add
                // a SHOULD clause across the catch-all field. Multiple tokens
                // boost relevance for products matching more of them.
                var tokens = req.Query
                    .ToLowerInvariant()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries
                              | StringSplitOptions.TrimEntries);

                if (tokens.Length > 0)
                {
                    var textQuery = new BooleanQuery();
                    foreach (var token in tokens)
                    {
                        // TermQuery = exact term match. Cheap and fast.
                        textQuery.Add(
                            new TermQuery(new Term(ProductIndexFields.CatchAll, token)),
                            Occur.SHOULD);
                    }
                    // The text query as a whole MUST match (at least one token).
                    textQuery.MinimumNumberShouldMatch = 1;
                    bq.Add(textQuery, Occur.MUST);
                    hasAnyClause = true;
                }
            }

            // ── Category filters ────────────────────────────────────────
            if (req.CategoryId is int catId)
                AddIntFilter(bq, ProductIndexFields.CategoryId, catId, ref hasAnyClause);

            if (req.SubCategoryId is int subId)
                AddIntFilter(bq, ProductIndexFields.SubCategoryId, subId, ref hasAnyClause);

            if (req.SubCategoryCategoryId is int sscId)
                AddIntFilter(bq, ProductIndexFields.SubCategoryCategoryId, sscId, ref hasAnyClause);

            if (req.CommodityClassId is int ccId)
                AddIntFilter(bq, ProductIndexFields.CommodityClassId, ccId, ref hasAnyClause);

            // ── Condition (exact text) ──────────────────────────────────
            if (!string.IsNullOrWhiteSpace(req.Condition))
            {
                bq.Add(new TermQuery(new Term(ProductIndexFields.Condition, req.Condition)),
                    Occur.MUST);
                hasAnyClause = true;
            }

            // ── Price range ─────────────────────────────────────────────
            if (req.MinPrice is not null || req.MaxPrice is not null)
            {
                bq.Add(
                    NumericRangeQuery.NewInt64Range(
                        field: ProductIndexFields.PriceMinor,
                        min: req.MinPrice,
                        max: req.MaxPrice,
                        minInclusive: true,
                        maxInclusive: true),
                    Occur.MUST);
                hasAnyClause = true;
            }

            // ── Seller filter ───────────────────────────────────────────
            if (req.SellerProfileId is Guid sellerId)
            {
                bq.Add(new TermQuery(new Term(
                        ProductIndexFields.SellerProfileId, sellerId.ToString("N"))),
                    Occur.MUST);
                hasAnyClause = true;
            }

            // ── Boolean flag filters ────────────────────────────────────
            if (req.IsFeatured == true)
                AddBoolFilter(bq, ProductIndexFields.IsFeatured, true, ref hasAnyClause);

            if (req.VerifiedSellerOnly == true)
                AddBoolFilter(bq, ProductIndexFields.IsVerified, true, ref hasAnyClause);

            if (req.WithImageOnly == true)
                AddBoolFilter(bq, ProductIndexFields.HasImage, true, ref hasAnyClause);

            // If no clauses were added (empty request), match everything.
            // This is what powers "browse all products" via the search endpoint.
            Query finalQuery = hasAnyClause ? bq : new MatchAllDocsQuery();

            // ── Sort ────────────────────────────────────────────────────
            Sort? sort = req.Sort switch
            {
                ProductSortMode.Relevance => null, // null = sort by score
                ProductSortMode.NewestFirst =>
                    new Sort(new SortField(ProductIndexFields.CreatedAtTicks, SortFieldType.INT64, reverse: true)),
                ProductSortMode.PriceLowToHigh =>
                    new Sort(new SortField(ProductIndexFields.PriceMinor, SortFieldType.INT64, reverse: false)),
                ProductSortMode.PriceHighToLow =>
                    new Sort(new SortField(ProductIndexFields.PriceMinor, SortFieldType.INT64, reverse: true)),
                ProductSortMode.HighestRated =>
                    new Sort(
                        new SortField(ProductIndexFields.AverageRating, SortFieldType.SINGLE, reverse: true),
                        new SortField(ProductIndexFields.ReviewCount, SortFieldType.INT32, reverse: true)),
                _ => null,
            };

            return (finalQuery, sort);
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
