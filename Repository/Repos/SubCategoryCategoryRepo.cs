using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;


namespace Repository.Repos
{
   public class SubCategoryCategoryRepo : RepositoryBase<SubCategoryCategory>, ISubCategoryCategoryRepo
    {
        public SubCategoryCategoryRepo(ApplicationDbContext _db) : base(_db)
        {

        }


      public async  Task<IEnumerable<CategoryDto>> GetAllSubCategoryCategories()
        {
            return await FindAll(false).Select(c => new CategoryDto()
            {
                CategoryName = c.SubCategoryCategoryName,
                RefId = c.SubCategoryCategoryId,
            }).ToListAsync();
        }
        public async Task<SubCategoryCategory?> FindSubCategoryCategoryById(int categoryId, bool tracking)
        {
            return await FindByCondition(k => k.SubCategoryCategoryId.Equals(categoryId), tracking).SingleOrDefaultAsync();

        }
        public async Task<IEnumerable<CategoryDto>> FindSubCategoryCategoryByName(string categoryName, bool tracking)
        {
            return await FindByCondition(k => k.SubCategoryCategoryName != null && EF.Functions.Like(k.SubCategoryCategoryName, $"%{categoryName}%"), tracking).Select(c => new CategoryDto()
            {
                CategoryName = c.SubCategoryCategoryName,
                RefId = c.SubCategoryCategoryId,
            }).ToListAsync();
        }
        public void CreateSubCategoryCategory(SubCategoryCategory category)=>CreateBase(category);
        public void UpdateSubCategoryCategory(SubCategoryCategory category)=>UpdateBase(category);
        public void DeleteSubCategoryCategory(SubCategoryCategory category)=>DeleteBase(category);
        public async Task<IEnumerable<SubCategoryCategoryRefDto>> FindSubCategoryCategoryRefById(int subCategoryId)
        {
            return await FindByCondition(k => k.SubCategoryId == subCategoryId, false)
                .Select(c => new SubCategoryCategoryRefDto()
                {
                    SubCategoryCategoryId = c.SubCategoryCategoryId,
                    SubCategoryId = c.SubCategoryId,
                    SubCategoryCategoryName = c.SubCategoryCategoryName,
                }).OrderBy(p => p.SubCategoryCategoryName).ToListAsync() ?? [];
        }  

        public async Task<IEnumerable<MiniSubCategoryCategoryDto>> GetAllSubCategoryCategoriesInDb()=>await FindAll(false).Select(scc=>new MiniSubCategoryCategoryDto()
        {
            SubCategoryCategoryId=scc.SubCategoryCategoryId,
            SubCategoryCategoryName=scc.SubCategoryCategoryName,
            SubCategoryId=scc.SubCategoryCategoryId,
        }).ToListAsync();

        public async Task<IEnumerable<SubCategoryCategorySeedDto>> GetSubCategoryCAtegorySeedData()
        {
            return await FindAll(false).Select(p => new SubCategoryCategorySeedDto()
            {
                SubCategoryCategoryId = p.SubCategoryCategoryId,
                SubCategoryCategoryName = p.SubCategoryCategoryName,
                SubCategoryId = p.SubCategoryId

            }).ToListAsync();
        }
 
    }
}
