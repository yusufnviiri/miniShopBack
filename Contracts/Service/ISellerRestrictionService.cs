using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface ISellerRestrictionService
    {
        Task<IEnumerable<SellerRestrictionDto>> GetAllSellerRestrictionsAsync();
        Task<SellerRestriction?> FindSellerRestrictionByIdAsync(Guid restrictionId, bool tracking);
        Task CreateSellerRestrictionAsync(SellerRestriction restriction);
        Task UpdateSellerRestrictionAsync(SellerRestrictionDto restriction);
        Task DeleteSellerRestrictionAsync(Guid restrictionId);
    }
}
