using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using SixLabors.ImageSharp.Formats.Webp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class ProductImageService : IProductImageService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;


        public ProductImageService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _env = env;

        }



        public async Task<IEnumerable<ProductImageRefDto?>> GetAllProductImagesAsync() => await _repoManager.ProductImageRepo.GetAllProductImages();
        public async Task<ProductImage?> FindProductImageByIdAsync(Guid imageId, Guid productId, bool tracking) => await _repoManager.ProductImageRepo.FindProductImageByProductId(imageId, productId, tracking);
        public async Task<ProductImage?> GetPrimaryImageAsync(Guid productId,bool tracking) => await _repoManager.ProductImageRepo.GetPrimaryImage(productId,tracking);
        public async Task CreateProductImageAsync(ProductImageDto imageDto)
        {
            if (imageDto != null)
            {
                var image = _mapper.Map<ProductImage>(imageDto);
                _repoManager.ProductImageRepo.CreateProductImage(image);
                await _repoManager.SaveRepoDataAsync();
            }

        }
        public async Task UpdateProductImageAsync(ProductImageDto imageDto)
        {
            if (imageDto != null)
            {
                var oldImage = await _repoManager.ProductImageRepo.FindProductImageByProductId(imageDto.ProductImageId, (Guid)imageDto.ProductId, true);
                _mapper.Map(imageDto, oldImage);
                _repoManager.ProductImageRepo.UpdateProductImage(oldImage);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteProductImageIdRefAsync(Guid imageId, Guid productId)
        {
            var image = await _repoManager.ProductImageRepo
                .FindProductImageByProductId(imageId, productId, true);

            if (image == null)
                return;


            var productFolder = Path.Combine(
                _env.WebRootPath,
                "uploads", "products",
                "images",
                productId.ToString()
            );

            if (Directory.Exists(productFolder))
            {
                var files = Directory.GetFiles(
                    productFolder,
                    $"{imageId}_*"
                );

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

            _repoManager.ProductImageRepo.DeleteProductImage(image);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task CreateProductImageListAsync(ICollection<ProductImage> images)
        {
            if (images != null && images.Count > 0)
            {
                foreach (var img in images)
                {
                    _repoManager.ProductImageRepo.CreateProductImage(img);
                }

                await _repoManager.SaveRepoDataAsync();
            }
            else return;
        }

        public async Task MakeImagePrimaryAsync(MiniProductImageDto miniProduct)
        {
            var primaryImage = await _repoManager.ProductImageRepo.GetPrimaryImage(miniProduct.ProductId, true);
            if (primaryImage != null)
            {
                primaryImage.IsPrimary = false;
                await _repoManager.SaveRepoDataAsync();
            }

            var imageData = await _repoManager.ProductImageRepo.FindProductImageById(miniProduct.ImageId, true);
            if (imageData != null)
            {
                imageData.IsPrimary = true;
                await _repoManager.SaveRepoDataAsync();
            }
            else return;

        }

        public async Task DeleteProductImageAsync(Guid imageId)
        {
            var image = await _repoManager.ProductImageRepo.FindProductImageById(imageId, true);
            if (image != null)
            {
                _repoManager.ProductImageRepo.DeleteProductImage(image);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"product image with id {imageId} not found");
            }

        }



  

    }
}