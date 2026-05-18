namespace saccoshop.backgroundservices
{
    public class TempFileCleanupService : BackgroundService
    {
        private readonly IWebHostEnvironment _env;
        public TempFileCleanupService(IWebHostEnvironment env) => _env = env;

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            var tempDir = Path.Combine(_env.ContentRootPath, "uploads", "tmp");
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    if (Directory.Exists(tempDir))
                    {
                        var cutoff = DateTime.UtcNow.AddHours(-6);
                        foreach (var f in Directory.EnumerateFiles(tempDir))
                        {
                            if (System.IO.File.GetCreationTimeUtc(f) < cutoff)
                                System.IO.File.Delete(f);
                        }
                    }
                }
                catch { /* swallow, retry next cycle */ }
                await Task.Delay(TimeSpan.FromHours(1), ct);
            }
        }
    }
}
