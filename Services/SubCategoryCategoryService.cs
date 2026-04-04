using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
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
    internal sealed class SubCategoryCategoryService : ISubCategoryCategoryService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public SubCategoryCategoryService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

      public async  Task<IEnumerable<CategoryDto>> GetAllSubCategoryCategoriesAsync()=>
            await _repoManager.SubCategoryCategoryRepo.GetAllSubCategoryCategories();
        public async Task<SubCategoryCategory?> FindSubCategoryCategoryByIdAsync(int categoryId, bool tracking)=>
            await _repoManager.SubCategoryCategoryRepo.FindSubCategoryCategoryById(categoryId, tracking);
        public async Task<IEnumerable<CategoryDto>> FindSubCategoryCategoryByNameAsync(string categoryName, bool tracking)=>
            await _repoManager.SubCategoryCategoryRepo.FindSubCategoryCategoryByName(categoryName, tracking);
        public async Task CreateSubCategoryCategoryAsync(SubCategoryCategory category)
        {
            var categoryEntity = _mapper.Map<SubCategoryCategory>(category);
            _repoManager.SubCategoryCategoryRepo.CreateSubCategoryCategory(categoryEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateSubCategoryCategoryAsync(SubCategoryCategory category)
        {
            var categoryEntity = await _repoManager.SubCategoryCategoryRepo.FindSubCategoryCategoryById(category.SubCategoryCategoryId, true);
            if (categoryEntity == null)
            {
                _logger.LogError($"SubCategoryCategory with id: {category.SubCategoryCategoryId} not found.");
                throw new KeyNotFoundException("SubCategoryCategory not found");
            }
            //_mapper.Map(category, categoryEntity);
            categoryEntity.SubCategoryCategoryName = category.SubCategoryCategoryName;
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteSubCategoryCategoryAsync(int subCategoryCategoryId)
        {
            var categoryEntity = await _repoManager.SubCategoryCategoryRepo.FindSubCategoryCategoryById(subCategoryCategoryId, true);
            if (categoryEntity == null)
            {
                _logger.LogError($"SubCategoryCategory with id: {subCategoryCategoryId} not found.");
                throw new KeyNotFoundException("SubCategoryCategory not found");
            }
            _repoManager.SubCategoryCategoryRepo.DeleteSubCategoryCategory(categoryEntity);
            await _repoManager.SaveRepoDataAsync();
        }

        public async Task<IEnumerable<SubCategoryCategoryRefDto>> FindSubCategoryCategoryRefByIdAsync(int subCategoryId)=>
            await _repoManager.SubCategoryCategoryRepo.FindSubCategoryCategoryRefById(subCategoryId);

    }
}