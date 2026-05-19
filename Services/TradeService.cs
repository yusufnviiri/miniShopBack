using AutoMapper;
using Contracts;
using Contracts.Lucene;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Services.BusinessRules;
using Shared.Dtos;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class TradeService : ITradeService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly SlugService _slugService;
        private readonly ITradeIndexer _indexer;

        public TradeService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, SlugService slugService, ITradeIndexer tradeIndexer )
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _dbContext = dbContext;
            _slugService = slugService;
            _indexer = tradeIndexer;

        }

        private string CreateTradeSlug(string name)
        {
            return _slugService.Generate(name);
        }
        public async Task<(ICollection<HomePageTradeDto> tradersData, MetaData MetaData)> GetHomePageTradesAsync(ProductRequestParameters requestParameters, CancellationToken ct = default)
        {
            var tradesPagedList = await _repoManager.TradeRepo.GetHomePageTrades(requestParameters,ct);
            return (tradersData: tradesPagedList, tradesPagedList.MetaData);
        }
        public async Task<TradeDataDto?> GetTradeDataAsync(Guid tradeId)
        {
            var trade = await _repoManager.TradeRepo.GetTradeData(tradeId);
            if (trade != null)
            {
                trade.Contact = await _repoManager.UserProfileRepo.GetUserContact(trade.SellerUserProfileId) ?? "";
                trade.WhatsAppNumber = await _repoManager.SellerProfileRepo.GetSellerWhatsAppNumber(trade.SellerUserProfileId) ?? "";
            }
            return trade;
        }
        public async Task<ShowTradeDataDto?> FindSellerTradeAsync(Guid tradeId) => await _repoManager.TradeRepo.FindSellerTrade(tradeId);
        public async Task<Trade?> FindTradeForUpdateAsync(Guid tradeId) => await _repoManager.TradeRepo.FindTradeForUpdate(tradeId);
        public async Task<Trade> CreateTradeAsync(NewTradeDto tradeDto, CancellationToken ct = default)

        {
            if (tradeDto.MinimumPrice < 0 || tradeDto.MinimumPrice > 1000000000)
            {
                throw new Exception($"Invalid MinimumPrice: {tradeDto.MinimumPrice}");
            }

            var tradeEntity = _mapper.Map<Trade>(tradeDto);
            var (commodityClass, sellerProfileId) = await SellerRules.CommodityClassToSellerRef(tradeDto.SellerId, _repoManager);


            tradeEntity.CommodityClassId = commodityClass;
            tradeEntity.SellerProfileId = sellerProfileId;

            var numberOfTrades = await _repoManager.TradeRepo.NextTradeSlugNumberAsync();
            tradeEntity.Slug =  $"{CreateTradeSlug(tradeDto.TradeName)}-{numberOfTrades}";
            _repoManager.TradeRepo.CreateTrade(tradeEntity);

            try
            {
                await _repoManager.SaveRepoDataAsync();
                await _indexer.QueueIndexAsync(tradeEntity.TradeId, ct);

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("==== INNER EXCEPTION ====");
                Console.WriteLine(ex.InnerException?.Message);

                foreach (var entry in _dbContext.ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    {
                        Console.WriteLine($"Entity: {entry.Entity.GetType().Name}");

                        foreach (var prop in entry.Properties)
                        {
                            Console.WriteLine($"{prop.Metadata.Name}: {prop.CurrentValue}");
                        }
                    }
                }

                throw;
            }
            
            return tradeEntity;
        }




        public async Task UpdateTradeAsync(Trade trade, CancellationToken ct = default)
        {
            _repoManager.TradeRepo.UpdateTrade(trade);
            await _repoManager.SaveRepoDataAsync();
            await _indexer.QueueIndexAsync(trade.TradeId, ct);

        }
        public async Task DeleteTradeAsync(Guid tradeId, CancellationToken ct = default)
        {
            var existingTrade = await _repoManager.TradeRepo.FindTradeForUpdate(tradeId);
            if (existingTrade != null)
            {
                _repoManager.TradeRepo.DeleteTrade(existingTrade);
                await _repoManager.SaveRepoDataAsync();
                await _indexer.QueueRemoveAsync(tradeId, ct);


            }
            else
            {
                throw new ObjectBadRequestExeption($"trade with id {tradeId} not found");
            }
        }

        public void MakeTradeFeautured(Guid tradeId) => _repoManager.TradeRepo.MakeTradeFeautured(tradeId);
        public void MakeAllTradesFeautured() => _repoManager.TradeRepo.MakeAllTradesFeautured();

        public async Task UpdateTradeDescription(SharedUpdatesDto sharedUpdates, CancellationToken ct = default)
        {
            var trade = await _repoManager.TradeRepo.FindTradeForUpdate(sharedUpdates.ItemId);
            if (trade != null)
            {
                trade.Description = sharedUpdates.ItemDescription;
                await _repoManager.SaveRepoDataAsync();
                await _indexer.QueueIndexAsync(trade.TradeId, ct);

            }
            else
            {
                throw new ObjectBadRequestExeption($"trade with id {sharedUpdates.ItemId} not found");
            }

        }


      public async   Task<ShowTradeDataDto?> FindTradeBySlugNameAsync(bool tracking, string slugName)=> await _repoManager.TradeRepo.FindTradeBySlugName(tracking,slugName);

        public async Task<TradeDataDto?> GetTradeDataUsingSlugNameAsync(string slugName)=> await _repoManager.TradeRepo.GetTradeDataUsingSlugName(slugName);
        public async Task<ShowTradeDataDto?> FindSellerTradeUsingSlugNameAsync(bool tracking, string slugName)=> await _repoManager.TradeRepo.FindSellerTradeUsingSlugName(tracking,slugName);

        public async Task<List<SlugInfo>> GetAllTradesSlugsAsync() => await _repoManager.TradeRepo.GetAllTradesSlugsAsync();


    }


}
