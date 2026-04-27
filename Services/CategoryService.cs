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

    internal sealed class CategoryService : ICategoryService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }


       public async Task<IEnumerable<ShowAllCategoriesDto>> GetAllCategoriesAsync()=>
            await _repoManager.CategoryRepo.GetAllCategories();
        public async Task<IEnumerable<CategoryRefDto>> GetAllTradeCategoriesAsync() =>
         await _repoManager.CategoryRepo.GetAllTradeCategories();
        
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesWithSubCategoriesAsync()=>
            await _repoManager.CategoryRepo.GetAllCategoriesWithSubCategories(false); 
        public async Task CreateCategoryAsync(CategoryDto category)
        {
            var categoryEntity = new Category()
            {
                CategoryName = category.CategoryName,
                GeneralCategoryId = category.GeneralCategoryId,
                Type=category.Type
                
            };
                
            _repoManager.CategoryRepo.CreateCategory(categoryEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateCategoryAsync(CategoryDto category)
        {
            var categoryEntity = await _repoManager.CategoryRepo.FindCategoryById(category.CategoryId, true);
            if (categoryEntity == null)
            {
                _logger.LogError($"Category with id: {category.CategoryId} not found.");
                throw new KeyNotFoundException("Category not found");
            }

            categoryEntity.CategoryName = category.CategoryName;
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _repoManager.CategoryRepo.FindCategoryById(categoryId, true);
            if (category == null)
            {
                _logger.LogError($"Category with id: {categoryId} not found.");
                throw new KeyNotFoundException("Category not found");
            }
            _repoManager.CategoryRepo.DeleteCategory(category);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task<Category?> FindCategoryDtoByIdAsync(int categoryId, bool tracking)=> await _repoManager.CategoryRepo.FindCategoryById(categoryId,tracking);
        public async Task<CategoryDto?> FindCategoryWithSubCategoriesAsync(int categoryId) => await _repoManager.CategoryRepo.FindCategoryWithSubCategories(categoryId);

        public async Task<IEnumerable<CategoryRefDto>> GetAllCategoryReferencesAsync()=>
            await _repoManager.CategoryRepo.GetAllCategoryReferences();

        public async Task<IEnumerable<MiniCategoryDto>> GetAllCategoriesInDbAsync() => await _repoManager.CategoryRepo.GetAllCategoriesInDb();
        public async Task<AllCategoriesRef> GetAllCategoriesRefInDbAsync()
        {
            AllCategoriesRef categoriesRef = new AllCategoriesRef()
            {
                Categories = await _repoManager.CategoryRepo.GetAllCategoriesInDb(),
                SubCategories = await _repoManager.SubCategoryRepo.GetAllSubCategoriesInDb(),
                SubCategoryCategories = await _repoManager.SubCategoryCategoryRepo.GetAllSubCategoryCategoriesInDb(),
            };
            return categoriesRef;

        }

    public async  Task<IEnumerable<CategoryTreeDto>> TradeCategoriesAsync()=>await _repoManager.CategoryRepo.TradeCategories();
    public async Task<IEnumerable<CategoryTreeDto>> ProductCategoriesAsync() => await _repoManager.CategoryRepo.ProductCategories();

        public async Task<CategoriesSubCategoryCategoryDto> GetAllCategorySeedDataAsync()
        {
            var categories = await _repoManager.CategoryRepo.GetCategorySeedData();
            var subCategoryCategories = await _repoManager.SubCategoryCategoryRepo.GetSubCategoryCAtegorySeedData();
            var subCategories = await _repoManager.SubCategoryRepo.GetSubCategorySeedData();


            CategoriesSubCategoryCategoryDto subs = new CategoriesSubCategoryCategoryDto()
            {
                Categories = (ICollection<CategorySeedDto>)categories,
                SubCategories = (ICollection<SubCategorySeedDto>)subCategories,
                SubCategoryCategories = (ICollection<SubCategoryCategorySeedDto>)subCategoryCategories
            };
            return subs;

        }


    }
}