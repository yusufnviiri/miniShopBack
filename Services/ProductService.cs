using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Services.BusinessRules;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class ProductService:IProductService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SlugService _slugService;



        public ProductService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager,SlugService slugService)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _slugService = slugService;

        }

        private  string CreateSlug(string name)
        {
            return _slugService.Generate(name);
        }

        public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsAsync()=>await _repoManager.ProductRepo.GetAllProducts(tracking: false);
        public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategoryAsync(string categoryName)=>await _repoManager.ProductRepo.GetAllProductsByCategory(tracking: false, categoryName);
        public async Task<ShowProductDto?> FindProductByIdAsync(bool tracking, Guid productId)=>await _repoManager.ProductRepo.FindProductById(tracking, productId);

        public async Task<ProductDataDto?> GetProductDataAsync(Guid productId)
        {
            var product = await _repoManager.ProductRepo.GetProductData(productId);

            if (product != null)
            {
                product.Contact = await _repoManager.UserProfileRepo.GetUserContact(product.SellerUserProfileId)??"";
                product.WhatsAppNumber = await _repoManager.SellerProfileRepo.GetSellerWhatsAppNumber(product.SellerUserProfileId) ?? "";

            }
            return product;
        }


        public async Task<ProductDataDto?> GetProductDataBySlugNameAsync(string slugName)
        {
            var product = await _repoManager.ProductRepo.GetProductDataUsingSlugName(slugName);

            if (product != null)
            {
                product.Contact = await _repoManager.UserProfileRepo.GetUserContact(product.SellerUserProfileId) ?? "";
                product.WhatsAppNumber = await _repoManager.SellerProfileRepo.GetSellerWhatsAppNumber(product.SellerUserProfileId) ?? "";

            }
            return product;
        }

        public async Task<Product> CreateProductAsync(NewProductDto product)
        {
           
           
            var productEntity = _mapper.Map<Product>(product);
            var numberOfProducts = await _repoManager.ProductRepo.NumberOfProducts();
            productEntity.Slug = $"{CreateSlug(product.ProductName)}-{numberOfProducts + 1}";
            var (commodityClass, sellerProfileId) = await SellerRules.CommodityClassToSellerRef(product.SellerId, _repoManager);


            productEntity.CommodityClassId=commodityClass;
            productEntity.SellerProfileId=sellerProfileId;
            _repoManager.ProductRepo.CreateProduct(productEntity);
            await _repoManager.SaveRepoDataAsync();
            return productEntity;
        }
        public async Task UpdateProductAsync(NewProductDto product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _repoManager.ProductRepo.UpdateProduct(productEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteProductAsync(Guid productId)
        {
            var productEntity = await _repoManager.ProductRepo.FindProductForUpdate( productId);
            if (productEntity == null)
            {
                _logger.LogError($"Product with id: {productId} not found.");
                throw new ArgumentNullException(nameof(productId), "Product not found.");
            }
            _repoManager.ProductRepo.DeleteProduct(productEntity);
            await _repoManager.SaveRepoDataAsync(); 
        }

     
     

        public async Task<(ICollection<HomePageProductDto> productsData, MetaData MetaData) >GetHomePageProductsAsync(
        ProductRequestParameters requestParameters) {
          var products=  await _repoManager.ProductRepo.GetHomePageProducts(requestParameters);
            return (productsData: products, products.MetaData);

        }
        public async Task<ShowProductDto?> FindSellerProductAsync(Guid productId)=>await _repoManager.ProductRepo.FindSellerProduct(productId);
        public async Task<ShowProductDto?> FindSellerProductBySlugAsync(string slug)=>await _repoManager.ProductRepo.FindProductBySlugName(false,slug);

        public async Task UpdateProductPriceAsync(Guid productId, decimal newPrice)
        {
            var productForUpdate = await _repoManager.ProductRepo.FindProductForUpdate(productId);
            if (productForUpdate != null)
            {
                productForUpdate.Price = newPrice;
                await _repoManager.SaveRepoDataAsync();
            } else
            {
                throw new ObjectBadRequestExeption($"product with id {productId} not found");
            }
        }

        public async Task SetProductOldPriceAsync(Guid productId, decimal oldPrice)
        {
            var productForUpdate = await _repoManager.ProductRepo.FindProductForUpdate(productId);
            if (productForUpdate != null)
            {
                productForUpdate.OldPrice = oldPrice;
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"product with id {productId} not found");
            }
        }
        public async Task MakeAllProductsFeautured()
        {
            _repoManager.ProductRepo.MakeAllProductsFeautured();
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task ToggleProductFeaturedState(Guid productId)
        {
            var productForUpdate = await _repoManager.ProductRepo.FindProductForUpdate(productId);
            if (productForUpdate != null)
            {
                productForUpdate.IsFeatured = !productForUpdate.IsFeatured;
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"product with id {productId} not found");
            }

        }



        public async Task UpdateProductDescription(SharedUpdatesDto sharedUpdates)
        {
            var product = await _repoManager.ProductRepo.FindProductForUpdate(sharedUpdates.ItemId);
            if (product != null)
            {
                product.Description = sharedUpdates.ItemDescription;
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"trade with id {sharedUpdates.ItemId} not found");
            }

        }
        public async Task<HomePageCustomProductsDto?> HomePageCustomProductsAsync() => await _repoManager.ProductRepo.HomePageCustomProducts();
    }
}
