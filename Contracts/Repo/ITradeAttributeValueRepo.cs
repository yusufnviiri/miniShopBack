using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ITradeAttributeValueRepo
    {

        Task<IEnumerable<TradeAttributeValueDto>> GetAllTradeAttributeValues();
        Task<TradeAttributeValue?> FindATradeAttributeValue(int tradeAttributeValueId, bool tracking);
        void CreateTradeAttributeValue(TradeAttributeValue attributeValue);
        void UpdateTradeAttributeValue(TradeAttributeValue attributeValue);
        void DeleteTradeAttributeValue(TradeAttributeValue attributeValue);
    }
}
