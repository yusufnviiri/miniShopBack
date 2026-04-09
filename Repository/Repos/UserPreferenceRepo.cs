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
    public class UserPreferenceRepo : RepositoryBase<UserPreference>, IUserPreferenceRepo
    {
        public UserPreferenceRepo(ApplicationDbContext _db) : base(_db)
        {

        }

       public async Task<IEnumerable<UserPreference>> GetAllUserPreferences()=>await FindAll(false).ToListAsync();
        public IQueryable<UserPreference> HomeUserPreferencesQueryData()=>FindAll(false);
        public async Task<UserPreference?> FindUserPreferenceById(Guid userPreferenceId, bool tracking)
        {
            var userPreference = await FindByCondition(p => p.UserPreferenceId == userPreferenceId, tracking).FirstOrDefaultAsync();
            return userPreference;

        }
        public async Task<UserPreference?> FindUserPreferenceForUpdate(Guid userPreferenceId)
        {
            var userPreference = await FindByCondition(p => p.UserPreferenceId == userPreferenceId, true).FirstOrDefaultAsync();
            return userPreference;
        }
        public void CreateUserPreference(UserPreference userPreference)=> CreateBase(userPreference);
        public void UpdateUserPreference(UserPreference userPreference)=>UpdateBase(userPreference);
        public void DeleteUserPreference(UserPreference userPreference)=>DeleteBase(userPreference);
    }
}
