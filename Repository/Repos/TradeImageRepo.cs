using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class TradeImageRepo : RepositoryBase<TradeImage>, ITradeImageRepo
    {
        public TradeImageRepo(ApplicationDbContext _db) : base(_db)
        {

        }

        public async Task<IEnumerable<TradeImageRefDto?>> GetAllTradeImages()
        {
            {
                {
                    return await FindAll(false).Select(k => new TradeImageRefDto()
                    {
                        TradeId = (Guid)k.TradeId,
                        TradeImageId = k.TradeImageId,
                        IsPrimary = k.IsPrimary,


                    }).ToListAsync();
                }
            }
        }

        public async Task<TradeImage?> FindTradeImageByTradeId(Guid imageId, Guid tradeId, bool tracking)
        {
            return await FindByCondition(p => p.TradeImageId == imageId && p.TradeId == tradeId, tracking).FirstOrDefaultAsync();
        }

        public async Task<TradeImage?> FindTradeImageById(Guid imageId, bool tracking)
        {
            return await FindByCondition(p => p.TradeImageId == imageId, tracking).FirstOrDefaultAsync();
        }
        public async Task<TradeImage?> GetTradePrimaryImage(Guid tradeId, bool tracking)

        {
            return await FindByCondition(p => p.IsPrimary == true&&p.TradeId==tradeId , tracking).FirstOrDefaultAsync();
        }

       
        public void CreateTradeImage(TradeImage image) => CreateBase(image);
        public void UpdateTradeImage(TradeImage image) => UpdateBase(image);
        public void DeleteTradeImage(TradeImage image) => DeleteBase(image);
   
     

    }
}
    
