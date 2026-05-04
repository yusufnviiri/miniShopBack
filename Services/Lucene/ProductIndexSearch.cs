using Contracts.Lucene;
using Entities.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Shared.Lucene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenMode = Lucene.Net.Index.OpenMode;

namespace Services.Lucene
{
   public class ProductIndexSearch : IProductIndexSearch, IDisposable
    {
        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
        private readonly FSDirectory _directory;
        private readonly Analyzer _analyzer;
        private readonly string _indexPath;

        public ProductIndexSearch(IConfiguration config)
        {
            _indexPath = config["Lucene:IndexPath"]
                         ?? Path.Combine(AppContext.BaseDirectory, "lucene-index");

            System.IO.Directory.CreateDirectory(_indexPath);
            _directory = FSDirectory.Open(_indexPath);
            _analyzer = new StandardAnalyzer(AppLuceneVersion);
        }

        // ──────────────────────────────────────────────
        //  WRITE helpers
        // ──────────────────────────────────────────────

        public void IndexProduct(Product product)
        {
            using var writer = CreateWriter();
            writer.AddDocument(BuildDocument(product));
            writer.Commit();
        }

        public void IndexProducts(IEnumerable<Product> products)
        {
            using var writer = CreateWriter();
            foreach (var p in products)
                writer.AddDocument(BuildDocument(p));
            writer.Commit();
        }

        public void UpdateProduct(Product product)
        {
            using var writer = CreateWriter();
            var term = new Term(ProductSeachFields.ProductId, product.ProductId.ToString());
            writer.UpdateDocument(term, BuildDocument(product));
            writer.Commit();
        }

        public void DeleteProduct(Guid productId)
        {
            using var writer = CreateWriter();
            writer.DeleteDocuments(new Term(ProductSeachFields.ProductId, productId.ToString()));
            writer.Commit();
        }

        public void RebuildIndex(IEnumerable<Product> products)
        {
            var config = new IndexWriterConfig(AppLuceneVersion, _analyzer)
            {
                OpenMode = OpenMode.CREATE   // wipes existing index
            };

            using var writer = new IndexWriter(_directory, config);
            foreach (var p in products)
                writer.AddDocument(BuildDocument(p));

            writer.Commit();
        }

        // ──────────────────────────────────────────────
        //  Document builder — maps Product → Lucene doc
        // ──────────────────────────────────────────────

        private static Document BuildDocument(Product p)
        {
            var doc = new Document();

            // Stored + indexed identifiers
            doc.Add(new StringField(ProductSeachFields.ProductId,
                p.ProductId.ToString(), Field.Store.YES));

            doc.Add(new StringField(ProductSeachFields.Slug,
                p.Slug, Field.Store.YES));

            doc.Add(new StringField(ProductSeachFields.SellerProfileId,
                p.SellerProfileId.ToString(), Field.Store.YES));

            // Full-text searchable fields (TextField = tokenised)
            doc.Add(new TextField(ProductSeachFields.ProductName,
                p.ProductName, Field.Store.YES));

            doc.Add(new TextField(ProductSeachFields.Description,
                p.Description ?? string.Empty, Field.Store.NO));

            doc.Add(new TextField(ProductSeachFields.CategoryName,
                p.Category?.CategoryName ?? string.Empty, Field.Store.YES));

            doc.Add(new TextField(ProductSeachFields.SubCategoryName,
                p.SubCategory?.SubCategoryName ?? string.Empty, Field.Store.YES));

            doc.Add(new TextField(ProductSeachFields.Condition,
                p.Condition ?? string.Empty, Field.Store.YES));

            // Composite full-text field (boosted search target)
            doc.Add(new TextField(ProductSeachFields.FullText,
                $"{p.ProductName} {p.Description} " +
                $"{p.Category?.CategoryName} {p.SubCategory?.SubCategoryName} " +
                $"{p.Condition}",
                Field.Store.NO));

            // Numeric fields — stored as Int32 / Double for range queries
            doc.Add(new DoubleDocValuesField(ProductSeachFields.Price, (double)p.Price));
            doc.Add(new StoredField(ProductSeachFields.Price, (double)p.Price));

            doc.Add(new DoubleDocValuesField(ProductSeachFields.OldPrice, (double)p.OldPrice));
            doc.Add(new StoredField(ProductSeachFields.OldPrice, (double)p.OldPrice));

            // Boolean flags — stored as "1" / "0" for filter queries
            doc.Add(new StringField(ProductSeachFields.IsActive,
                p.IsActive ? "1" : "0", Field.Store.YES));

            doc.Add(new StringField(ProductSeachFields.IsDeleted,
                p.IsDeleted ? "1" : "0", Field.Store.YES));

            doc.Add(new StringField(ProductSeachFields.IsFeatured,
                p.IsFeatured ? "1" : "0", Field.Store.YES));

            doc.Add(new StringField(ProductSeachFields.HasImage,
                p.HasImage ? "1" : "0", Field.Store.YES));

            // Date — ticks as Int64 for sorting
            doc.Add(new Int64Field(ProductSeachFields.CreatedAt,
                p.CreatedAt.Ticks, Field.Store.YES));

            return doc;
        }

        // ──────────────────────────────────────────────
        //  Private helpers
        // ──────────────────────────────────────────────

        private IndexWriter CreateWriter()
        {
            var config = new IndexWriterConfig(AppLuceneVersion, _analyzer)
            {
                OpenMode = OpenMode.CREATE_OR_APPEND
            };
            return new IndexWriter(_directory, config);
        }

        public void Dispose()
        {
            _analyzer.Dispose();
            _directory.Dispose();
        }
    }
}

