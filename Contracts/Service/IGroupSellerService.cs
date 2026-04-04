using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IGroupSellerService
    {
        Task<IEnumerable<GroupSeller>> GetAllGroupSellersAsync();
        Task<GroupSeller?> FindGroupSellerByIdAsync(Guid GroupSellerId, bool tracking);
        Task CreateGroupSellerAsync(GroupSeller groupSeller);
        Task UpdateGroupSellerAsync(GroupSeller groupSeller);
        Task DeleteGroupSellerAsync(Guid groupSellerId);
    }
}
