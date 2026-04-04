using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ITradeImageRepo
    {


        Task<IEnumerable<TradeImageRefDto?>> GetAllTradeImages();
        Task<TradeImage?> FindTradeImageByTradeId(Guid imageId, Guid tradeId, bool tracking);
        Task<TradeImage?> FindTradeImageById(Guid imageId, bool tracking);
        Task<TradeImage?> GetTradePrimaryImage(Guid tradeId, bool tracking);

        void CreateTradeImage(TradeImage image);
        void UpdateTradeImage(TradeImage image);
        void DeleteTradeImage(TradeImage image);
    }
}
