using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{

    public class SubCategoryRepo : RepositoryBase<SubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<IEnumerable<ShowAllCategoriesDto>> GetAllSubCategories()
        {
            return await FindAll(false).Select(c => new ShowAllCategoriesDto()
            {
                CategoryName = c.SubCategoryName,
                RefId = c.SubCategoryId,

            }).ToListAsync();
        }
        public async Task<IEnumerable<CategoryDto>> GetAllSubCategoriesWithSubCategories(bool tracking)
        {
            return await FindAll(false).Select(c => new CategoryDto()
            {
                CategoryName = c.SubCategoryName,
                RefId = c.SubCategoryId,
                SubCategories = c.SubCategoryCategories.Select(sc => new ShowSubCategoryDto()
                {
                    SubCategoryName = sc.SubCategoryCategoryName
                }).OrderBy(p => p.SubCategoryName).ToList()
            }).OrderBy(p => p.CategoryName).ToListAsync();

        }
        public async Task<SubCategory?> FindSubCategoryById(int categoryId, bool tracking)
        {
            return await FindByCondition(k => k.SubCategoryId == categoryId, tracking).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<CategoryDto>> FindSubCategoryByName(string categoryName, bool tracking)
        {
            return await FindByCondition(k => k.SubCategoryName != null && EF.Functions.Like(k.SubCategoryName, $"%{categoryName}%"), tracking).Select(c => new CategoryDto()
            {
                CategoryName = c.SubCategoryName,
                RefId = c.SubCategoryId,
                SubCategories = c.SubCategoryCategories.Select(sc => new ShowSubCategoryDto()
                {
                    SubCategoryName = sc.SubCategoryCategoryName
                }).OrderBy(p=>p.SubCategoryName).ToList()
            }).ToListAsync();
        }
        public async Task<SubCategory?> FindSubCategoryByIdWithSubCategories(int categoryId)
        {
            return await FindByCondition(k => k.SubCategoryId == categoryId, false).Select(c => new SubCategory()
            {
                SubCategoryName = c.SubCategoryName,
                SubCategoryId = c.SubCategoryId,
                CategoryId = c.CategoryId,
                SubCategoryCategories = c.SubCategoryCategories.Select(sc => new SubCategoryCategory()
                {
                    SubCategoryCategoryName = sc.SubCategoryCategoryName,
                    SubCategoryCategoryId = sc.SubCategoryCategoryId,
                }).OrderBy(p => p.SubCategoryCategoryName).ToList()
            }).FirstOrDefaultAsync();
        }

        public void CreateSubCategory(SubCategory category) => CreateBase(category);
        public void UpdateSubCategory(SubCategory category) => UpdateBase(category);
        public void DeleteSubCategory(SubCategory category) => DeleteBase(category);

        public async Task<IEnumerable<SubCategoryRefDto>> FindCategorySubCategoryRefById(int categoryId)
        {
            return await FindByCondition(k => k.CategoryId == categoryId, false).Select(c => new SubCategoryRefDto()
            {
                SubCategoryId = c.SubCategoryId,
                SubCategoryName = c.SubCategoryName,
                CategoryId = c.CategoryId
            }).OrderBy(p => p.SubCategoryName).ToListAsync() ?? [];


        }

        public async Task<IEnumerable<MiniSubCategoryDto>> GetAllSubCategoriesInDb()=>await FindAll(false).Select(sc=>new MiniSubCategoryDto()
        {
            SubCategoryId=sc.SubCategoryId,
            SubCategoryName=sc.SubCategoryName,
            CategoryId=sc.CategoryId
        }).ToListAsync();
        public async Task<IEnumerable<SubCategorySeedDto>> GetSubCategorySeedData()
        {
            return await FindAll(false).Select(p => new SubCategorySeedDto()
            {
                CategoryId = p.CategoryId,
                SubCategoryName = p.SubCategoryName,
                SubCategoryId = p.SubCategoryId

            }).ToListAsync();
        }
    }
}