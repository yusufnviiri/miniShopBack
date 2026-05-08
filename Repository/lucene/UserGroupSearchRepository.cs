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
    public sealed class UserGroupSearchRepository : IUserGroupSearchRepository
    {
        private readonly LuceneIndexContext _ctx;
        private readonly LuceneOptions _options;

        public UserGroupSearchRepository(
            ILuceneIndexRegistry registry,
            IOptions<LuceneOptions> options)
        {
            _ctx = registry.Get(SearchIndexNames.UserGroups);
            _options = options.Value;
        }

        public void AddOrUpdate(UserGroupIndexDocument d)
        {
            var doc = BuildDocument(d);
            _ctx.Writer.UpdateDocument(
                new Term(UserGroupIndexFields.UserGroupId, d.UserGroupId.ToString("N")),
                doc);
        }

        public void Delete(Guid userGroupId)
        {
            _ctx.Writer.DeleteDocuments(
                new Term(UserGroupIndexFields.UserGroupId, userGroupId.ToString("N")));
        }

        public void Commit() => _ctx.Writer.Commit();

        public UserGroupSearchResult Search(UserGroupSearchRequest request, int maxPageSize)
        {
            var sw = Stopwatch.StartNew();
            var page = request.Page < 1 ? 1 : request.Page;
            var size = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, maxPageSize);

            var (query, sort) = UserGroupQueryBuilder.Build(request, _ctx.Analyzer);
            var hitsNeeded = page * size;

            var searcher = _ctx.SearcherManager.Acquire();
            try
            {
                TopDocs topDocs = sort is null
                    ? searcher.Search(query, hitsNeeded)
                    : searcher.Search(query, hitsNeeded, sort);

                var skip = (page - 1) * size;
                var pageHits = topDocs.ScoreDocs.Skip(skip).Take(size);

                var items = new List<UserGroupCardDto>(size);
                foreach (var hit in pageHits)
                {
                    var doc = searcher.Doc(hit.Doc);
                    items.Add(UserGroupCardProjector.Project(doc, hit.Score));
                }

                sw.Stop();
                return new UserGroupSearchResult
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

        private static Document BuildDocument(UserGroupIndexDocument d)
        {
            var doc = new Document();

            // Identity
            doc.Add(new StringField(UserGroupIndexFields.UserGroupId,
                d.UserGroupId.ToString("N"), Field.Store.YES));
            doc.Add(new StringField(UserGroupIndexFields.SlugName, d.SlugName, Field.Store.YES));

            // Searchable text
            doc.Add(new TextField(UserGroupIndexFields.UserGroupName, d.UserGroupName, Field.Store.YES));
            doc.Add(new TextField(UserGroupIndexFields.AboutGroup, Truncate(d.AboutGroup, 1000), Field.Store.YES));
            doc.Add(new TextField(UserGroupIndexFields.GroupType, d.GroupType, Field.Store.YES));
            doc.Add(new TextField(UserGroupIndexFields.Company, d.Company, Field.Store.YES));

            // Unstemmed prefix on group name (same lesson as products/trades)
            foreach (var token in TokenizeForPrefix(d.UserGroupName))
            {
                doc.Add(new StringField(UserGroupIndexFields.UserGroupNamePrefix, token, Field.Store.NO));
            }

            // Filters
            doc.Add(new Int32Field(UserGroupIndexFields.GroupTypeId, d.GroupTypeId, Field.Store.YES));
            doc.Add(new StringField(UserGroupIndexFields.City, (d.City ?? "").ToLowerInvariant(), Field.Store.YES));
            doc.Add(new StringField(UserGroupIndexFields.Region, (d.Region ?? "").ToLowerInvariant(), Field.Store.YES));
            doc.Add(new StringField(UserGroupIndexFields.Country, (d.Country ?? "").ToLowerInvariant(), Field.Store.YES));

            // MemberCount — indexed for sort/range, stored for display
            doc.Add(new Int32Field(UserGroupIndexFields.MemberCount, d.MemberCount, Field.Store.YES));
            doc.Add(new NumericDocValuesField(UserGroupIndexFields.MemberCount, d.MemberCount));

            // Display-only
            doc.Add(new StoredField(UserGroupIndexFields.Email, d.Email ?? ""));
            doc.Add(new StoredField(UserGroupIndexFields.Contact, d.Contact ?? ""));

            // Sort
            doc.Add(new Int64Field(UserGroupIndexFields.CreatedAtTicks, d.CreatedAtTicks, Field.Store.YES));
            doc.Add(new NumericDocValuesField(UserGroupIndexFields.CreatedAtTicks, d.CreatedAtTicks));

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

        private static string Truncate(string s, int max) =>
            string.IsNullOrEmpty(s) || s.Length <= max ? s : s.Substring(0, max);
    }
}
