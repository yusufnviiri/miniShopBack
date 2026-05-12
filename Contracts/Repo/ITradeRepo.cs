using Entities.Models;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ITradeRepo
    {

        Task<PagedList<HomePageTradeDto>> GetHomePageTrades(ProductRequestParameters requestParameters);
        Task<TradeDataDto?> GetTradeData(Guid tradeId);
        Task<ShowTradeDataDto?> FindSellerTrade(Guid TradeId);
        Task MakeTradeFeautured(Guid tradeId);
        Task<int> NumberOfTrades();
        Task MakeAllTradesFeautured();


        Task<Trade?> FindTradeForUpdate(Guid tradeId);
        Guid CreateTrade(Trade trade);
        void UpdateTrade(Trade trade);
        void DeleteTrade(Trade trade);
        Task<ICollection<SellerTradeListDto>> GetGroupMembersTrades(IList<Guid> groupMemberIds, Guid groupId);
        Task<Guid> GetTradeIdBySlugName(string slug);












        Task<ShowTradeDataDto?> FindTradeBySlugName(bool tracking, string slugName);

        Task<TradeDataDto?> GetTradeDataUsingSlugName(string slugName);
        Task<ShowTradeDataDto?> FindSellerTradeUsingSlugName(bool tracking, string slugName);
      

        


















    }
}
