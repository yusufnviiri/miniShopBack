using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IBuyerProfileService
    {
        Task<IEnumerable<BuyerProfileDto>> GetAllBuyerProfilesAsync();
        Task<BuyerProfile?> FindBuyerProfileByIdAsync(Guid buyerProfileId, bool tracking);
        Task CreateBuyerProfileAsync(BuyerProfile buyerProfile);
        Task UpdateBuyerProfileAsync(BuyerProfileDto buyerProfileDto);
        Task DeleteBuyerProfileAsync(Guid buyerProfileId);
    }
}
