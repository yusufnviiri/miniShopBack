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
    public class TradeAttributeValueRepo : RepositoryBase<TradeAttributeValue>, ITradeAttributeValueRepo
    {
        public TradeAttributeValueRepo(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<TradeAttributeValueDto>> GetAllTradeAttributeValues()
        {
            return await FindAll(false).Select(pv => new TradeAttributeValueDto()
            {
                TradeId = pv.TradeId,
                TradeAttributeValueId = pv.TradeAttributeValueId,
                CategoryAttributeId = pv.CategoryAttributeId,
                StringValue = pv.StringValue,
                IntValue = pv.IntValue,
                DecimalValue = pv.DecimalValue,
                BoolValue = pv.BoolValue,
                DateOnlyValue = pv.DateValue,

            }).ToListAsync();
        }
        public async Task<TradeAttributeValue?> FindATradeAttributeValue(int tradeAttributeValueId, bool tracking)
        {
            return await FindByCondition(pv => pv.TradeAttributeValueId == tradeAttributeValueId, tracking).FirstOrDefaultAsync();
        }
        public void CreateTradeAttributeValue(TradeAttributeValue attributeValue) => CreateBase(attributeValue);
        public void UpdateTradeAttributeValue(TradeAttributeValue attributeValue) => UpdateBase(attributeValue);
        public void DeleteTradeAttributeValue(TradeAttributeValue attributeValue) => DeleteBase(attributeValue);
    }

}

