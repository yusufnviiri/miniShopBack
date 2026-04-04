using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IActiveUserProfileService
    {
        Task<UserGroup> GetGroupAsync(Guid groupId);
        Task<UserProfile> GetActiveUserProfileAsync(string identityId);


    }
}
