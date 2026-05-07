using Contracts.Lucene;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Microsoft.Extensions.Options;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.lucene
{
    public sealed class ProductSearchRepository : IProductSearchRepository
    {
        private readonly LuceneIndexContext _ctx;
        private readonly LuceneOptions _options;


        public ProductSearchRepository(
        ILuceneIndexRegistry registry,
        IOptions<LuceneOptions> options)
        {
            _ctx = registry.Get(SearchIndexNames.Products);
            _options = options.Value;
        }

        public void AddOrUpdate(ProductIndexDocument d)
        {
            var doc = BuildDocument(d);

            // UpdateDocument with a Term acts as upsert: deletes any existing
            // doc matching the term, then adds the new one. Atomic.
            _ctx.Writer.UpdateDocument(
                new Term(ProductIndexFields.ProductId, d.ProductId.ToString("N")),
                doc);
        }

        public void Delete(Guid productId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(ProductIndexFields.ProductId, productId.ToString("N")));
        }

        public void DeleteBySeller(Guid sellerProfileId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(ProductIndexFields.SellerProfileId, sellerProfileId.ToString("N")));
        }

        public void Commit() => _ctx.Writer.Commit();

        private static IEnumerable<string> TokenizeForPrefix(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) yield break;

            foreach (var raw in text.Split(
                new[] { ' ', '\t', '\n', '\r', '-', '_', ',', '.', '/', '\\' },
                StringSplitOptions.RemoveEmptyEntries))
            {
                var lower = raw.ToLowerInvariant();
                if (lower.Length > 0) yield return lower;
            }
        }

        private static Document BuildDocument(ProductIndexDocument d)
        {
            var doc = new Document();

            // ─── Identity (exact-match, stored) ───────────────────────────
            // StringField = indexed but NOT analyzed. Stored = readable on retrieval.
            // We use "N" GUID format (no dashes) for compactness.
            doc.Add(new StringField(ProductIndexFields.ProductId,
                d.ProductId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.ProductImageId,
                d.ProductImageId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.Slug, d.Slug, Field.Store.YES));

            // ─── Searchable text ──────────────────────────────────────────
            // TextField = indexed AND analyzed (tokenized, lowercased, stop-worded).
            doc.Add(new TextField(ProductIndexFields.ProductName, d.ProductName, Field.Store.YES));

            // New unstemmed prefix field — add this block right after
            foreach (var token in TokenizeForPrefix(d.ProductName))
            {
                doc.Add(new StringField(ProductIndexFields.ProductNamePrefix, token, Field.Store.NO));
            }
            doc.Add(new TextField(ProductIndexFields.Description,
                Truncate(d.Description, 1000), Field.Store.YES));

            // SortedDocValuesField gives us efficient sorting on this field.
            // We store a lowercased copy so "apple" sorts the same as "Apple".
            doc.Add(new SortedDocValuesField(ProductIndexFields.ProductNameSort,
                new Lucene.Net.Util.BytesRef(d.ProductName.ToLowerInvariant())));

            // ─── Exact-match filters ──────────────────────────────────────
            doc.Add(new StringField(ProductIndexFields.Condition,
                d.Condition, Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.IsFeatured,
                d.IsFeatured ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.HasImage,
                d.HasImage ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.IsVerified,
                d.IsVerified ? "1" : "0", Field.Store.YES));

            // ─── Numeric IDs (filterable, exact match) ────────────────────
            // Int32Field = indexed for range/exact. Store.YES so we can read it back.
            doc.Add(new Int32Field(ProductIndexFields.CategoryId,
                d.CategoryId, Field.Store.YES));
            doc.Add(new Int32Field(ProductIndexFields.SubCategoryId,
                d.SubCategoryId, Field.Store.YES));
            doc.Add(new Int32Field(ProductIndexFields.SubCategoryCategoryId,
                d.SubCategoryCategoryId, Field.Store.YES));
            doc.Add(new Int32Field(ProductIndexFields.CommodityClassId,
                d.CommodityClassId, Field.Store.YES));
            doc.Add(new Int32Field(ProductIndexFields.SellerTypeId,
                d.SellerTypeId, Field.Store.YES));
            doc.Add(new Int32Field(ProductIndexFields.SellerTierId,
                d.SellerTierId, Field.Store.YES));

            // ─── Category names (searchable + displayable) ────────────────
            doc.Add(new TextField(ProductIndexFields.CategoryName,
                d.CategoryName, Field.Store.YES));
            doc.Add(new TextField(ProductIndexFields.SubCategoryName,
                d.SubCategoryName, Field.Store.YES));
            doc.Add(new TextField(ProductIndexFields.SubCategoryCategoryName,
                d.SubCategoryCategoryName, Field.Store.YES));

            // ─── Price (range + sort) ─────────────────────────────────────
            // Int64Field for indexed range queries. Doc value for sorting.
            doc.Add(new Int64Field(ProductIndexFields.PriceMinor,
                d.PriceMinor, Field.Store.YES));
            doc.Add(new NumericDocValuesField(ProductIndexFields.PriceMinor,
                d.PriceMinor));

            // OldPrice: stored only — display-only on the card, no need to filter/sort.
            doc.Add(new StoredField(ProductIndexFields.OldPriceMinor, d.OldPriceMinor));

            // ─── Time (sort by newest) ────────────────────────────────────
            doc.Add(new Int64Field(ProductIndexFields.CreatedAtTicks,
                d.CreatedAtTicks, Field.Store.YES));
            doc.Add(new NumericDocValuesField(ProductIndexFields.CreatedAtTicks,
                d.CreatedAtTicks));

            // ─── Display-only fields ──────────────────────────────────────

            doc.Add(new StoredField(ProductIndexFields.WhatsAppNumber, d.WhatsAppNumber));

            // ─── Seller (searchable + filterable + displayable) ───────────
            doc.Add(new StringField(ProductIndexFields.SellerProfileId,
                d.SellerProfileId.ToString("N"), Field.Store.YES));

            doc.Add(new StringField(ProductIndexFields.SellerLocation,
             d.SellerLocation, Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.SellerId,
                d.SellerId.ToString("N"), Field.Store.YES));
            doc.Add(new TextField(ProductIndexFields.SellerName,
                d.SellerName, Field.Store.YES));
            doc.Add(new StringField(ProductIndexFields.SellerSlug,
                d.SellerSlug, Field.Store.YES));

            // ─── Aggregates ───────────────────────────────────────────────
            doc.Add(new Int32Field(ProductIndexFields.ReviewCount,
                d.ReviewCount, Field.Store.YES));
            doc.Add(new NumericDocValuesField(ProductIndexFields.ReviewCount,
                d.ReviewCount));
            doc.Add(new SingleField(ProductIndexFields.AverageRating,
                d.AverageRating, Field.Store.YES));

            // ─── Catch-all for free-text search ───────────────────────────
            // We concatenate the main searchable fields into one analyzed-only
            // field so a single query like "red running shoes nakimuli" can
            // match across name + description + seller + categories without us
            // building a 6-clause BooleanQuery on every search.
            //var catchAll = string.Join(" ",
            //    d.ProductName, d.Description, d.SellerName,
            //    d.CategoryName, d.SubCategoryName, d.SubCategoryCategoryName);
            //doc.Add(new TextField(ProductIndexFields.CatchAll, catchAll, Field.Store.NO));

            return doc;
        }

        private static string Truncate(string s, int max) =>
            string.IsNullOrEmpty(s) || s.Length <= max ? s : s.Substring(0, max);
        public ProductSearchResult Search(ProductSearchRequest request, int maxPageSize)
        {
            var sw = Stopwatch.StartNew();

            // ── Sanitize paging ────────────────────────────────────────
            var page = request.Page < 1 ? 1 : request.Page;
            var size = request.PageSize <= 0
                ? 20
                : Math.Min(request.PageSize, maxPageSize);

            // ── Build the query ────────────────────────────────────────
            var (query, sort) = ProductQueryBuilder.Build(request, _ctx.Analyzer);

            // We only need the hits for THIS page, but Lucene's TopDocs API
            // wants `numHits = page * size` (it returns the top N, then we skip).
            // For deep pagination this is wasteful — at page 1000 with size 20
            // you'd ask for 20,000 hits. In practice product search rarely goes
            // past page 20-30; if you need true deep pagination, switch to
            // SearchAfter (Lucene's cursor-style API). Skipping that for now.
            var hitsNeeded = page * size;

            // ── Acquire a searcher (must be released in finally) ──────
            var searcher = _ctx.SearcherManager.Acquire();
            try
            {
                TopDocs topDocs = sort is null
                    ? searcher.Search(query, hitsNeeded)
                    : searcher.Search(query, hitsNeeded, sort);

                var totalHits = topDocs.TotalHits;
                var skip = (page - 1) * size;
                var pageHits = topDocs.ScoreDocs
                    .Skip(skip)
                    .Take(size);

                var items = new List<ProductCardDto>(size);
                foreach (var hit in pageHits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    items.Add(ProductCardProjector.Project(doc, hit.Score));
                }

                sw.Stop();
                return new ProductSearchResult
                {
                    Items = items,
                    TotalHits = totalHits,
                    Page = page,
                    PageSize = size,
                    ElapsedMs = sw.ElapsedMilliseconds,
                };
            }
            finally
            {
                // CRITICAL: every Acquire() must be matched with a Release().
                // Failing to release leaks readers and breaks future refreshes.
                _ctx.SearcherManager.Release(searcher);
            }
        }
    }
}