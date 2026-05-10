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
    internal sealed class SubCategoryService : ISubCategoryService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public SubCategoryService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

      public async  Task<IEnumerable<ShowAllCategoriesDto>> GetAllSubCategoriesAsync()=>
            await _repoManager.SubCategoryRepo.GetAllSubCategories();
      

        public async Task<IEnumerable<CategoryDto>> GetAllSubCategoriesWithSubCategoriesAsync(bool tracking)=>
            await _repoManager.SubCategoryRepo.GetAllSubCategoriesWithSubCategories(tracking);
        public async Task<SubCategory?> FindSubCategoryByIdAsync(int categoryId, bool tracking)=>
            await _repoManager.SubCategoryRepo.FindSubCategoryById(categoryId, tracking);
        public async Task<IEnumerable<CategoryDto>> FindSubCategoryByNameAsync(string categoryName, bool tracking)=>
            await _repoManager.SubCategoryRepo.FindSubCategoryByName(categoryName, tracking);
        public async Task CreateSubCategoryAsync(SubCategoryDto category)
        {
            var categoryEntity = _mapper.Map<SubCategory>(category);
            _repoManager.SubCategoryRepo.CreateSubCategory(categoryEntity);
            await _repoManager.SaveRepoDataAsync();
            _repoManager.CategoryRepo.Invalidate();

        }
        public async Task UpdateSubCategoryAsync(SubCategory category)
        {
            var categoryEntity = await _repoManager.SubCategoryRepo.FindSubCategoryById(category.SubCategoryId, true);
            if (categoryEntity == null)
            {
                _logger.LogError($"SubCategory with id: {category.SubCategoryId} not found.");
                throw new KeyNotFoundException("SubCategory not found");
            }
            categoryEntity.SubCategoryName = category.SubCategoryName;
            //_mapper.Map(category, categoryEntity);
            await _repoManager.SaveRepoDataAsync();
            _repoManager.CategoryRepo.Invalidate();

        }
        public async Task DeleteSubCategory(int categoryId)
        {
            var categoryEntity = await _repoManager.SubCategoryRepo.FindSubCategoryById(categoryId, true);
            if (categoryEntity == null)
            {
                _logger.LogError($"SubCategory with id: {categoryId} not found.");
                throw new KeyNotFoundException("SubCategory not found");
            }
            _repoManager.SubCategoryRepo.DeleteSubCategory(categoryEntity);
            await _repoManager.SaveRepoDataAsync();
            _repoManager.CategoryRepo.Invalidate();

        }
        public async Task<SubCategory?> FindSubCategoryByIdWithSubCategoriesAsync(int subCategoryId)=>await _repoManager.SubCategoryRepo.FindSubCategoryByIdWithSubCategories(subCategoryId);
        public async Task<IEnumerable<SubCategoryRefDto>> FindCategorySubCategoryRefByIdAsync(int categoryId)=>
            await _repoManager.SubCategoryRepo.FindCategorySubCategoryRefById(categoryId);

        public async Task<IEnumerable<MiniSubCategoryDto>> GetAllSubCategoriesInDbAsync()=>await _repoManager.SubCategoryRepo.GetAllSubCategoriesInDb();


    }
}