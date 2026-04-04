using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IBuyerProfileRepo
    {
        Task<IEnumerable<BuyerProfileDto>> GetAllBuyerProfiles();
        Task<BuyerProfile?> FindBuyerProfileById(Guid buyerProfileId, bool tracking);
        void CreateBuyerProfile(BuyerProfile buyerProfile );
        void UpdateBuyerProfile(BuyerProfile buyerProfile);
        void DeleteBuyerProfile(BuyerProfile buyerProfile);
    }
}
