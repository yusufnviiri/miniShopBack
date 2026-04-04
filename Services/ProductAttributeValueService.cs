using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class ProductAttributeValueService : IProductAttributeValueService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;

        public ProductAttributeValueService(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repoManager = repository;
        }

        public async Task<IEnumerable<ProductAttributeValueDto>> GetAllProductAttributeValuesAsync() => await _repoManager.ProductAttributeValueRepo.GetAllProductAttributeValues();
        public async Task<ProductAttributeValue?> FindAProductAttributeValueAsync(int productAttributeValueId, bool tracking) => await _repoManager.ProductAttributeValueRepo.FindAProductAttributeValue(productAttributeValueId, tracking);
        public async Task CreateProductAttributeValueAsync(ProductAttributeValue attributeValue)
        {
            _repoManager.ProductAttributeValueRepo.CreateProductAttributeValue(attributeValue);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateProductAttributeValueAsync(ProductAttributeValue attributeValue)
        {
            var existingValue = await _repoManager.ProductAttributeValueRepo.FindAProductAttributeValue(attributeValue.ProductAttributeValueId, true);
            if (existingValue is null)
            {
                _logger.LogError($" Attribute Value with id: {attributeValue.ProductAttributeValueId} not found.");
                throw new ObjectBadRequestExeption($" Attribute Value with id: {attributeValue.ProductAttributeValueId} not found.");
            }

            existingValue.IntValue = attributeValue.IntValue;
            existingValue.StringValue = attributeValue.StringValue;
            existingValue.BoolValue = attributeValue.BoolValue;
            existingValue.DecimalValue = attributeValue.DecimalValue;
            existingValue.ProductId = attributeValue.ProductId;
            existingValue.CategoryAttributeId = attributeValue.CategoryAttributeId;

            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteProductAttributeValueAsync(int attributeValueId)
        {
            var existingValue = await _repoManager.ProductAttributeValueRepo.FindAProductAttributeValue(attributeValueId, true);
            if (existingValue is null)
            {
                _logger.LogError($" Attribute Value with id: {attributeValueId} not found.");
                throw new ObjectBadRequestExeption($" Attribute Value with id: {attributeValueId} not found.");
            }
            _repoManager.ProductAttributeValueRepo.DeleteProductAttributeValue(existingValue);
            await _repoManager.SaveRepoDataAsync();
        }

        public async Task<ProductAttributeValue?> MapAttributeValueToCategoryAttribute(int categoryAttributeId, int? intValue = null, DateOnly? dateOnlyValue = null, string? stringValue = null, bool? boolValue = false)
        {
            var categoryAttribute = await _repoManager.CategoryAttributeRepo.FindCategoryAttribute(categoryAttributeId, false);
            if (categoryAttribute is null)
            {
                throw new ObjectBadRequestExeption($" Attribute Value with id: {categoryAttributeId} not found.");

            }
            else
            {
                var attributeDataType = categoryAttribute.AttributeDataType.DataTypeName;
                var attributeValue = new ProductAttributeValue
                {
                    CategoryAttributeId = categoryAttributeId
                }; switch (attributeDataType){
                    case "String": attributeValue.StringValue = stringValue; break;
                    case "Int": attributeValue.IntValue = intValue; break;
                    //case "Decimal": attributeValue.DecimalValue = (decimal?)intValue; break;
                    case "Decimal":
                        attributeValue.DecimalValue = intValue
                            ?? (intValue.HasValue ? (decimal?)intValue.Value : null);
                        break;
                    case "Bool": attributeValue.BoolValue = boolValue; break;
                    case "Date": attributeValue.DateValue = dateOnlyValue; break;
                    default:
                        throw new ObjectBadRequestExeption(
                            $"Unsupported attribute data type: {attributeDataType}"
                        );
                }
                return attributeValue;
            }

        }
      
        
        
        
        
    //    private static Exception InvalidType(AttributeInputType expected, object actual) =>
    //new ObjectBadRequestExeption(
    //    $"Invalid value type. Expected '{expected}', received '{actual.GetType().Name}'.");


    //    private static void AssignValue(  ProductAttributeValue target, AttributeInputType dataType,
    //object? value)
    //    {
    //        if (value is null)
    //            throw new ObjectBadRequestExeption("Attribute value cannot be null.");

    //        switch (dataType)
    //        {
    //            case AttributeInputType.String:
    //                target.StringValue = value as string
    //                    ?? throw InvalidType(dataType, value);
    //                break;

    //            case AttributeInputType.Int:
    //                target.IntValue = value as int?
    //                    ?? throw InvalidType(dataType, value);
    //                break;

    //            case AttributeInputType.Decimal:
    //                target.DecimalValue = value as decimal?
    //                    ?? throw InvalidType(dataType, value);
    //                break;

    //            case AttributeInputType.Bool:
    //                target.BoolValue = value as bool?
    //                    ?? throw InvalidType(dataType, value);
    //                break;

    //            case AttributeInputType.Date:
    //                target.DateValue = value as DateOnly?
    //                    ?? throw InvalidType(dataType, value);
    //                break;

    //            default:
    //                throw new NotSupportedException(
    //                    $"Unsupported AttributeDataType '{dataType}'.");
    //        }
    //    }

    //    public async Task<ProductAttributeValue> MapAttributeValueAsync( int categoryAttributeId,  object? value)
    //    {
    //        var categoryAttribute =
    //            await _repoManager.CategoryAttributeRepo.FindCategoryAttributerRef(categoryAttributeId,  false)
    //            ?? throw new ObjectBadRequestExeption(
    //                $"CategoryAttribute with id '{categoryAttributeId}' not found.");

    //        var attributeValue = new ProductAttributeValue
    //        {
    //            CategoryAttributeId = categoryAttributeId
    //        };

    //        AssignValue(attributeValue, categoryAttribute.InputType, value);

    //        return attributeValue;
    //    }


    }
}