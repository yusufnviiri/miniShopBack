using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public  interface ITradeAttributeValueService
    {

        Task<IEnumerable<TradeAttributeValueDto>> GetAllTradeAttributeValuesAsync();
        Task<TradeAttributeValue?> FindATradeAttributeValueAsync(int tradeAttributeValueId, bool tracking);
        Task CreateTradeAttributeValueAsync(TradeAttributeValue attributeValue);
        Task UpdateTradeAttributeValueAsync(TradeAttributeValue attributeValue);
        Task DeleteTradeAttributeValueAsync(int attributeValue);

        Task<TradeAttributeValue?> MapAttributeValueToCategoryAttribute(int categoryAttributeId, int? intValue = null, DateOnly? dateOnlyValue = null, string? stringValue = null, bool? boolValue = false);
    }
}
