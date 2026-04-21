using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ITradeImpressionRepo
    {

        Task<IEnumerable<TradeImpression>> GetAllTradeImpressions();
        Task<TradeImpression?> FindTradeImpressionById(Guid tradeImpressionId, bool tracking);
        Task<TradeImpression?> FindTradeImpressionByTradeId(Guid tradeId, bool tracking);
        Task<TradeImpression?> FindTradeImpressionForUpdate(Guid tradeImpressionId);
        IQueryable<TradeImpression> TradeImpressionsQueryData();

        void CreateTradeImpression(TradeImpression impression);
        void UpdateTradeImpression(TradeImpression impression);
        void DeleteTradeImpression(TradeImpression  tradeImpression);
    }
}
