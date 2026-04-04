using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class ApplicationUserService:IApplicationUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepositoryManager _repoManager;

        private User? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;


        public ApplicationUserService(ILoggerManager logger,IRepositoryManager repository, IMapper mapper,
        UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInMager)
        {
            _logger = logger; _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInMager;
            _repoManager = repository;

        }
        public async Task<IEnumerable<ShowApplicationUserDto>> GetAllApplicationUsersAsync() => await _repoManager.ApplicationUserRepo.GetAllApplicationUsers();
        public async Task<ShowApplicationUserDto?> FindApplicationUserByIdAsync(bool tracking, string userId)=>await _repoManager.ApplicationUserRepo.FindApplicationUserById(tracking, userId);
        public async Task<ApplicationUser?> FindApplicationUserForUpdateAsync(string userId)=>await _repoManager.ApplicationUserRepo.FindApplicationUserForUpdate(userId);
        public async Task CreateApplicationUserAsync(ApplicationUser user)
        {
            _repoManager.ApplicationUserRepo.CreateApplicationUser(user);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateApplicationUserAsync(ApplicationUser user)
        {
            if (user == null)
            {
                return;
            }
            _repoManager.ApplicationUserRepo.UpdateApplicationUser(user);
            await _repoManager.SaveRepoDataAsync();
        }

        public async Task DeleteApplicationUserAsync(ApplicationUser user) { _repoManager.ApplicationUserRepo.DeleteApplicationUser(user);
            await _repoManager.SaveRepoDataAsync();
        }

    }
}
