using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class CategoryRepo : RepositoryBase<Category>, ICategoryRepo
    {
        private readonly IMemoryCache _cache;
        private const string ProductTreeKey = "category-tree:products";
        private const string TradeTreeKey = "category-tree:trades";

        public CategoryRepo(IMemoryCache cache, ApplicationDbContext _db) : base(_db)
        {
_cache = cache;
        }

        public async Task<IEnumerable<CategoryTreeDto>> TradeCategories()
        {

            if (_cache.TryGetValue(TradeTreeKey, out IEnumerable<CategoryTreeDto>? cached))
                return cached!;

            var result = await FindByCondition(k => k.Type != null && EF.Functions.Like(k.Type, "%trade%"), false)

                .Select(c => new CategoryTreeDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,

                    SubCategories = c.SubCategories
                        .Select(sc => new SubCategoryTreeDto
                        {
                            SubCategoryId = sc.SubCategoryId,
                            SubCategoryName = sc.SubCategoryName,

                            SubCategoryCategories = sc.SubCategoryCategories
                                .Select(scc => new SubCategoryCategoryRefDto
                                {
                                    SubCategoryCategoryId = scc.SubCategoryCategoryId,
                                    SubCategoryId = scc.SubCategoryId,
                                    SubCategoryCategoryName = scc.SubCategoryCategoryName
                                })
                                .OrderBy(scc => scc.SubCategoryCategoryName)
                                .ToList()
                        })
                        .OrderBy(sc => sc.SubCategoryName)
                        .ToList()
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            _cache.Set(TradeTreeKey, result, TimeSpan.FromHours(1));
            return result;

        }
        public IQueryable<Category> CategoriesQueryData()=>FindAll(false);

        public async Task<IEnumerable<CategoryTreeDto>> ProductCategories()
        {
            if (_cache.TryGetValue(ProductTreeKey, out IEnumerable<CategoryTreeDto>? cached))
                return cached!;

            var result = await FindByCondition(c => c.Type != null && EF.Functions.Like(c.Type, "%product%"), false)

                .Select(c => new CategoryTreeDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,

                    SubCategories = c.SubCategories
                        .Select(sc => new SubCategoryTreeDto
                        {
                            SubCategoryId = sc.SubCategoryId,
                            SubCategoryName = sc.SubCategoryName,

                            SubCategoryCategories = sc.SubCategoryCategories
                                .Select(scc => new SubCategoryCategoryRefDto
                                {
                                    SubCategoryCategoryId = scc.SubCategoryCategoryId,
                                    SubCategoryId = scc.SubCategoryId,
                                    SubCategoryCategoryName = scc.SubCategoryCategoryName
                                })
                                .OrderBy(scc => scc.SubCategoryCategoryName)
                                .ToList()
                        })
                        .OrderBy(sc => sc.SubCategoryName)
                        .ToList()
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            _cache.Set(ProductTreeKey, result, TimeSpan.FromHours(2));
            return result;
        }
       
        public async  Task<IEnumerable<CategoryDto>> GetAllCategoriesWithSubCategories (bool tracking)
        {
            var query =  await FindAll(tracking).AsSplitQuery().Select(c => new CategoryDto
            {
                RefId = c.CategoryId,
                CategoryName = c.CategoryName,
                SubCategories=c.SubCategories!.Select(sc => new ShowSubCategoryDto
                {
                    SubCategoryName = sc.SubCategoryName,
                    SubCategoryCategories = sc.SubCategoryCategories!.Select(scc => new CategoryDto
                    {
                        RefId = scc.SubCategoryCategoryId,
                        CategoryName = scc.SubCategoryCategoryName,
                    }).ToList()
                }).ToList()
            }).ToListAsync();
            return query;

        }
        public async Task<Category?> FindCategoryById(int categoryId, bool tracking)
        {
            var category = await FindByCondition(k => k.CategoryId.Equals(categoryId), tracking).SingleOrDefaultAsync();
              return category;
        }
        public async Task<CategoryDto?> FindCategoryWithSubCategories(int categoryId)
        {
            var category = await FindByCondition(k => k.CategoryId.Equals(categoryId), false).Select(p => new CategoryDto() { CategoryName = p.CategoryName,  CategoryId = p.CategoryId, SubCategories = p.SubCategories.Select(k => new ShowSubCategoryDto() { SubCategoryName = k.SubCategoryName, SubCategoryId = k.SubCategoryId }).OrderBy(p => p.SubCategoryName).ToList() }).OrderBy(p => p.CategoryName).FirstOrDefaultAsync();
            return category;
        }
        public async Task<IEnumerable<CategoryDto>> FindCategoryByName(string categoryName, bool tracking)
        {
         
            return await FindByCondition(k=>k.CategoryName!=null&&EF.Functions.Like(k.CategoryName,$"%{categoryName}%"),tracking)
                .Select(c=>new CategoryDto() { CategoryName = c.CategoryName, RefId = c.CategoryId, SubCategories =c.SubCategories
                .Select(p=>new ShowSubCategoryDto(){ SubCategoryName=p.SubCategoryName,SubCategoryCategories=p.SubCategoryCategories
                .Select(h=>new CategoryDto() { CategoryName=h.SubCategoryCategoryName,RefId=h.SubCategoryCategoryId}).ToList().ToList()}).ToList()}).ToListAsync();
        }
        public async Task<IEnumerable<ShowAllCategoriesDto>> GetAllCategories() {
        return await FindAll(false).Select(c=>new ShowAllCategoriesDto()
        {
            CategoryName = c.CategoryName,
            RefId = c.CategoryId,
           
        }).OrderBy(p => p.CategoryName).ToListAsync();                  
        }
        public async Task<IEnumerable<CategoryRefDto>> GetAllTradeCategories()
        {
            return await FindByCondition((p => EF.Functions.Like(p.GeneralCategory!=null?p.GeneralCategory.GeneralCategoryName:"not categorized", "%trade%")),false).Select(c => new CategoryRefDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).OrderBy(p => p.CategoryName).ToListAsync();
        }
        

       public void CreateCategory(Category category)=>CreateBase(category);
       public void UpdateCategory(Category category)=>UpdateBase(category);
       public void DeleteCategory(Category category)=>DeleteBase(category);
        public async Task<IEnumerable<CategoryRefDto>> GetAllCategoryReferences()
        {
            return await FindAll(false).Select(c => new CategoryRefDto()
            {
                CategoryId= c.CategoryId,
                CategoryName = c.CategoryName,
            }).OrderBy(p=>p.CategoryName).ToListAsync();
        }
        public async Task<IEnumerable<MiniCategoryDto>> GetAllCategoriesInDb() =>
       await FindAll(false).Select(c=>new MiniCategoryDto() {CategoryId=c.CategoryId,CategoryName=c.CategoryName,Type=c.Type,GeneralCategoryId=c.GeneralCategoryId }).ToListAsync();

       public async Task<IEnumerable<CategorySeedDto?>> GetCategorySeedData()
        {
            return await FindAll(false).Select(p=>new CategorySeedDto()
            {
                CategoryId=p.CategoryId,
                CategoryName=p.CategoryName,
                Type=p.Type,
                GeneralCategoryId=p.GeneralCategoryId,
            }).ToListAsync();
        }

        public void Invalidate()
        {
            _cache.Remove(ProductTreeKey);
            _cache.Remove(TradeTreeKey);
        }
       public async Task<List<SlugInfo>> GetAllCategorySlugsAsync()
       {
           return await FindAll(false).Select(c => new SlugInfo()
           {
               UpdatedAt = DateTime.Now,
               Slug = c.CategoryName
           }).ToListAsync();
       }
    }
}