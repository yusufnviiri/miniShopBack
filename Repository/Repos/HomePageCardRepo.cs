using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class HomePageCardRepo : RepositoryBase<HomePageCard>, IHomePageCardRepo
    {
        public HomePageCardRepo(ApplicationDbContext _db) : base(_db)
        {

        }
        public async Task<IEnumerable<HomePageCard>> GetAllHomePageCards()
        {
            return await FindAll(false).ToListAsync();
        }
        public IQueryable<HomePageCard> HomePageCardsQueryData() => FindAll(false).Include(p=>p.CategoryLinks);

        public async Task<HomePageCard?> FindHomePageCardById(int homePageCardId, bool tracking)
        {
            var homePageCard = await FindByCondition(h => h.HomePageCardId == homePageCardId, tracking).FirstOrDefaultAsync();
            return homePageCard;
        }
        public async Task<HomePageCard?> FindHomePageCardForUpdate(int homePageCardId)
        {
            var homePageCard = await FindByCondition(h => h.HomePageCardId == homePageCardId, true).FirstOrDefaultAsync();
            return homePageCard;
        }


        public void CreateHomePageCard(HomePageCard pageCard) => CreateBase(pageCard);
        public void UpdateHomePageCard(HomePageCard pageCard) => UpdateBase(pageCard);
        public void DeleteHomePageCard(HomePageCard pageCard) => DeleteBase(pageCard);
    }
}
