using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IHomePageCardService
    {
        Task<IEnumerable<HomePageCard>> GetAllHomePageCardsAsync();
        Task<HomePageCard?> FindHomePageCardByIdAsync(int homePageCardId, bool tracking);
        Task CreateHomePageCardAsync(HomePageCard pageCard);
        Task UpdateHomePageCardAsync(HomePageCard pageCard);
        Task DeleteHomePageCardAsync(int pageCardId);
        Task<IEnumerable<HomePageCardDto>> GetAllHomePageCardDtos();

    }
}
