using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IApplicationUserRepo
    {

        Task<IEnumerable<ShowApplicationUserDto>> GetAllApplicationUsers();
        Task<ShowApplicationUserDto?> FindApplicationUserById(bool tracking, string userId);
        Task<ApplicationUser?> FindApplicationUserForUpdate(string userId);
        void CreateApplicationUser(ApplicationUser user );
        void UpdateApplicationUser(ApplicationUser user);
        void DeleteApplicationUser(ApplicationUser user);
    }
}
