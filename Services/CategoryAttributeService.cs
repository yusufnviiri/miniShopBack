using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
     internal sealed class CategoryAttributeService : ICategoryAttributeService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;

        public CategoryAttributeService(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repoManager = repository;
        }
        public async  Task<IEnumerable<CategoryAttributeDto>> GetAllCategoryAttributesAsync()=>await _repoManager.CategoryAttributeRepo.GetAllCategoryAttributes();
        public async Task<CategoryAttribute?> FindCategoryAttributeAsync(int CategoryAttributeId, bool tracking)=>await _repoManager.CategoryAttributeRepo.FindCategoryAttribute(CategoryAttributeId,tracking);
        public async Task CreateCategoryAttributeAsync(CategoryAttribute categoryAttribute)
        {
            _repoManager.CategoryAttributeRepo.CreateCategoryAttribute(categoryAttribute);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateCategoryAttributeAsync(CategoryAttribute attribute)
        {
            var existingAttribute = await _repoManager.CategoryAttributeRepo.FindCategoryAttribute(attribute.CategoryAttributeId, true);
            if (existingAttribute is null)
            {
                _logger.LogError($"Category Attribute with id: {attribute.CategoryAttributeId} not found.");
                throw new ObjectBadRequestExeption($"Category Attribute with id: {attribute.CategoryAttributeId} not found.");
            }
            existingAttribute.AttributeName = attribute.AttributeName;
            existingAttribute.AttributeDataTypeId = attribute.AttributeDataTypeId;
            _repoManager.CategoryAttributeRepo.UpdateCategoryAttribute(existingAttribute);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteCategoryAttributeAsync(int categoryId)
        {
            var existingAttribute = await _repoManager.CategoryAttributeRepo.FindCategoryAttribute(categoryId, true);
            if (existingAttribute is null)
            {
                _logger.LogError($"Category Attribute with id: {categoryId} not found.");
                throw new ObjectBadRequestExeption($"Category Attribute with id: {categoryId} not found.");
            }

        }
        public async Task<IReadOnlyList<CategoryAttributeDto>> FindCategoryCategoryAttributesAsync(int CategoryId) => await _repoManager.CategoryAttributeRepo.FindCategoryCategoryAttributes(CategoryId);

    }
}
