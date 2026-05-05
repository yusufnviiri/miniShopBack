using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directory = System.IO.Directory;
using Lucene.Net.Analysis.En;


namespace Repository.lucene
{
/// <summary>
/// Owns the IndexWriter and SearcherManager for a single Lucene index.
/// One instance per named index (e.g., "products", "users").
/// Thread-safe. Long-lived (singleton). Disposed on app shutdown.
/// </summary>
public sealed class LuceneIndexContext : IDisposable
    {
        public const LuceneVersion Version = LuceneVersion.LUCENE_48;

        public string Name { get; }
        public Analyzer Analyzer { get; }
        public IndexWriter Writer { get; }
        public SearcherManager SearcherManager { get; }

        private readonly FSDirectory _directory;
        private bool _disposed;

        public LuceneIndexContext(string name, string indexPath, double ramBufferMb)
        {
            Name = name;

            // Ensure the folder exists.
          Directory.CreateDirectory(indexPath);

            // FSDirectory.Open picks the best file-system directory implementation
            // for the OS (typically MMapDirectory on 64-bit, which uses memory-mapped
            // I/O — this is the key to Lucene's RAM efficiency).
            _directory = FSDirectory.Open(indexPath);

            // StandardAnalyzer: lowercases, splits on whitespace/punctuation,
            // removes common English stop words. Good default for product text.
            //Analyzer = new StandardAnalyzer(Version);
            Analyzer = new EnglishAnalyzer(Version);

            var config = new IndexWriterConfig(Version, Analyzer)
            {
                OpenMode = OpenMode.CREATE_OR_APPEND,
                RAMBufferSizeMB = ramBufferMb,
            };

            Writer = new IndexWriter(_directory, config);

            // SearcherManager gives us a thread-safe, refreshable view of the index.
            // The 'true' parameter applies all pending deletes when refreshing.
            SearcherManager = new SearcherManager(Writer, applyAllDeletes: true, null);
        }

        /// <summary>
        /// Refreshes the searcher to pick up writes since the last refresh.
        /// Called periodically by SearcherRefreshService.
        /// Cheap if nothing changed; safe to call often.
        /// </summary>
        public void MaybeRefresh() => SearcherManager.MaybeRefresh();

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            // Order matters: searcher first (depends on writer), then writer, then directory.
            SearcherManager?.Dispose();
            Writer?.Dispose();
            Analyzer?.Dispose();
            _directory?.Dispose();
        }
    }
}

