using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  internal sealed class HomePageCardService : IHomePageCardService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public HomePageCardService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async  Task<IEnumerable<HomePageCard>> GetAllHomePageCardsAsync()
        {
            var homePageCards = await _repoManager.HomePageCardRepo.GetAllHomePageCards();
            return homePageCards;
        }
        public async Task<HomePageCard?> FindHomePageCardByIdAsync(int homePageCardId, bool tracking)=>
            await _repoManager.HomePageCardRepo.FindHomePageCardById(homePageCardId, tracking);
        public async Task CreateHomePageCardAsync(HomePageCard pageCard)
        {
            if (pageCard == null)
            {
                throw new ObjectBadRequestExeption($"Object properties not set");
            }

            if (pageCard.IsGroupCard == true)
            {
                if (pageCard.UserGroupId != null && pageCard.UserGroupId != Guid.Empty)
                {
                    var groupExists = await _repoManager.HomePageCardRepo.CheckIfGroupHomePageCardExists(pageCard.UserGroupId.Value);
                    if (!groupExists)
                    {
                        var lastIndex = await _repoManager.HomePageCardRepo.HomePageCardCount();
                        pageCard.Index = lastIndex + 1;
                        _repoManager.HomePageCardRepo.CreateHomePageCard(pageCard);
                        await _repoManager.SaveRepoDataAsync();
                        _repoManager.ProductRepo.InvalidateHomePageCards();   // ← here                }
                    }


                }
            }
            else
            {
                var lastIndex = await _repoManager.HomePageCardRepo.HomePageCardCount();
                pageCard.Index = lastIndex + 1;
                _repoManager.HomePageCardRepo.CreateHomePageCard(pageCard);
                await _repoManager.SaveRepoDataAsync();
                _repoManager.ProductRepo.InvalidateHomePageCards();   // ← here        
            }
        }
        public async Task UpdateHomePageCardAsync(HomePageCard pageCard)
        {
            if (pageCard == null)
            {
                throw new ObjectBadRequestExeption($"Object properties not set");
            }
             _repoManager.HomePageCardRepo.UpdateHomePageCard(pageCard);
            await _repoManager.SaveRepoDataAsync();
            _repoManager.ProductRepo.InvalidateHomePageCards();   // ← here

        }
        public async Task DeleteHomePageCardAsync(int pageCardId)
        {
            var homePageCard = await _repoManager.HomePageCardRepo.FindHomePageCardForUpdate(pageCardId);
            if (homePageCard == null)
            {
                throw new ObjectBadRequestExeption($"HomePageCard with id: {pageCardId} not found");
            }
             _repoManager.HomePageCardRepo.DeleteHomePageCard(homePageCard);
            await _repoManager.SaveRepoDataAsync();
            _repoManager.ProductRepo.InvalidateHomePageCards();   // ← here

        }
        public async Task<IEnumerable<HomePageCardDto>> GetAllHomePageCardDtos()
        {
            var categoriesQuery = _repoManager.CategoryRepo.CategoriesQueryData();
            var cards = _repoManager.HomePageCardRepo.HomePageCardsQueryData();

            var cardDtos = await cards
                .Select(card => new HomePageCardDto
                {
                    HomePageCardId = card.HomePageCardId,
                    Title = card.Title,
                    Color = card.Color,
                    IsActive = card.IsActive,
                    LinkLabel = card.LinkLabel,
                    IsRow = card.IsRow,
                    Index = card.Index,

                    Categories = categoriesQuery
                        .Where(c => card.CategoryLinks
                            .Any(link => link.CategoryId == c.CategoryId))
                        .Select(c => new MiniCategoryDto
                        {
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName
                        })
                        .ToList()
                })
                .ToListAsync();

            return cardDtos;
        }
    }
}
