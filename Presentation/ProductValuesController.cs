using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace Presentation
    {
        [Route("api/productvalues")]
        [ApiController]
        public class ProductValuesController : ControllerBase
        {

            private readonly IServiceManager _service;
            //private readonly IActiveUserContext _context;

            public ProductValuesController(IServiceManager service)
            {
                _service = service;
                //_context = context;
            }

        [Authorize]


        [HttpGet("categories")]
        public async Task<ActionResult> GetProductCategories()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("productcategorydata")]
        public async Task<ActionResult> ProductCategories()
        {
            var categories = await _service.CategoryService.ProductCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("tradecategorydata")]
        public async Task<ActionResult> TradeCategories()
        {
            var categories = await _service.CategoryService.TradeCategoriesAsync();
            return Ok(categories);
        }
        [Authorize]

        [HttpGet("tradecategories")]
        public async Task<ActionResult> GetTradeCategories()
        {
            var categories = await _service.CategoryService.GetAllTradeCategoriesAsync();
            return Ok(categories);
        }
        [Authorize]

        [HttpGet("categories/{id}")]

        public async Task<ActionResult> GetProductCategory(int id)
        {
            var category = await _service.CategoryService.FindCategoryDtoByIdAsync(id, false);
            return Ok(category);
        }
        [Authorize]

        [HttpPost("categories/new")]
        public async Task<ActionResult> CreateProductCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto is null) { return BadRequest("Object is null"); }
            await _service.CategoryService.CreateCategoryAsync(categoryDto);
            return Ok(new { message = "Category created" });
        }
        [Authorize]

        [HttpGet("categories/{categoryId}/subcategories")]
        public async Task<ActionResult> GetProductCategoryWithSubCategories([FromRoute] int categoryId)
        {
            if (categoryId < 1) { return BadRequest("Object is null"); }
            var category = await _service.CategoryService.FindCategoryWithSubCategoriesAsync(categoryId);
            return Ok(category);
        }
        [Authorize]


        [HttpPost("category/subcategory/new")]
        public async Task<ActionResult> CreateSubCategory(SubCategoryDto subCategory)
        {
            if (subCategory is null) { return BadRequest("Object is null"); }
            await _service.SubCategoryService.CreateSubCategoryAsync(subCategory);
            return Ok(new { message = "Category created" });
        }
        [Authorize]

        [HttpGet("subCategories/{id}")]
        public async Task<ActionResult> GetProductSubCategory(int id)
        {
            var category = await _service.SubCategoryService.FindSubCategoryByIdAsync(id, false);
            return Ok(category);
        }
        [Authorize]

        [HttpPost("subcategory/subcategorycategory/new")]
        public async Task<ActionResult> CreateSubCategoryCategory(SubCategoryCategory subCategoryCategory)
        {
            if (subCategoryCategory is null) { return BadRequest("Object is null"); }
            await _service.SubCategoryCategoryService.CreateSubCategoryCategoryAsync(subCategoryCategory);
            return Ok(new { message = "Category created" });
        }
        [Authorize]

        [HttpGet("categories/subcategory/{subcategoryId}")]
        public async Task<ActionResult> GetProductCategorySubCategoriesWithCategories([FromRoute] int subcategoryId)
        {
            if (subcategoryId < 1) { return BadRequest("Object is null"); }
            var subcategory = await _service.SubCategoryService.FindSubCategoryByIdWithSubCategoriesAsync(subcategoryId);
            return Ok(subcategory);
        }
        [Authorize]


        [HttpGet("subCategories/Categories/{id}")]
        public async Task<ActionResult> GetProductSubCategoriesCategory(int id)
        {
            var category = await _service.SubCategoryCategoryService.FindSubCategoryCategoryByIdAsync(id, false);
            return Ok(category);
        }
        [Authorize]

        [HttpDelete("category/{id}")]
        public async Task<ActionResult> DeleteProductCategory([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(new { message = "Id is zero" }); }
            await _service.CategoryService.DeleteCategoryAsync(id);
            return Ok(new { message = "Category Deleted" });
        }
        [Authorize]


        [HttpDelete("subCategory/{id}")]
        public async Task<ActionResult> DeleteProductSubCategory([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(new { message = "Id is zero" }); }
            await _service.SubCategoryService.DeleteSubCategory(id);
            return Ok(new { message = "Category Deleted" });
        }
        [Authorize]


        [HttpDelete("subCategoryCategory/{id}")]
        public async Task<ActionResult> DeleteCategorySubCategory([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(new { message = "Id is zero" }); }
            await _service.SubCategoryCategoryService.DeleteSubCategoryCategoryAsync(id);
            return Ok(new { message = "Category Deleted" });
        }
        [Authorize]

        [HttpPut("category/edit")]
        public async Task<ActionResult> UpdateProductCategory(CategoryDto category)
        {
            if (category is null) { return BadRequest( new { message = "Object is null" }); }
            await _service.CategoryService.UpdateCategoryAsync(category);
            return Ok(new { message = "Update Category Successful" });
        }
        [Authorize]

        [HttpPut("subCategory/edit")]
        public async Task<ActionResult> UpdateSubCategory(SubCategory category)
        {
            if (category is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.SubCategoryService.UpdateSubCategoryAsync(category);
            return Ok(new { message = "Update Category Successful" });
        }
        [Authorize]

        [HttpPut("subCategoryCategory/edit")]
        public async Task<ActionResult> UpdateSubCategoryCategory(SubCategoryCategory category)
        {
            if (category is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.SubCategoryCategoryService.UpdateSubCategoryCategoryAsync(category);
            return Ok(new { message = "Update Category Successful" });
        }
        // real stuff here


        [HttpGet("categoryRef")]
        public async Task<ActionResult> GetProductCategoryReferences()
        {
            var categories = await _service.CategoryService.GetAllCategoryReferencesAsync();
            return Ok(categories);
        }
        [HttpGet("subcategoryRef/{categoryId}")]
        public async Task<ActionResult> GetCategorySubCategoryReferences([FromRoute] int categoryId)
        {
            var categories = await _service.SubCategoryService.FindCategorySubCategoryRefByIdAsync(categoryId);
            return Ok(categories);
        }
        [HttpGet("subcategorycategoryRef/{categoryId}")]
        public async Task<ActionResult> GetSubCategoryCategoryReferences([FromRoute] int categoryId)
        {
            var categories = await _service.SubCategoryCategoryService.FindSubCategoryCategoryRefByIdAsync(categoryId);
            return Ok(categories);
        }



        [Authorize]


        [HttpPost("categoryattribute/new")]
        public async Task<ActionResult> CreateCategoryAttribute([FromBody] CategoryAttribute attribute )
        {
            if (attribute is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.CategoryAttributeService.CreateCategoryAttributeAsync(attribute);
            return Ok(new { message = "Category attribute created" });
        }
        [Authorize]


        [HttpPut("categoryattribute/update")]
        public async Task<ActionResult> UpdateCategoryAttribute([FromBody] CategoryAttribute attribute)
        {
            if (attribute is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.CategoryAttributeService.UpdateCategoryAttributeAsync(attribute);
            return Ok(new { message = "Category attribute updated" });
        }
        [HttpGet("categoryattributes")]
        public async Task<ActionResult> GetCategoryAttributes()
        {
            var attributes = await _service.CategoryAttributeService.GetAllCategoryAttributesAsync();
            return Ok(attributes);
        }
        [HttpGet("categoryattributes/{id}")]
        public async Task<ActionResult> GetCategoryAttribute([FromRoute] int id)
        {
            var attribute = await _service.CategoryAttributeService.FindCategoryAttributeAsync(id,false);
            return Ok(attribute);
        }

        [HttpGet("categorycategoryattributes/{id}")]
        public async Task<ActionResult> GetCategoryCategoryAttribute([FromRoute] int id)
        {
            var attributes = await _service.CategoryAttributeService.FindCategoryCategoryAttributesAsync(id);
            return Ok(attributes);
        }
        [Authorize]

        [HttpDelete("categoryattribute/{id}")]
        public async Task<ActionResult> DeleteCategoryAttribute([FromRoute] int id)
        {
            await _service.CategoryAttributeService.DeleteCategoryAttributeAsync(id);
            return Ok(new { message = "Category attribute deleted" });
        }
        [Authorize]


        [HttpPost("attributevalues/new")]
        public async Task<ActionResult> CreateCategoryAttributeValue([FromBody] ProductAttributeValue attributeValue )
        {
            if (attributeValue is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.ProductAttributeValueService.CreateProductAttributeValueAsync(attributeValue);
            return Ok(new { message = "Category attribute value created" });
        }
        [HttpGet("attributevalues")]
        public async Task<ActionResult> GetCategoryAttributeValues()
        {
            var attributesValues = await _service.ProductAttributeValueService.GetAllProductAttributeValuesAsync();
            return Ok(attributesValues);
        }
        [HttpGet("attributevalues/{id}")]
        public async Task<ActionResult> GetCategoryAttributeValues([FromRoute] int id)
        {
            var attribute = await _service.ProductAttributeValueService.FindAProductAttributeValueAsync(id, false);
            return Ok(attribute);
        }
        [Authorize]

        [HttpDelete("categoryattributevalue/{id}")]
        public async Task<ActionResult> DeleteCategoryAttributeValue([FromRoute] int id)
        {
            await _service.ProductAttributeValueService.DeleteProductAttributeValueAsync(id);
            return Ok(new { message = "Trade attribute value deleted" });
        }


        [Authorize]

        [HttpPost("tradeattributevalues/new")]
        public async Task<ActionResult> CreateTradeAttributeValue([FromBody] TradeAttributeValue attributeValue)
        {
            if (attributeValue is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.TradeAttributeValueService.CreateTradeAttributeValueAsync(attributeValue);
            return Ok(new { message = "Trade attribute value created" });
        }

        [HttpGet("tradeattributevalues")]
        public async Task<ActionResult> GetTradeAttributeValues()
        {
            var attributesValues = await _service.TradeAttributeValueService.GetAllTradeAttributeValuesAsync();
            return Ok(attributesValues);
        }
        [HttpGet("tradeattributevalues/{id}")]
        public async Task<ActionResult> GetTradeAttributeValues([FromRoute] int id)
        {
            var attribute = await _service.TradeAttributeValueService.FindATradeAttributeValueAsync(id, false);
            return Ok(attribute);
        }
        [HttpDelete("tradeattributevalue/{id}")]
        public async Task<ActionResult> DeleteTradeAttributeValue([FromRoute] int id)
        {
            await _service.TradeAttributeValueService.DeleteTradeAttributeValueAsync(id);
            return Ok(new { message = "trade attribute value deleted" });
        }




        [HttpGet("generalcategorieswithref")]
        public async Task<ActionResult> GetAllReferencedGeneralCategories()
        {
            var categories = await _service.GeneralCategoryService.GetReferencedGeneralCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("productgeneralcategorieswithref")]
        public async Task<ActionResult> GetProductGeneralCategories()
        {
            var categories = await _service.GeneralCategoryService.GetReferencedProductGeneralCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("tradegeneralcategorieswithref")]
        public async Task<ActionResult> GetTradeGeneralCategories()
        {
            var categories = await _service.GeneralCategoryService.GetReferencedTradeGeneralCategoriesAsync();
            return Ok(categories);
        }
        [Authorize]

        [HttpGet("allgeneralcategories")]
        public async Task<ActionResult> GetAllProductGeneralCategories()
        {
            var categories = await _service.GeneralCategoryService.GetAllGeneralCategoriesWithCategoriesAsync(false);
            return Ok(categories);
        }
        [HttpGet("allcategoriesref")]
        public async Task<ActionResult> GetAllCategoriesInDb()
        {
            var categories = await _service.CategoryService.GetAllCategoriesRefInDbAsync();
            return Ok(categories);
        }

        [HttpGet("generalcategories/{id}")]

        public async Task<ActionResult> GetProductGeneralCategory(int id)
        {
            var category = await _service.GeneralCategoryService.FindGeneralCategoryByIdAsync(id, false);
            return Ok(category);
        }
        [HttpPost("generalcategories/new")]
        public async Task<ActionResult> CreateProductGeneralCategory([FromBody] GeneralCategory category)
        {
            if (category is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.GeneralCategoryService.CreateGeneralCategoryAsync(category);
            return Ok(new { message = "Category created" });
        }
        [HttpPut("generalcategories/update")]
        public async Task<ActionResult> UpdateProductGeneralCategory([FromBody] GeneralCategoryUpdateDto dto )
        {
            if (dto is null) { return BadRequest(new { message = "Object is null" }); }
            await _service.GeneralCategoryService.AddCategoryToGeneralCategoryAsync(dto);
            return Ok(new { message = "Category updated" });
        }

        [HttpGet("generalcategories/{categoryId}/categories")]
        public async Task<ActionResult> GetGeneralCategoryWithCategories([FromRoute] int categoryId)
        {
            if (categoryId < 1) { return BadRequest("Object is null"); }
            var category = await _service.GeneralCategoryService.FindCategoryWithSubCategoriesAsync(categoryId);
            return Ok(category);
        }





        [HttpGet("seeddata")]
        public async Task<ActionResult> GetCategorySeedDataAsync()
        {
            var categories = await _service.CategoryService.GetAllCategorySeedDataAsync();
            return Ok(categories);
        }


    }
}
