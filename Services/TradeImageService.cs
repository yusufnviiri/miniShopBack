using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
        internal sealed class TradeImageService : ITradeImageService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;


        public TradeImageService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _env = env;

        }



        public async Task<IEnumerable<TradeImageRefDto?>> GetAllTradeImagesAsync() => await _repoManager.TradeImageRepo.GetAllTradeImages();


        public async Task<TradeImage?> FindTradeImageByTradeIdAsync(Guid imageId, Guid productId, bool tracking) => await _repoManager.TradeImageRepo.FindTradeImageByTradeId(imageId, productId, tracking);
      
        
        public async Task<TradeImage?> GetTradePrimaryImage(Guid tradeId, bool tracking) => await _repoManager.TradeImageRepo.GetTradePrimaryImage(tradeId, tracking);
       
        
        public async Task CreateTradeImageAsync(TradeImageDto imageDto)
        {
            if (imageDto != null)
            {
                var image = _mapper.Map<TradeImage>(imageDto);
                _repoManager.TradeImageRepo.CreateTradeImage(image);
                await _repoManager.SaveRepoDataAsync();
            }

        }
        public async Task UpdateTradeImageAsync(TradeImageDto imageDto)
        {
            if (imageDto != null)
            {
                var oldImage = await _repoManager.TradeImageRepo.FindTradeImageByTradeId(imageDto.TradeImageId, (Guid)imageDto.TradeId, true);
                _mapper.Map(imageDto, oldImage);
                _repoManager.TradeImageRepo.UpdateTradeImage(oldImage);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteTradeImageIdRefAsync(Guid imageId, Guid tradeId)
        {
            var image = await _repoManager.TradeImageRepo
                .FindTradeImageByTradeId(imageId, tradeId, true);

            if (image == null)
                return;


            var tradeFolder = Path.Combine(
                _env.WebRootPath,
                "uploads", "trades",
                "images",
                tradeId.ToString()
            );

            if (Directory.Exists(tradeFolder))
            {
                var files = Directory.GetFiles(
                    tradeFolder,
                    $"{imageId}_*"
                );

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }

            _repoManager.TradeImageRepo.DeleteTradeImage(image);
            await _repoManager.SaveRepoDataAsync();
        }
    

        public async Task MakeImagePrimaryAsync(MiniTradeImage tradeImage)
        {
            var primaryImage = await _repoManager.TradeImageRepo.GetTradePrimaryImage(tradeImage.TradeId, true);
            if (primaryImage != null)
            {
                primaryImage.IsPrimary = false;
                await _repoManager.SaveRepoDataAsync();
            }

            var imageData = await _repoManager.TradeImageRepo.FindTradeImageById(tradeImage.ImageId, true);
            if (imageData != null)
            {
                imageData.IsPrimary = true;
                await _repoManager.SaveRepoDataAsync();
            }
            else return;

        }

        public async Task DeleteTradeImageAsync(Guid imageId)
        {
            var image = await _repoManager.TradeImageRepo.FindTradeImageById(imageId, true);
            if (image != null)
            {
                _repoManager.TradeImageRepo.DeleteTradeImage(image);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"product image with id {imageId} not found");
            }

        }


        public async Task CreateTradeImageListAsync(ICollection<TradeImage> images)
        {
            if (images != null && images.Count > 0)
            {
                foreach (var img in images)
                {
                    _repoManager.TradeImageRepo.CreateTradeImage(img);
                }

                await _repoManager.SaveRepoDataAsync();
            }
            else return;
        }



    }

}

