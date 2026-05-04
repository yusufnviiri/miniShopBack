using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ITradeService
    {


        Task<(ICollection<HomePageTradeDto> tradersData, MetaData MetaData)> GetHomePageTradesAsync(ProductRequestParameters requestParameters);
        Task<TradeDataDto?> GetTradeDataAsync(Guid tradeId);
        Task<ShowTradeDataDto?> FindSellerTradeAsync(Guid TradeId);
        Task<Trade?> FindTradeForUpdateAsync(Guid tradeId);
        Task<Trade> CreateTradeAsync(NewTradeDto tradeDto);
        Task UpdateTradeAsync(Trade trade);
        Task DeleteTradeAsync(Guid tradeId);
        void MakeTradeFeautured(Guid tradeId);
        Task UpdateTradeDescription(SharedUpdatesDto sharedUpdates);
        void MakeAllTradesFeautured();


        Task<ShowTradeDataDto?> FindTradeBySlugNameAsync(bool tracking, string slugName);

        Task<TradeDataDto?> GetTradeDataUsingSlugNameAsync(string slugName);
        Task<ShowTradeDataDto?> FindSellerTradeUsingSlugNameAsync(bool tracking, string slugName);
    }
}
