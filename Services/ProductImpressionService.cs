using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class ProductImpressionService : IProductImpressionService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public ProductImpressionService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async  Task<IEnumerable<ProductImpression>> GetAllProductImpressionsAsync() => await _repoManager.ProductImpressionRepo.GetAllProductImpressions();

        public async Task<ProductImpression?> FindProductImpressionByIdAsync(Guid productImpressionId, bool tracking) => await _repoManager.ProductImpressionRepo.FindProductImpressionById(productImpressionId, tracking);

        public async Task<ProductImpression?> FindProductImpressionByProductIdAsync(Guid productId, bool tracking)=> await _repoManager.ProductImpressionRepo.FindProductImpressionByProductId(productId,tracking);
       
        public async Task CreateProductImpressionAsync(NewImpressionDto newImpression)
        {
            ProductImpression impression = new()
            {
                ProductId = newImpression.ItemId,
                UserProfileId = newImpression.UserProfileId
            };

            _repoManager.ProductImpressionRepo.CreateProductImpression(impression);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateProductImpressionAsync(ProductImpression impression)
        {
            var oldImpression = await _repoManager.ProductImpressionRepo.FindProductImpressionForUpdate(impression.ProductImpressionId);
            if (oldImpression != null)
            {
                oldImpression.ProductId = impression.ProductId;

                _repoManager.ProductImpressionRepo.UpdateProductImpression(oldImpression);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteProductImpressionAsync(Guid productImpressionId) { 
            var impression = await _repoManager.ProductImpressionRepo.FindProductImpressionForUpdate(productImpressionId);
            if (impression != null)
            {
                _repoManager.ProductImpressionRepo.DeleteProductImpression(impression);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"object not found for id {productImpressionId}");
            }
        
        }
    }
}
