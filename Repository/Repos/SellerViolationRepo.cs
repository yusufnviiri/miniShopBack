using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class SellerViolationRepo:RepositoryBase<SellerViolation>, ISellerViolationRepo
    {
        public SellerViolationRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

       public async Task<IEnumerable<SellerViolation>> GetAllSellerViolations()
        {
            return await FindAll(false).ToListAsync();
        }
        public async Task<SellerViolation?> FindSellerViolationById(Guid violationId, bool tracking)
        {
            return await FindByCondition(sv => sv.SellerViolationId == violationId, tracking)
                .FirstOrDefaultAsync();
        }
       public  void CreateSellerViolation(SellerViolation violation)=> CreateBase(violation);
      public  void UpdateSellerViolation(SellerViolation violation)=> UpdateBase(violation);
     public  void DeleteSellerViolation(SellerViolation violation)=> DeleteBase(violation);
    }
}
