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
    public class BuyerProfileRepo : RepositoryBase<BuyerProfile>, IBuyerProfileRepo
    {
        public BuyerProfileRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

      public async Task<IEnumerable<BuyerProfileDto>> GetAllBuyerProfiles()
        {
            var buyerProfileDtos = await FindAll(false).Select(bp => new BuyerProfileDto
            {
                BuyerProfileId = bp.BuyerProfileId,
                BuyerId = bp.BuyerId,
                PurchaseLimit = bp.PurchaseLimit,
                BuyerTierId = bp.BuyerTierId,
                BuyerTier = bp.BuyerTier,
                BuyerTypeId = bp.BuyerTypeId,
                BuyerType = bp.BuyerType
            }).ToListAsync();
            return buyerProfileDtos;

        }
      public async Task<BuyerProfile?> FindBuyerProfileById(Guid buyerProfileId, bool tracking)
        {
            return await FindByCondition(bp => bp.BuyerProfileId == buyerProfileId, tracking)
                .FirstOrDefaultAsync();
        }
        public void CreateBuyerProfile(BuyerProfile buyerProfile)=>CreateBase(buyerProfile);
        public void UpdateBuyerProfile(BuyerProfile profile)=>UpdateBase(profile);
        public void DeleteBuyerProfile(BuyerProfile buyerProfile)=>DeleteBase(buyerProfile);
    }
}
