using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IGroupSellerRepo
    {

        Task<IEnumerable<GroupSeller>> GetAllGroupSellers();
        Task<GroupSeller?> FindGroupSellerById(Guid groupSellerId, bool tracking);
        void CreateGroupSeller(GroupSeller groupSeller);
        void UpdateGroupSeller(GroupSeller groupSeller);
        void DeleteGroupSeller(GroupSeller groupSeller);
    }
}
