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
 public class TradeImpressionRepo : RepositoryBase<TradeImpression>, ITradeImpressionRepo
    {
        public TradeImpressionRepo(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<IEnumerable<TradeImpression>> GetAllTradeImpressions()=>await FindAll(false).ToListAsync();
        public async  Task<TradeImpression?> FindTradeImpressionById(Guid tradeImpressionId, bool tracking) => await FindByCondition(p => p.TradeImpressionId == tradeImpressionId, tracking).FirstOrDefaultAsync();
        public async Task<TradeImpression?> FindTradeImpressionByTradeId(Guid tradeId, bool tracking) => await FindByCondition(p => p.TradeImpressionId == tradeId, tracking).FirstOrDefaultAsync();
        public async Task<TradeImpression?> FindTradeImpressionForUpdate(Guid tradeImpressionId) => await FindByCondition(p => p.TradeImpressionId == tradeImpressionId, true).FirstOrDefaultAsync();
        public IQueryable<TradeImpression> TradeImpressionsQueryData()=>FindAll(false);
        public void CreateTradeImpression(TradeImpression impression) => CreateBase(impression);
        public void UpdateTradeImpression(TradeImpression impression) => UpdateBase(impression);
        public void DeleteTradeImpression(TradeImpression impression) => DeleteBase(impression);
    }
}
