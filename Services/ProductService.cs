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
 

        public ProductService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }


       public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsAsync()=>await _repoManager.ProductRepo.GetAllProducts(tracking: false);
        public async Task<IEnumerable<ShowProductMiniDetailsDto>> GetAllProductsByCategoryAsync(string categoryName)=>await _repoManager.ProductRepo.GetAllProductsByCategory(tracking: false, categoryName);
        public async Task<ShowProductDto?> FindProductByIdAsync(bool tracking, Guid productId)=>await _repoManager.ProductRepo.FindProductById(tracking, productId);

        public async Task<ProductDataDto?> GetProductDataAsync(Guid productId) => await _repoManager.ProductRepo.GetProductData(productId);
        public async Task<Product> CreateProductAsync(NewProductDto product)
        {
           
            var productEntity = _mapper.Map<Product>(product);
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
            var productEntity = await _repoManager.ProductRepo.FindProductById(tracking: true, productId);
            if (productEntity == null)
            {
                _logger.LogError($"Product with id: {productId} not found.");
                throw new ArgumentNullException(nameof(productId), "Product not found.");
            }
            _repoManager.ProductRepo.DeleteProduct(_mapper.Map<Product>(productEntity));
            await _repoManager.SaveRepoDataAsync(); 
        }

        public async Task<double> GetProductStockQuantityAsync(Guid productId)=>
            await _repoManager.ProductRepo.GetProductStockQuantity(productId);
        public async Task UpdateProductStockLevelAsync(Guid productId, double quantityToDeduct)
        {
            var productEntity = await _repoManager.ProductRepo.FindProductForUpdate(productId) ?? throw new ItemNotFoundException(productId);

            if (quantityToDeduct <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantityToDeduct));

            if (productEntity.StockQuantity < quantityToDeduct)
                throw new InvalidOperationException("Insufficient stock.");

            productEntity.StockQuantity -= (int)quantityToDeduct;

            await _repoManager.SaveRepoDataAsync();       


        }

        public async Task<(ICollection<HomePageProductDto> productsData, MetaData MetaData) >GetHomePageProductsAsync(
        ProductRequestParameters requestParameters) {
          var products=  await _repoManager.ProductRepo.GetHomePageProducts(requestParameters);
            return (productsData: products, products.MetaData);

        }
        public async Task<ShowProductDto?> FindSellerProductAsync(Guid productId)=>await _repoManager.ProductRepo.FindSellerProduct(productId);

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

        public async Task UpdateProductStockAsync(Guid productId, int newStockQuantity)
        {
            var productForUpdate = await _repoManager.ProductRepo.FindProductForUpdate(productId);
            if (productForUpdate != null)
            {
                productForUpdate.StockQuantity = newStockQuantity;
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"product with id {productId} not found");
            }
        }


        public void MakeAllProductsFeautured()
        {
            _repoManager.ProductRepo.MakeAllProductsFeautured();
           
        }
        public void MakeProductFeautured(Guid productId)
        {
            _repoManager.ProductRepo.MakeProductFeautured(productId);

        }

 

    }
}
