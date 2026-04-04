using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.context;
using Services.Hubs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ImageProcessingWorker : BackgroundService
    {
        private readonly IBackgroundJobQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<ProductHub> _hubContext;



        public ImageProcessingWorker(IBackgroundJobQueue queue, IServiceScopeFactory scopeFactory, IWebHostEnvironment env, IHubContext<ProductHub> hubContext)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _env = env;
            _hubContext = hubContext;

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _queue.DequeueAsync(stoppingToken);

                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await ProcessProductImage(job, db);
            }
        }

        private async Task ProcessProductImage(ImageJob job, ApplicationDbContext _context)
        {
            using var image = await Image.LoadAsync(job.TempPath);
           
                var outputDir = Path.Combine(_env.WebRootPath,   // 🔥 THIS is the fix
            "uploads","products", "images", job.ProductId.ToString());

                Directory.CreateDirectory(outputDir);



                var imageSizes = new[]{(name: "thumb", width: 150), (name: "medium", width: 600),    (name: "large", width: 1200)
};

                int totalSteps = imageSizes.Length;
                int completedSteps = 0;

                await _hubContext.Clients.Group(job.ProductId.ToString()).SendAsync("ImageProgress", new
                {
                    productId = job.ProductId,
                    imageId = job.ImageId,
                    progress = 0
                });
                foreach (var (name, width) in imageSizes)
                {
                    using var clone = image.Clone(ctx =>
                        ctx.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(width, 0)
                        }));

                    var path = Path.Combine(outputDir, $"{job.ImageId}_{name}.webp");

                    await clone.SaveAsync(path, new WebpEncoder
                    {
                        Quality = 75 
                    });



                    completedSteps++;

                    int percent = (int)((completedSteps / (double)totalSteps) * 100);

                    await _hubContext.Clients
                        .Group(job.ProductId.ToString())
                        .SendAsync("ImageProgress", new
                        {
                            productId = job.ProductId,
                            imageId = job.ImageId,
                            progress = percent
                        });
                }
                File.Delete(job.TempPath);

                var processedImage = await _context.ProductImages
         .FirstOrDefaultAsync(x => x.ProductImageId == job.ImageId);

                if (processedImage != null)
                {

                    processedImage.IsProcessed = true;
                    await _context.SaveChangesAsync();

                    await _hubContext.Clients.Group(job.ProductId.ToString())
        .SendAsync("ImageCompleted", new
        {
            productId = job.ProductId,
            imageId = job.ImageId
        });



               


                }

            }


        }



    }


