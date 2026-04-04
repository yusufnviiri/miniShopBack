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
    public class SellerRestrictionRepo: RepositoryBase<SellerRestriction>, ISellerRestrictionRepo
    {
        public SellerRestrictionRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

      public async  Task<IEnumerable<SellerRestrictionDto>> GetAllSellerRestrictions()=> await FindAll(false)
            .Select(r => new SellerRestrictionDto
            {
               SellerRestrictionId = r.SellerRestrictionId,
                SellerProfileId = r.SellerProfileId,
                ExpiresAt = r.ExpiresAt,
                Reason = r.Reason
            }).ToListAsync();
      public async  Task<SellerRestriction?> FindSellerRestrictionById(Guid restrictionId, bool tracking)=>
            await FindByCondition(r => r.SellerRestrictionId == restrictionId, tracking)
            .FirstOrDefaultAsync();
       public void CreateSellerRestriction(SellerRestriction restriction)=>CreateBase(restriction);
       public  void UpdateSellerRestriction(SellerRestriction restriction)=>UpdateBase(restriction);
      public  void DeleteSellerRestriction(SellerRestriction restriction)=>DeleteBase(restriction);

    }
}
