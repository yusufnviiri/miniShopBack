using AfricasTalkingCS;
using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class UserOtpService : IUserOtpService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;


        public UserOtpService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

       public async  Task<UserOtp?> FindUserOtpAsync(string id, bool tracking)=>
            await _repoManager.UserOtpRepo.FindUserOtp(id,tracking);
        public async Task<IEnumerable<UserOtp>> GetAllUserOtpsAsync()=>await _repoManager.UserOtpRepo.GetAllUserOtps();
        public async Task CreateUserOtpAsync(UserOtp userOtp )
        {
            _repoManager.UserOtpRepo.CreateUserOtp(userOtp);
            await _repoManager.SaveRepoDataAsync();
         

        }

        public async Task CreateUserOtpAsyncRef(ApplicationUser user )
        {
            //_repoManager.UserOtpRepo.CreateUserOtp(userOtp);
            await _repoManager.SaveRepoDataAsync();


        }
        public async Task UpdateUserOtpAsync(UserOtp userOtp)
        {
            var Otp = await _repoManager.UserOtpRepo.FindUserOtp(userOtp.UserId, true);
            if (Otp == null) {return; }
            var updated= _mapper.Map<UserOtp>(Otp);
            _repoManager.UserOtpRepo.UpdateUserOtp(updated);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteUserOtpAsync(string userOtp)
        {
            var Otp = await _repoManager.UserOtpRepo.FindUserOtp(userOtp,true);
            if (Otp == null) return;
            _repoManager.UserOtpRepo.DeleteUserOtp(Otp);
            await _repoManager.SaveRepoDataAsync();

        }



    }
}
