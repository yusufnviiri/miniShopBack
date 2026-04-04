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
    internal sealed class GeneralCategoryService : IGeneralCategoryService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public GeneralCategoryService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryRefDto>> GetAllGeneralCategoriesAsync()
        {
            var categories = await _repoManager.GeneralCategoryRepo.GetAllGeneralCategories();
            return categories;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllGeneralCategoriesWithCategoriesAsync(bool tracking) => await _repoManager.GeneralCategoryRepo.GetAllGeneralCategoriesWithCategories(tracking);
        public async Task<CategoryDto?> FindCategoryWithSubCategoriesAsync(int generalCategoryId) => await _repoManager.GeneralCategoryRepo.FindCategoryWithSubCategories(generalCategoryId);
        public async Task<GeneralCategory?> FindGeneralCategoryByIdAsync(int generalCategoryId, bool tracking) => await _repoManager.GeneralCategoryRepo.FindGeneralCategoryById(generalCategoryId, tracking);
        public async Task<IEnumerable<CategoryDto>> FindGeneralCategoryByNameAsync(string generalCategoryName, bool tracking)
        {
            var category = await _repoManager.GeneralCategoryRepo.FindGeneralCategoryByName(generalCategoryName, tracking);
            return category;
        }
        public async Task CreateGeneralCategoryAsync(GeneralCategory generalCategory)
        {
            _repoManager.GeneralCategoryRepo.CreateGeneralCategory(generalCategory);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateGeneralCategoryAsync(GeneralCategory generalCategory)
        {
            var category = await _repoManager.GeneralCategoryRepo.FindGeneralCategoryById(generalCategory.GeneralCategoryId, true);
            if (category != null)
            {
                _repoManager.GeneralCategoryRepo.UpdateGeneralCategory(category);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"object with id {generalCategory.GeneralCategoryId} not found");
            }
        }
        public async Task DeleteGeneralCategoryAsync(int generalCategoryId)
        {
            var category = await _repoManager.GeneralCategoryRepo.FindGeneralCategoryById(generalCategoryId, true);
            if (category != null)
            {
                _repoManager.GeneralCategoryRepo.DeleteGeneralCategory(category);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"object with id {generalCategoryId} not found");
            }
        }
        public async Task AddCategoryToGeneralCategoryAsync(GeneralCategoryUpdateDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // 1️⃣ Fetch general category with tracking
            var generalCategory = await _repoManager
                .GeneralCategoryRepo
                .FindGeneralCategoryWithCategoriesById(dto.GeneralCategoryId,true);

            if (generalCategory is null)
                throw new ObjectBadRequestExeption(
                    $"General Category with id {dto.GeneralCategoryId} was not found.");

            // 2️⃣ Update name only if changed (case-insensitive safe compare)
            if (!string.IsNullOrWhiteSpace(dto.GeneralCategoryName) &&
                !string.Equals(dto.GeneralCategoryName.Trim(),
                               generalCategory.GeneralCategoryName,
                               StringComparison.OrdinalIgnoreCase))
            {
                generalCategory.GeneralCategoryName = dto.GeneralCategoryName.Trim();
            }

            // 3️⃣ Determine which category IDs are actually new
            var existingCategoryIds = generalCategory.Categories
                .Select(c => c.CategoryId)
                .ToHashSet(); // O(1) lookups

            var newCategoryIds = dto.CategoryIds
                .Where(id => !existingCategoryIds.Contains(id))
                .ToList();

            if (!newCategoryIds.Any())
            {
                await _repoManager.SaveRepoDataAsync();
                return;
            }

            // 4️⃣ Fetch all categories in ONE query (avoid N+1)

            foreach(var item  in newCategoryIds)
            {
                var categoryToAttach = await _repoManager.CategoryRepo.FindCategoryById(item, tracking: true);
                if (categoryToAttach != null)
                {
                    categoryToAttach.GeneralCategoryId = generalCategory.GeneralCategoryId;
                    await _repoManager.SaveRepoDataAsync();
                }
                ;
               


            }

         

          
        }

        public async Task<IEnumerable<CategoryDto>> GetReferencedGeneralCategoriesAsync()=>await _repoManager.GeneralCategoryRepo.GetReferencedGeneralCategories();

        public async Task<IEnumerable<CategoryDto>> GetReferencedProductGeneralCategoriesAsync()=>await _repoManager.GeneralCategoryRepo.GetReferencedProductGeneralCategories();
        public async Task<IEnumerable<CategoryDto>> GetReferencedTradeGeneralCategoriesAsync()=> await _repoManager.GeneralCategoryRepo.GetReferencedTradeGeneralCategories();


    }
}