using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{


    internal sealed class TradeImpressionService : ITradeImpressionService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public TradeImpressionService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TradeImpression>> GetAllTradeImpressionsAsync() => await _repoManager.TradeImpressionRepo.GetAllTradeImpressions();
        public async Task<TradeImpression?> FindTradeImpressionByIdAsync(Guid tradeImpressionId, bool tracking) => await _repoManager.TradeImpressionRepo.FindTradeImpressionById(tradeImpressionId, tracking);


        public async Task<TradeImpression?> FindTradeImpressionByTradeIdAsync(Guid tradeId, bool tracking) => await _repoManager.TradeImpressionRepo.FindTradeImpressionByTradeId(tradeId, tracking);
        public async Task CreateTradeImpressionAsync(NewImpressionDto newImpression)
        {
            TradeImpression impression = new()
            {
                TradeId = newImpression.ItemId,
                UserProfileId = newImpression.UserProfileId
            };

            _repoManager.TradeImpressionRepo.CreateTradeImpression(impression);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateTradeImpressionAsync(TradeImpression impression)
        {
            var oldImpression = await _repoManager.TradeImpressionRepo.FindTradeImpressionForUpdate(impression.TradeImpressionId);
            if (oldImpression != null)
            {
                oldImpression.TradeId = impression.TradeId;

                _repoManager.TradeImpressionRepo.UpdateTradeImpression(oldImpression);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteTradeImpressionAsync(Guid tradeImpressionId)
        {
            var impression = await _repoManager.TradeImpressionRepo.FindTradeImpressionForUpdate(tradeImpressionId);
            if (impression != null)
            {
                _repoManager.TradeImpressionRepo.DeleteTradeImpression(impression);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"object not found for id {tradeImpressionId}");
            }

        }
    }
}