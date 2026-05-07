using Contracts.Lucene;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Microsoft.Extensions.Options;
using Shared.Dtos;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repository.lucene
{
    public sealed class TradeSearchRepository : ITradeSearchRepository
    {
        private readonly LuceneIndexContext _ctx;
        private readonly LuceneOptions _options;

        public TradeSearchRepository(
            ILuceneIndexRegistry registry,
            IOptions<LuceneOptions> options)
        {
            _ctx = registry.Get(SearchIndexNames.Trades);
            _options = options.Value;
        }

        public void AddOrUpdate(TradeIndexDocument d)
        {
            var doc = BuildDocument(d);
            _ctx.Writer.UpdateDocument(
                new Term(TradeIndexFields.TradeId, d.TradeId.ToString("N")),
                doc);
        }

        public void Delete(Guid tradeId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(TradeIndexFields.TradeId, tradeId.ToString("N")));
        }

        public void DeleteBySeller(Guid sellerProfileId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(TradeIndexFields.SellerProfileId, sellerProfileId.ToString("N")));
        }

        public void Commit() => _ctx.Writer.Commit();

        public TradeSearchResult Search(TradeSearchRequest request, int maxPageSize)
        {
            var sw = Stopwatch.StartNew();

            var page = request.Page < 1 ? 1 : request.Page;
            var size = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, maxPageSize);

            var (query, sort) = TradeQueryBuilder.Build(request, _ctx.Analyzer);
            var hitsNeeded = page * size;

            var searcher = _ctx.SearcherManager.Acquire();
            try
            {
                TopDocs topDocs = sort is null
                    ? searcher.Search(query, hitsNeeded)
                    : searcher.Search(query, hitsNeeded, sort);

                var skip = (page - 1) * size;
                var pageHits = topDocs.ScoreDocs.Skip(skip).Take(size);

                var items = new List<TradeCardDto>(size);
                foreach (var hit in pageHits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    items.Add(TradeCardProjector.Project(doc, hit.Score));
                }

                sw.Stop();
                return new TradeSearchResult
                {
                    Items = items,
                    TotalHits = topDocs.TotalHits,
                    Page = page,
                    PageSize = size,
                    ElapsedMs = sw.ElapsedMilliseconds,
                };
            }
            finally
            {
                _ctx.SearcherManager.Release(searcher);
            }
        }

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

        private static Document BuildDocument(TradeIndexDocument d)
        {
            var doc = new Document();

            // Identity
            doc.Add(new StringField(TradeIndexFields.TradeId,
                d.TradeId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.Slug, d.Slug, Field.Store.YES));
            foreach (var token in TokenizeForPrefix(d.TradeName))
            {
                doc.Add(new StringField(TradeIndexFields.TradeNamePrefix, token, Field.Store.NO));
            }

            // Searchable text
            doc.Add(new TextField(TradeIndexFields.TradeName, d.TradeName, Field.Store.YES));
            doc.Add(new TextField(TradeIndexFields.Description,
                Truncate(d.Description, 1000), Field.Store.YES));
            doc.Add(new SortedDocValuesField(TradeIndexFields.TradeNameSort,
                new Lucene.Net.Util.BytesRef(d.TradeName.ToLowerInvariant())));

            // Flags
            doc.Add(new StringField(TradeIndexFields.IsFeatured,
                d.IsFeatured ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.HasImage,
                d.HasImage ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.IsModified,
                d.IsModified ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.IsVerified,
                d.IsVerified ? "1" : "0", Field.Store.YES));

            // Categories
            doc.Add(new Int32Field(TradeIndexFields.CategoryId, d.CategoryId, Field.Store.YES));
            doc.Add(new Int32Field(TradeIndexFields.SubCategoryId, d.SubCategoryId, Field.Store.YES));
            doc.Add(new Int32Field(TradeIndexFields.SubCategoryCategoryId,
                d.SubCategoryCategoryId, Field.Store.YES));
            doc.Add(new Int32Field(TradeIndexFields.CommodityClassId,
                d.CommodityClassId, Field.Store.YES));

            doc.Add(new TextField(TradeIndexFields.CategoryName, d.CategoryName, Field.Store.YES));
            doc.Add(new TextField(TradeIndexFields.SubCategoryName,
                d.SubCategoryName, Field.Store.YES));
            doc.Add(new TextField(TradeIndexFields.SubCategoryCategoryName,
                d.SubCategoryCategoryName, Field.Store.YES));

            // Time
            doc.Add(new Int64Field(TradeIndexFields.CreatedAtTicks,
                d.CreatedAtTicks, Field.Store.YES));
            doc.Add(new NumericDocValuesField(TradeIndexFields.CreatedAtTicks, d.CreatedAtTicks));

            // Image
            doc.Add(new StringField(TradeIndexFields.TradeImageId,
                d.TradeImageId.ToString("N"), Field.Store.YES));

            // Seller
            doc.Add(new StringField(TradeIndexFields.SellerProfileId,
                d.SellerProfileId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.SellerId,
                d.SellerId.ToString("N"), Field.Store.YES));
            doc.Add(new TextField(TradeIndexFields.SellerName, d.SellerName, Field.Store.YES));
            doc.Add(new StringField(TradeIndexFields.SellerSlugName,
                d.SellerSlugName, Field.Store.YES));
            doc.Add(new StoredField(TradeIndexFields.WhatsAppNumber, d.WhatsAppNumber));
            doc.Add(new Int32Field(TradeIndexFields.SellerTypeId, d.SellerTypeId, Field.Store.YES));
            doc.Add(new Int32Field(TradeIndexFields.SellerTierId, d.SellerTierId, Field.Store.YES));

            // Aggregates
            doc.Add(new Int32Field(TradeIndexFields.ReviewCount, d.ReviewCount, Field.Store.YES));
            doc.Add(new NumericDocValuesField(TradeIndexFields.ReviewCount, d.ReviewCount));
            doc.Add(new SingleField(TradeIndexFields.AverageRating, d.AverageRating, Field.Store.YES));

            return doc;
        }

        private static string Truncate(string s, int max) =>
            string.IsNullOrEmpty(s) || s.Length <= max ? s : s.Substring(0, max);
    }
}
