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
    public sealed class UserSearchRepository : IUserSearchRepository
    {
        private readonly LuceneIndexContext _ctx;
        private readonly LuceneOptions _options;

        public UserSearchRepository(
            ILuceneIndexRegistry registry,
            IOptions<LuceneOptions> options)
        {
            _ctx = registry.Get(SearchIndexNames.Users);
            _options = options.Value;
        }

        public void AddOrUpdate(UserIndexDocument d)
        {
            var doc = BuildDocument(d);
            _ctx.Writer.UpdateDocument(
                new Term(UserIndexFields.UserProfileId, d.UserProfileId.ToString("N")),
                doc);
        }

        public void Delete(Guid userProfileId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(UserIndexFields.UserProfileId, userProfileId.ToString("N")));
        }

        public void Commit() => _ctx.Writer.Commit();

        public UserSearchResult Search(UserSearchRequest request, int maxPageSize)
        {
            var sw = Stopwatch.StartNew();

            var page = request.Page < 1 ? 1 : request.Page;
            var size = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, maxPageSize);

            var (query, sort) = UserQueryBuilder.Build(request, _ctx.Analyzer);
            var hitsNeeded = page * size;

            var searcher = _ctx.SearcherManager.Acquire();
            try
            {
                TopDocs topDocs = sort is null
                    ? searcher.Search(query, hitsNeeded)
                    : searcher.Search(query, hitsNeeded, sort);

                var skip = (page - 1) * size;
                var pageHits = topDocs.ScoreDocs.Skip(skip).Take(size);

                var items = new List<UserCardDto>(size);
                foreach (var hit in pageHits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    items.Add(UserCardProjector.Project(doc, hit.Score));
                }

                sw.Stop();
                return new UserSearchResult
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

        private static Document BuildDocument(UserIndexDocument d)
        {
            var doc = new Document();

            // ── Identity ──
            doc.Add(new StringField(UserIndexFields.UserProfileId,
                d.UserProfileId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(UserIndexFields.SlugName, d.SlugName, Field.Store.YES));

            // ── Searchable text ──
            doc.Add(new TextField(UserIndexFields.DisplayName, d.DisplayName, Field.Store.YES));
            doc.Add(new TextField(UserIndexFields.Company, d.Company, Field.Store.YES));
            doc.Add(new TextField(UserIndexFields.UserGroupNames,
                d.UserGroupNames, Field.Store.NO));

            // Unstemmed prefix field for partial-typing matches
            foreach (var token in TokenizeForPrefix(d.DisplayName))
            {
                doc.Add(new StringField(UserIndexFields.DisplayNamePrefix,
                    token, Field.Store.NO));
            }

            // ── Filters (exact match) ──
            doc.Add(new StringField(UserIndexFields.IsSeller,
                d.IsSeller ? "1" : "0", Field.Store.YES));
            doc.Add(new StringField(UserIndexFields.City,
                (d.City ?? "").ToLowerInvariant(), Field.Store.YES));
            doc.Add(new StringField(UserIndexFields.Region,
                (d.Region ?? "").ToLowerInvariant(), Field.Store.YES));
            doc.Add(new StringField(UserIndexFields.Country,
                (d.Country ?? "").ToLowerInvariant(), Field.Store.YES));
            doc.Add(new Int32Field(UserIndexFields.UserStatusId,
                d.UserStatusId, Field.Store.YES));

            // ── Display extras ──
            if (d.SellerProfileId.HasValue)
            {
                doc.Add(new StringField(UserIndexFields.SellerProfileId,
                    d.SellerProfileId.Value.ToString("N"), Field.Store.YES));
            }
            doc.Add(new StoredField(UserIndexFields.SellerSlugName, d.SellerSlugName ?? ""));

            doc.Add(new Int32Field(UserIndexFields.UserGroupCount,
                d.UserGroupCount, Field.Store.YES));
            doc.Add(new Int32Field(UserIndexFields.ProductCount,
                d.ProductCount, Field.Store.YES));
            doc.Add(new NumericDocValuesField(UserIndexFields.ProductCount, d.ProductCount));
            doc.Add(new Int32Field(UserIndexFields.TradeCount,
                d.TradeCount, Field.Store.YES));
            doc.Add(new NumericDocValuesField(UserIndexFields.TradeCount, d.TradeCount));

            // ── Sort ──
            doc.Add(new Int64Field(UserIndexFields.CreatedAtTicks,
                d.CreatedAtTicks, Field.Store.YES));
            doc.Add(new NumericDocValuesField(UserIndexFields.CreatedAtTicks, d.CreatedAtTicks));

            return doc;
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
    }
}
