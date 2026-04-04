using Contracts.Service;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ActiveUserProfileService : IActiveUserProfileService
    {
        private readonly ApplicationDbContext _dbContext;

        public ActiveUserProfileService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserGroup> GetGroupAsync(Guid groupId)
        {
            return await _dbContext.UserGroups
                .FirstOrDefaultAsync(ug => ug.UserGroupId == groupId)
                ?? throw new Exception("Group not found");
        }
        public async Task<UserProfile> GetActiveUserProfileAsync(string identityId)
        {
            return await _dbContext.UserProfiles
                .FirstOrDefaultAsync(ug => ug.IdentityUserId == identityId)
                ?? throw new Exception("User not found");
        }
    }
}
