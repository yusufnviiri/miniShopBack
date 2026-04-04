using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IApplicationUserService
    {

        Task<IEnumerable<ShowApplicationUserDto>> GetAllApplicationUsersAsync();
        Task<ShowApplicationUserDto?> FindApplicationUserByIdAsync(bool tracking, string userId);
        Task<ApplicationUser?> FindApplicationUserForUpdateAsync(string userId);
        Task CreateApplicationUserAsync(ApplicationUser user);
        Task UpdateApplicationUserAsync(ApplicationUser user);
        Task DeleteApplicationUserAsync(ApplicationUser user);
    }
}
