using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ITradeImpressionService
    {

        Task<IEnumerable<TradeImpression>> GetAllTradeImpressionsAsync();
        Task<TradeImpression?> FindTradeImpressionByIdAsync(Guid tradeImpressionId, bool tracking);
        Task<TradeImpression?> FindTradeImpressionByTradeIdAsync(Guid tradeId, bool tracking);
        Task CreateTradeImpressionAsync(NewImpressionDto impressionDto);
        Task UpdateTradeImpressionAsync(TradeImpression impression);
        Task DeleteTradeImpressionAsync(Guid tradeImpressionId);
    }
}
