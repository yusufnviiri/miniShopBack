using Contracts.Lucene;
using J2N.Text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lucene
{
    public class LuceneSearchService : ILuceneSearchService
    {
        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
        private readonly string _indexPath;

        public LuceneSearchService(IConfiguration config)
        {
            _indexPath = config["Lucene:IndexPath"]
                         ?? Path.Combine(AppContext.BaseDirectory, "lucene-index");
        }

        public ProductSearchResponse Search(ProductSearchRequest req)
        {
            using var directory = FSDirectory.Open(_indexPath);
            using var reader = DirectoryReader.Open(directory);
            var searcher = new IndexSearcher(reader);
            using var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var masterQuery = new BooleanQuery();

            // ── 1. Full-text query ────────────────────
            if (!string.IsNullOrWhiteSpace(req.Query))
            {
                var parser = new MultiFieldQueryParser(
                    AppLuceneVersion,
                    [
                        ProductSeachFields.ProductName,
                    ProductSeachFields.FullText,
                    ProductSeachFields.Description
                    ],
                    analyzer,
                    new Dictionary<string, float>
                    {
                        [ProductSeachFields.ProductName] = 3.0f,   // boost product name
                        [ProductSeachFields.FullText] = 1.5f,
                        [ProductSeachFields.Description] = 1.0f
                    });

                parser.DefaultOperator = QueryParserBase.OR_OPERATOR;
                parser.AllowLeadingWildcard = true;

                try
                {
                    var textQuery = parser.Parse(QueryParserBase.Escape(req.Query) + "*");
                    masterQuery.Add(textQuery, Occur.MUST);
                }
                catch (J2N.Text.ParseException)
                {
                    // Fallback to term query if parse fails
                    var fallback = new TermQuery(
                        new Term(ProductSeachFields.ProductName, req.Query.ToLower()));
                    masterQuery.Add(fallback, Occur.MUST);
                }
            }

            // ── 2. Always exclude deleted / inactive ──
            masterQuery.Add(
                new TermQuery(new Term(ProductSeachFields.IsDeleted, "0")), Occur.MUST);
            masterQuery.Add(
                new TermQuery(new Term(ProductSeachFields.IsActive, "1")), Occur.MUST);

            // ── 3. Category / SubCategory filters ─────
            if (!string.IsNullOrWhiteSpace(req.Category))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.CategoryName,
                        req.Category.ToLower())),
                    Occur.MUST);

            if (!string.IsNullOrWhiteSpace(req.SubCategory))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.SubCategoryName,
                        req.SubCategory.ToLower())),
                    Occur.MUST);

            // ── 4. Condition filter ───────────────────
            if (!string.IsNullOrWhiteSpace(req.Condition))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.Condition,
                        req.Condition.ToLower())),
                    Occur.MUST);

            // ── 5. Boolean flag filters ───────────────
            if (req.IsFeatured.HasValue)
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.IsFeatured,
                        req.IsFeatured.Value ? "1" : "0")),
                    Occur.MUST);

            if (req.HasImage.HasValue)
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.HasImage,
                        req.HasImage.Value ? "1" : "0")),
                    Occur.MUST);

            // ── 6. Price range filter ─────────────────
            if (req.MinPrice.HasValue || req.MaxPrice.HasValue)
            {
                double min = req.MinPrice.HasValue ? (double)req.MinPrice.Value : double.MinValue;
                double max = req.MaxPrice.HasValue ? (double)req.MaxPrice.Value : double.MaxValue;

                masterQuery.Add(
                    NumericRangeQuery.NewDoubleRange(
                        ProductSeachFields.Price, min, max, true, true),
                    Occur.MUST);
            }

            // Use MatchAllDocsQuery if no real query built
            Query finalQuery = masterQuery.Clauses.Count == 0
                ? new MatchAllDocsQuery()
                : masterQuery;

            // ── 7. Sort ───────────────────────────────
            Sort sort = req.SortBy switch
            {
                "price_asc" => new Sort(new SortField(ProductSeachFields.Price, SortFieldType.DOUBLE, false)),
                "price_desc" => new Sort(new SortField(ProductSeachFields.Price, SortFieldType.DOUBLE, true)),
                "newest" => new Sort(new SortField(ProductSeachFields.CreatedAt, SortFieldType.INT64, true)),
                _ => Sort.RELEVANCE
            };

            // ── 8. Execute + Paginate ─────────────────
            int skip = (req.Page - 1) * req.PageSize;
            int fetch = skip + req.PageSize;

            TopDocs hits = (sort == Sort.RELEVANCE)
                ? searcher.Search(finalQuery, fetch)
                : searcher.Search(finalQuery, fetch, sort);

            int totalHits = hits.TotalHits;
            int totalPages = (int)Math.Ceiling(totalHits / (double)req.PageSize);

            var results = hits.ScoreDocs
                .Skip(skip)
                .Take(req.PageSize)
                .Select(sd =>
                {
                    var doc = searcher.Doc(sd.Doc);
                    return new ProductSearchResult(
                        ProductId: Guid.Parse(doc.Get(ProductSeachFields.ProductId)),
                        ProductName: doc.Get(ProductSeachFields.ProductName),
                        CategoryName: doc.Get(ProductSeachFields.CategoryName),
                        SubCategoryName: doc.Get(ProductSeachFields.SubCategoryName),
                        Condition: doc.Get(ProductSeachFields.Condition),
                        Price: (decimal)(double.TryParse(
                                           doc.Get(ProductSeachFields.Price),
                                           out var pr) ? pr : 0),
                        OldPrice: (decimal)(double.TryParse(
                                           doc.Get(ProductSeachFields.OldPrice),
                                           out var op) ? op : 0),
                        Slug: doc.Get(ProductSeachFields.Slug),
                        IsFeatured: doc.Get(ProductSeachFields.IsFeatured) == "1",
                        HasImage: doc.Get(ProductSeachFields.HasImage) == "1",
                        Score: sd.Score
                    );
                })
                .ToList();

            return new ProductSearchResponse(results, totalHits, req.Page, req.PageSize, totalPages);
        }




        public HomePageCustomProductsDto SearchHomePage(string searchQuery)
        {
            using var directory = FSDirectory.Open(_indexPath);
            using var reader = DirectoryReader.Open(directory);
            var searcher = new IndexSearcher(reader);
            using var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var masterQuery = new BooleanQuery();

            // ── 1. Full-text query ────────────────────
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var parser = new MultiFieldQueryParser(
                    AppLuceneVersion,
                    [
                        ProductSeachFields.ProductName,
                    ProductSeachFields.FullText,
                    ProductSeachFields.Description
                    ],
                    analyzer,
                    new Dictionary<string, float>
                    {
                        [ProductSeachFields.ProductName] = 3.0f,   // boost product name
                        [ProductSeachFields.FullText] = 1.5f,
                        [ProductSeachFields.Description] = 1.0f
                    });

                parser.DefaultOperator = QueryParserBase.OR_OPERATOR;
                parser.AllowLeadingWildcard = true;

                try
                {
                    var textQuery = parser.Parse(QueryParserBase.Escape(searchQuery) + "*");
                    masterQuery.Add(textQuery, Occur.MUST);
                }
                catch (J2N.Text.ParseException)
                {
                    // Fallback to term query if parse fails
                    var fallback = new TermQuery(
                        new Term(ProductSeachFields.ProductName, searchQuery.ToLower()));
                    masterQuery.Add(fallback, Occur.MUST);
                }
            }

            // ── 2. Always exclude deleted / inactive ──
            masterQuery.Add(
                new TermQuery(new Term(ProductSeachFields.IsDeleted, "0")), Occur.MUST);
            masterQuery.Add(
                new TermQuery(new Term(ProductSeachFields.IsActive, "1")), Occur.MUST);

            // ── 3. Category / SubCategory filters ─────
            if (!string.IsNullOrWhiteSpace(searchQuery))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.CategoryName,
                        searchQuery.ToLower())),
                    Occur.MUST);

            if (!string.IsNullOrWhiteSpace(searchQuery))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.SubCategoryName,
                            searchQuery.ToLower())),
                    Occur.MUST);

            // ── 4. Condition filter ───────────────────
            if (!string.IsNullOrWhiteSpace(searchQuery))
                masterQuery.Add(
                    new TermQuery(new Term(ProductSeachFields.Condition,
                        searchQuery.ToLower())),
                    Occur.MUST);

           



            // Use MatchAllDocsQuery if no real query built
            Query finalQuery = masterQuery.Clauses.Count == 0
                ? new MatchAllDocsQuery()
                : masterQuery;

            int fetch = int.MaxValue;


            TopDocs hits = searcher.Search(finalQuery, fetch);

            

            var results = hits.ScoreDocs.Select(sd =>
                {
                    var doc = searcher.Doc(sd.Doc);
                    return new ProductSearchResult(
                        ProductId: Guid.Parse(doc.Get(ProductSeachFields.ProductId)),
                        ProductName: doc.Get(ProductSeachFields.ProductName),
                        CategoryName: doc.Get(ProductSeachFields.CategoryName),
                        SubCategoryName: doc.Get(ProductSeachFields.SubCategoryName),
                        Condition: doc.Get(ProductSeachFields.Condition),
                        Price: (decimal)(double.TryParse(
                                           doc.Get(ProductSeachFields.Price),
                                           out var pr) ? pr : 0),
                        OldPrice: (decimal)(double.TryParse(
                                           doc.Get(ProductSeachFields.OldPrice),
                                           out var op) ? op : 0),
                        Slug: doc.Get(ProductSeachFields.Slug),
                        IsFeatured: doc.Get(ProductSeachFields.IsFeatured) == "1",
                        HasImage: doc.Get(ProductSeachFields.HasImage) == "1",
                        Score: sd.Score
                    );
                })
                .ToList();

            return new ProductSearchResponse(results, totalHits, req.Page, req.PageSize, totalPages);
        }











    }
}
