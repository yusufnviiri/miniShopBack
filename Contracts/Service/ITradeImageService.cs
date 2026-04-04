using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ITradeImageService
    {
        Task<IEnumerable<TradeImageRefDto?>> GetAllTradeImagesAsync();
        Task<TradeImage?> FindTradeImageByTradeIdAsync(Guid imageId, Guid tradeId, bool tracking);
        Task<TradeImage?> GetTradePrimaryImage(Guid tradeId, bool tracking);

        Task CreateTradeImageAsync(TradeImageDto image);

        Task UpdateTradeImageAsync(TradeImageDto imageDto);
        Task DeleteTradeImageAsync(Guid imageId);

        Task DeleteTradeImageIdRefAsync(Guid imageId, Guid tradeId);
        Task MakeImagePrimaryAsync(MiniTradeImage tradeImage);
        Task CreateTradeImageListAsync(ICollection<TradeImage> images);

    }
}
