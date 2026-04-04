using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class GeneralCategoryRepo:RepositoryBase<GeneralCategory>,IGeneralCategoryRepo
    {
        public GeneralCategoryRepo(ApplicationDbContext context):base(context) 
        {
            
        }




        public async Task<IEnumerable<CategoryDto>> GetReferencedTradeGeneralCategories()
        {
            var categories = await FindByCondition(
                    g => g.Categories.Any(c => c.Type.ToLower() == "trade"),
                    false
                )
                .Select(gc => new CategoryDto
                {
                    CategoryId = gc.GeneralCategoryId,
                    CategoryName = gc.GeneralCategoryName,

                    SubCategories = gc.Categories
                        .Where(c => c.Type.ToLower() == "trade")
                        .Select(c => new ShowSubCategoryDto
                        {
                            SubCategoryId = c.CategoryId,
                            SubCategoryName = c.CategoryName
                        })
                        .ToList()
                })
                .ToListAsync();

            return categories;
        }
        public async Task<IEnumerable<CategoryDto>> GetReferencedProductGeneralCategories()
        {
            var categories = await FindByCondition(
                    g => g.Categories.Any(c => c.Type.ToLower() == "product"),
                    false
                )
                .Select(gc => new CategoryDto
                {
                    CategoryId = gc.GeneralCategoryId,
                    CategoryName = gc.GeneralCategoryName,

                    SubCategories = gc.Categories
                        .Where(c => c.Type.ToLower() == "product")
                        .Select(c => new ShowSubCategoryDto
                        {
                            SubCategoryId = c.CategoryId,
                            SubCategoryName = c.CategoryName
                        })
                        .ToList()
                })
                .ToListAsync();

            return categories;
        }
        public async  Task<IEnumerable<CategoryRefDto>> GetAllGeneralCategories()=>await FindAll(false).Select(gc=> new CategoryRefDto()
      {
          CategoryId = gc.GeneralCategoryId,
          CategoryName= gc.GeneralCategoryName,
      }).ToListAsync();
        public async Task<IEnumerable<CategoryDto>> GetReferencedGeneralCategories() => await FindByCondition(g => g.Categories.Count() > 0, false).Select(gc => new CategoryDto()
        {
            CategoryId = gc.GeneralCategoryId,
            CategoryName = gc.GeneralCategoryName,
            SubCategories = gc.Categories.Any() ? gc.Categories.Select(k => new ShowSubCategoryDto() { SubCategoryId = k.CategoryId, SubCategoryName = k.CategoryName }).ToList() : new List<ShowSubCategoryDto>(),
        }).ToListAsync();
        public async Task<IEnumerable<CategoryDto>> GetAllGeneralCategoriesWithCategories(bool tracking) => await FindAll(false).Select(gc => new CategoryDto()
        {
            CategoryId = gc.GeneralCategoryId,
            CategoryName = gc.GeneralCategoryName,
            SubCategories = gc.Categories.Any() ? gc.Categories.Select(k => new ShowSubCategoryDto() { SubCategoryId = k.CategoryId, SubCategoryName = k.CategoryName }).ToList() : new List<ShowSubCategoryDto>(),
        }).ToListAsync();
        public async Task<CategoryDto?> FindCategoryWithSubCategories(int generalCategoryId)=>await FindByCondition(p=>p.GeneralCategoryId==generalCategoryId,false).Select(gc => new CategoryDto()
        {
            CategoryId = gc.GeneralCategoryId,
            CategoryName = gc.GeneralCategoryName,
            SubCategories=gc.Categories.Any()?gc.Categories.Select(k=> new ShowSubCategoryDto() { SubCategoryId = k.CategoryId, SubCategoryName = k.CategoryName }).ToList() : new List<ShowSubCategoryDto>(),
        }).FirstOrDefaultAsync();
        public async Task<GeneralCategory?> FindGeneralCategoryById(int generalCategoryId, bool tracking)=> await FindByCondition(p=>p.GeneralCategoryId==generalCategoryId,tracking).FirstOrDefaultAsync();
        public async Task<IEnumerable<CategoryDto>> FindGeneralCategoryByName(string generalCategoryName, bool tracking)
        {

            if (!string.IsNullOrWhiteSpace(generalCategoryName))
            {
                var query = FindAll(tracking);
               var searchTerm = generalCategoryName.Trim();

             var categories=await query.Where(p =>EF.Functions.Like(p.GeneralCategoryName, $"%{searchTerm}%")).Select(p=>new CategoryDto()
                {
                    CategoryId = p.GeneralCategoryId,
                    CategoryName = p.GeneralCategoryName,
                }).ToListAsync();
                return categories;
            }
            else
            {
                return [];
            }
        }
        public void CreateGeneralCategory(GeneralCategory generalCategory)=>CreateBase(generalCategory);
        public void UpdateGeneralCategory(GeneralCategory generalCategory)=>UpdateBase(generalCategory);
        public void DeleteGeneralCategory(GeneralCategory generalCategory)=>DeleteBase(generalCategory);
        public async Task<GeneralCategory?> FindGeneralCategoryWithCategoriesById(int generalCategoryId, bool tracking)
        {
          return  await FindByCondition(p => p.GeneralCategoryId == generalCategoryId, false).Select(gc => new GeneralCategory()
            {
                GeneralCategoryId = gc.GeneralCategoryId,
                GeneralCategoryName = gc.GeneralCategoryName,
                Categories = gc.Categories.Any() ? gc.Categories.Select(k => new Category() { CategoryId = k.CategoryId, CategoryName = k.CategoryName }).ToList() : new List<Category>(),
            }).FirstOrDefaultAsync();
        }
    }
}
