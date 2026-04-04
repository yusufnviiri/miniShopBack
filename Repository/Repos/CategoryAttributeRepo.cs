using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class CategoryAttributeRepo:RepositoryBase<CategoryAttribute>,ICategoryAttributeRepo
    {
        public CategoryAttributeRepo(ApplicationDbContext dbContext):base(dbContext) 
        {

        }

        public async Task<IEnumerable<CategoryAttributeDto>> GetAllCategoryAttributes()
        {
            return await FindAll(trackChanges: false)
                .Select(ca => new CategoryAttributeDto
                {
                    CategoryAttributeId = ca.CategoryAttributeId,
                    CategoryId = ca.CategoryId,
                    AttributeDataTypeId = ca.AttributeDataTypeId,
                    AttributeName = ca.AttributeName,                
                    InputTypeValue = AttributeInputTypeMapper.ResolveStringValue(ca.AttributeDataType!.DataTypeName)

                })
                .ToListAsync();

        }

        public async Task<IReadOnlyList<CategoryAttributeDto>> FindCategoryCategoryAttributes(
      int categoryId,
      CancellationToken cancellationToken = default)
        {
            return await FindByCondition(ca => ca.CategoryId == categoryId, trackChanges: false)
                .Select(ca => new CategoryAttributeDto
                {
                    CategoryAttributeId = ca.CategoryAttributeId,
                    CategoryId = ca.CategoryId,
                    AttributeDataTypeId = ca.AttributeDataTypeId,
                    AttributeName = ca.AttributeName,

                    InputType= AttributeInputTypeMapper.Resolve(ca.AttributeDataType!.DataTypeName),
                    InputTypeValue = AttributeInputTypeMapper.ResolveStringValue(ca.AttributeDataType!.DataTypeName)
                })
                .ToListAsync(cancellationToken);
        }

        public async  Task<CategoryAttribute?> FindCategoryAttribute(int CategoryAttributeId, bool tracking)
        {
            return await FindByCondition(ca => ca.CategoryAttributeId == CategoryAttributeId, tracking).Include(k=>k.AttributeDataType)
                .FirstOrDefaultAsync();
        }
        public void CreateCategoryAttribute(CategoryAttribute categoryAttribute)=>CreateBase(categoryAttribute);
        public void UpdateCategoryAttribute(CategoryAttribute attribute)=>UpdateBase(attribute);
        public void DeleteCategoryAttribute(CategoryAttribute attribute) =>DeleteBase(attribute);
        public  async Task<CategoryAttributeDto?> FindCategoryAttributerRef(int CategoryAttributeId, bool tracking)
        {
            return await FindByCondition(ca => ca.CategoryAttributeId == CategoryAttributeId, trackChanges: false)
               .Select(ca => new CategoryAttributeDto
               {
                   CategoryAttributeId = ca.CategoryAttributeId,
                   CategoryId = ca.CategoryId,
                   AttributeDataTypeId = ca.AttributeDataTypeId,
                   AttributeName = ca.AttributeName,

                   InputType = AttributeInputTypeMapper.Resolve(ca.AttributeDataType!.DataTypeName),
                   InputTypeValue = AttributeInputTypeMapper.ResolveStringValue(ca.AttributeDataType!.DataTypeName)
               }).FirstOrDefaultAsync();
        }


    }
}
