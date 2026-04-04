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
 public class ApplicationUserRepo : RepositoryBase<ApplicationUser>, IApplicationUserRepo
    {
        public ApplicationUserRepo(ApplicationDbContext _db) : base(_db)
        {

        }

     public async Task<IEnumerable<ShowApplicationUserDto>> GetAllApplicationUsers()
        {
            return await FindAll(false)
                .Select(u => new ShowApplicationUserDto
                {
                    IdentityUserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                })
                .ToListAsync();
        }
        public async Task<ShowApplicationUserDto?> FindApplicationUserById(bool tracking, string userId)
        {
            var user = await FindByCondition(p => p.Id == userId.ToString(), tracking)
                .Select(u => new ShowApplicationUserDto
                {
                    IdentityUserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                })
                .FirstOrDefaultAsync();
            return user;
        }
        public Task<ApplicationUser?> FindApplicationUserForUpdate(string userId)
        {
            return FindByCondition(p=>p.Id==userId,true).FirstOrDefaultAsync();
        }
        public void CreateApplicationUser(ApplicationUser user)=>CreateBase(user);
        public void UpdateApplicationUser(ApplicationUser user)=>UpdateBase(user);
        public void DeleteApplicationUser(ApplicationUser user)=>DeleteBase(user);
    }
}