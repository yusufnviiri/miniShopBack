using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface ISellerRestrictionRepo
    {
        Task<IEnumerable<SellerRestrictionDto>> GetAllSellerRestrictions();
        Task<SellerRestriction?> FindSellerRestrictionById(Guid restrictionId, bool tracking);
        void CreateSellerRestriction(SellerRestriction restriction);
        void UpdateSellerRestriction(SellerRestriction restriction);
        void DeleteSellerRestriction(SellerRestriction restriction);

    }
}
