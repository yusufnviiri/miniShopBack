using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IHomePageCardRepo
    {
        Task<IEnumerable<HomePageCard>> GetAllHomePageCards();
     IQueryable<HomePageCard> HomePageCardsQueryData();

        //Task<IEnumerable<HomePageCardDto>> GetAllHomePageCardDtos();
        Task<HomePageCard?> FindHomePageCardById(int homePageCardId , bool tracking);
        Task<HomePageCard?> FindHomePageCardForUpdate(int homePageCardId);
        void CreateHomePageCard(HomePageCard pageCard );
        void UpdateHomePageCard(HomePageCard pageCard);
        void DeleteHomePageCard(HomePageCard pageCard);
    }
}
