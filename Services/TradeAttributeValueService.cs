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
      internal sealed class TradeAttributeValueService : ITradeAttributeValueService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;

        public TradeAttributeValueService(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repoManager = repository;
        }

        public async Task<IEnumerable<TradeAttributeValueDto>> GetAllTradeAttributeValuesAsync() => await _repoManager.TradeAttributeValueRepo.GetAllTradeAttributeValues();
        public async Task<TradeAttributeValue?> FindATradeAttributeValueAsync(int tradeAttributeValueId, bool tracking) => await _repoManager.TradeAttributeValueRepo.FindATradeAttributeValue(tradeAttributeValueId, tracking);
        public async Task CreateTradeAttributeValueAsync(TradeAttributeValue attributeValue)
        {
            _repoManager.TradeAttributeValueRepo.CreateTradeAttributeValue(attributeValue);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateTradeAttributeValueAsync(TradeAttributeValue attributeValue)

        {
            var existingValue = await _repoManager.TradeAttributeValueRepo.FindATradeAttributeValue(attributeValue.TradeAttributeValueId, true);
            if (existingValue is null)
            {
                _logger.LogError($" Attribute Value with id: {attributeValue.TradeAttributeValueId} not found.");
                throw new ObjectBadRequestExeption($" Attribute Value with id: {attributeValue.TradeAttributeValueId} not found.");
            }

            existingValue.IntValue = attributeValue.IntValue;
            existingValue.StringValue = attributeValue.StringValue;
            existingValue.BoolValue = attributeValue.BoolValue;
            existingValue.DecimalValue = attributeValue.DecimalValue;
            existingValue.TradeId = attributeValue.TradeId;
            existingValue.CategoryAttributeId = attributeValue.CategoryAttributeId;

            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteTradeAttributeValueAsync(int attributeValueId)
        {
            var existingValue = await _repoManager.TradeAttributeValueRepo.FindATradeAttributeValue(attributeValueId, true);
            if (existingValue is null)
            {
                _logger.LogError($" Attribute Value with id: {attributeValueId} not found.");
                throw new ObjectBadRequestExeption($" Attribute Value with id: {attributeValueId} not found.");
            }
            _repoManager.TradeAttributeValueRepo.DeleteTradeAttributeValue(existingValue);
            await _repoManager.SaveRepoDataAsync();
        }

        public async Task<TradeAttributeValue?> MapAttributeValueToCategoryAttribute(int categoryAttributeId, int? intValue = null, DateOnly? dateOnlyValue = null, string? stringValue = null, bool? boolValue = false)
        {
            var categoryAttribute = await _repoManager.CategoryAttributeRepo.FindCategoryAttribute(categoryAttributeId, false);
            if (categoryAttribute is null)
            {
                throw new ObjectBadRequestExeption($" Attribute Value with id: {categoryAttributeId} not found.");

            }
            else
            {
                var attributeDataType = categoryAttribute.AttributeDataType.DataTypeName;
                var attributeValue = new TradeAttributeValue
                {
                    CategoryAttributeId = categoryAttributeId
                }; switch (attributeDataType)
                {
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

     

    }

}

