using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class RefreshTokenService : IRefreshTokenService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;


        public RefreshTokenService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }


       public async Task<IEnumerable<UserRefreshToken>> GetAllUserRefreshTokensAsync()=>await _repoManager.RefreshTokenRepo.GetAllUserRefreshTokens();
        public async Task<UserRefreshToken> CreateUserRefreshTokenAsync(UserRefreshToken token)
        {
            _repoManager.RefreshTokenRepo.CreateUserRefreshToken(token);
            await _repoManager.SaveRepoDataAsync();
            return token;
        }
        public async Task UpdateUserRefreshTokenAsync(UserRefreshToken token) {
            var tokenData = await _repoManager.RefreshTokenRepo.FindUserRefreshToken(token.UserId, true);
            if (token == null)
            {
                throw new ObjectBadRequestExeption($"token with id {token.UserId} cannot be found");
            }
                tokenData.RevokedAt = DateTime.UtcNow;
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteUserRefreshTokenAsync(string tokenId)
        {
            var token = await _repoManager.RefreshTokenRepo.FindUserRefreshToken(tokenId, true);
            if (token == null)
            {
                throw new ObjectBadRequestExeption($"token with id {tokenId} cannot be found");
            }
            _repoManager.RefreshTokenRepo.DeleteUserRefreshToken(token);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task<UserRefreshToken?> FindUserRefreshTokenAsync(string userId, bool tracking)=>await _repoManager.RefreshTokenRepo.FindUserRefreshToken(userId,tracking);

  public async Task<IEnumerable<UserRefreshToken?>> FindUserRefreshTokensByUserIdAsync(string userId, bool tracking)=>await _repoManager.RefreshTokenRepo.FindUserRefreshTokensByUserId(userId,tracking);
    }
}
