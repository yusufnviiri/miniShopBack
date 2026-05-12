using AutoMapper;
using Azure.Core;
using Contracts;
using Contracts.Lucene;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.BusinessRules;
using Shared.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Services
{
    internal sealed class UserProfileService : IUserProfileService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISmsSender _smsSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SlugService _slugService;
        private readonly IUserIndexer _userIndexer;




        public UserProfileService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ISmsSender smsSender, IHttpContextAccessor httpContextAccessor, SlugService slugService,IUserIndexer userIndexer)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
            _roleManager = roleManager;
            _smsSender = smsSender;
            _httpContextAccessor = httpContextAccessor;
            _slugService = slugService;
            _userIndexer = userIndexer;

        }

        private string CreateUserProfileSlug(string name)
        {
            return _slugService.Generate(name);
        }

        public async Task<IEnumerable<UserProfileDto>> GetAllUserProfilesAsync() => await _repoManager.UserProfileRepo.GetAllUserProfiles();
        public  Task<UserProfile?> FindUserProfileByIdAsync(Guid UserProfileId, bool tracking)=>_repoManager.UserProfileRepo.FindUserProfileById(UserProfileId, tracking);
     public async Task UpdateUserProfileAsync(NewUserDataDto userProfile, CancellationToken ct = default)
        {

            var address = await _repoManager.AddressRepo.FindAddressForUpdate(userProfile.AddressId);
            if (address != null) { 
                address.Region = userProfile.Region;
                address.City = userProfile.City;
                address.Country = userProfile.Country;
                address.Company = userProfile.Company;            
            }
            await _repoManager.SaveRepoDataAsync();



            var user =await _userManager.FindByIdAsync(userProfile.IdentityUserId);
            if (user != null) {
                user.FirstName = userProfile.FirstName;
                user.LastName = userProfile.LastName;
                user.PhoneNumber = userProfile.PhoneNumber;
                user.Email = userProfile.Email;
                user.UserName = userProfile.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                await _userIndexer.QueueIndexAsync(userProfile.UserProfileId, ct);

                if (!result.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        result.Errors.Select(e => e.Description));
                    throw new ObjectBadRequestExeption(errors);
                }
            }

           

        }

        public async Task<UserProfileDto?> ShowUserProfileAsync(Guid UserProfileId)
        {
            var profile = await _repoManager.UserProfileRepo.ShowUserProfile(UserProfileId);
            var userFollowing = await _repoManager.UserPreferenceRepo.GetUserFollowing(UserProfileId);
            if (profile != null)
            {
                profile.Following = userFollowing;
            }
            return profile;

        }

        public async Task<UserProfileDto?> ShowUserProfileBySlugAsync(string slug)
        {
            var userProfileId = await _repoManager.UserProfileRepo.GetUserProfileIdBySlugName(slug);
            if (userProfileId == Guid.Empty)
            {
                throw new ObjectBadRequestExeption("user with specified id not found");
            }
            else
            {

                var profile = await _repoManager.UserProfileRepo.ShowUserProfile(userProfileId);
                var userFollowing = await _repoManager.UserPreferenceRepo.GetUserFollowing(userProfileId);
                if (profile != null)
                {
                    profile.Following = userFollowing;
                }
                return profile;
            }

        }



        public async Task DeleteUserProfileAsync(Guid userProfile, CancellationToken ct = default)
        {
            var existingProfile = await _repoManager.UserProfileRepo.FindUserProfileById(userProfile, tracking: true);
            if (existingProfile != null)
            {
                _repoManager.UserProfileRepo.DeleteUserProfile(existingProfile);

                await _repoManager.SaveRepoDataAsync();
                await _userIndexer.QueueRemoveAsync(userProfile, ct);

            }
        }
        private async Task<int> ResolveOrCreateAddressAsync(NewUserDataDto dto)
        {
            if (dto.AddressId > 0)
            {
                var address = await _repoManager.AddressRepo
                    .FindAddressById(false, dto.AddressId);

                if (address is null)
                    throw new ObjectWithIntNotFoundException(dto.AddressId);

                return address.AddressId;
            }

            if (string.IsNullOrWhiteSpace(dto.City) &&
                string.IsNullOrWhiteSpace(dto.Region) &&
                string.IsNullOrWhiteSpace(dto.Company))
            {
                throw new InvalidOperationException("Insufficient address data.");
            }

            var addressEntity = new Address
            {
                City = dto.City,
                Region = dto.Region,
                Company = dto.Company,
                Country = dto.Country
            };

            var created = _repoManager.AddressRepo.CreateAddress(addressEntity);
            await _repoManager.SaveRepoDataAsync();

            return created.AddressId;
        }
        private async Task<bool> CheckIfPhoneNumberExists(string phoneNumber)
        {
           
            var existingUser = await _userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);

            if (existingUser)
            {
                throw new DataFormatException("Phone number already exists");
            }
            return false;
        }

        private async Task<ApplicationUser> CreateIdentityUserAsync(NewUserDataDto dto)
        {
           await CheckIfPhoneNumberExists( dto.PhoneNumber);

            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.PhoneNumber,
                PhoneNumberVerified = false,
                PhoneNumber = dto.PhoneNumber,
                
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

        // var otp =   CreateUserOtp(user);
        //var newOtp=    _repoManager.UserOtpRepo.CreateUserOtp(otp);
        //    await _repoManager.SaveRepoDataAsync();

         


            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw  new ObjectBadRequestExeption(errors)  ;
            }

            //await _smsSender.SendAsync(user.PhoneNumber, $"Your verification code is {newOtp.CodeHash}. It expires in 2 minutes.");
           
            //CHECK IF ROLE EXISTS
            var roleRef = await _userManager.IsInRoleAsync(user, dto.Role);
            if (!roleRef)
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));
            }
            var roleResult =   await _userManager.AddToRoleAsync(user, dto.Role);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(e => e.Description));

                throw new ObjectBadRequestExeption(errors);
            }

            return user;
        }
        private async Task<Guid> CreateDomainUserProfileAsync(string identityUserId, int addressId,string username, CancellationToken ct = default)
        {
            var profile = new UserProfile
            {
                IdentityUserId = identityUserId,
                AddressId = addressId
            };
            var numberOfUsers = await _repoManager.UserProfileRepo.NumberOfUserProfiles();

            profile.Slug = $"user={CreateUserProfileSlug(username)}-{numberOfUsers + 1}";

            _repoManager.UserProfileRepo.CreateUserProfile(profile);
            await _repoManager.SaveRepoDataAsync();
            await _userIndexer.QueueIndexAsync(profile.UserProfileId, ct);

            return profile.UserProfileId;
        }

        public async Task VerifyPhoneAsync(VerifyOtpRequest request)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new ObjectBadRequestExeption("User not found");

            var otpRecord = await _repoManager.UserOtpRepo.FindUserOtpWithPurpose(user.Id, "VerifyPhone", false);

            if (otpRecord == null)
                throw new ObjectBadRequestExeption("OTP expired or invalid");

            if (otpRecord.AttemptCount >= 5)
                throw new ObjectBadRequestExeption("Too many attempts");

            otpRecord.AttemptCount++;

            if (!BCrypt.Net.BCrypt.Verify(request.Code, otpRecord.CodeHash))
            {
                await _repoManager.SaveRepoDataAsync();
                throw new ObjectBadRequestExeption("Invalid code");
            }

            otpRecord.Used = true;
            user.PhoneNumberConfirmed = true;
            user.PhoneNumberVerified = false;
            await _userManager.UpdateAsync(user);
            await _repoManager.SaveRepoDataAsync();
        }

        public async Task CreateUserDevice(LoginRequestDto loginRequest,ApplicationUser user)
        {

            //var device = await _repoManager.UserDeviceRepo.FindUserDevice(loginRequest.DeviceId, user.Id, false);


            var device = await _repoManager.UserDeviceRepo.FindUserDevice(loginRequest.PhoneNumber, user.Id, false);
            if (device == null)
            {
                device = new UserDevice
                {
                    UserId = user.Id,
                    DeviceId = loginRequest.PhoneNumber,
                    UserAgent = _httpContextAccessor.UserAgent(),
                    IpHash = _httpContextAccessor.IpAddress(),

                    //IpHash = GenerateOtp.HashIp(_httpContextAccessor.IpAddress()),
                    IsTrusted = false,
                    FirstSeenAt = DateTime.UtcNow,
                    LastSeenAt = DateTime.UtcNow
                };

                _repoManager.UserDeviceRepo.CreateUserDevice(device);
                await _repoManager.SaveRepoDataAsync();
            }

        }




        public async Task<Guid> CreateUserProfileAsync(NewUserDataDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto);


            try
            {                
                var addressId = await ResolveOrCreateAddressAsync(dto);
                var user = await CreateIdentityUserAsync(dto);
             return await CreateDomainUserProfileAsync(user.Id, addressId, $"{dto.FirstName} {dto.LastName}");

            }
            catch
            {
                throw;
            }
        }


public async Task<string> CreateUserProfileWithMemberAsync(GroupMemberJoinNewUserProfileDataDto dataDto)
        {
         
            await CheckIfPhoneNumberExists(dataDto.PhoneNumber);

            var NewUserData = _mapper.Map<NewUserDataDto>(dataDto);
            NewUserData.Role = "user";
            switch(dataDto.GroupRoleId)
            {
                case 1:
                    NewUserData.GroupRole = "Member";
                    break;
                case 2:
                    NewUserData.GroupRole = "Treasurer";
                    break;
                case 3:
                    NewUserData.GroupRole = "Secretary";
                    break;
                case 4:
                    NewUserData.GroupRole = "Chairperson";
                    break;
                case 5:
                    NewUserData.GroupRole = "Admin";
                    break;
                case 6:
                    NewUserData.GroupRole = "Auditor";
                    break;
                default:
                    NewUserData.GroupRole = "Member";
                    break;
            }
            NewUserData.AddressId=await _repoManager.UserGroupRepo.GetGroupAddress(dataDto.UserGroupId);
            var userProfileId = await CreateUserProfileAsync(NewUserData);
            dataDto.UserProfileId = userProfileId;
            GroupMember groupMember = new GroupMember()
            {UserGroupId=dataDto.UserGroupId,
            UserProfileId=userProfileId,
                GroupRoleId=dataDto.GroupRoleId,
                MemberStatusId=dataDto.MemberStatusId,

            };
       
  
            _repoManager.GroupMemberRepo.CreateGroupMember(groupMember);
            var createdMember = await _repoManager.UserProfileRepo.FindUserProfileById(userProfileId,true);
            if (createdMember != null)
            {
                createdMember.ActiveGroupId = dataDto.UserGroupId;
            }
            await _repoManager.SaveRepoDataAsync();
            var groupSlugName = await _repoManager.UserGroupRepo.GetGroupSlugNameOnly(groupMember.UserGroupId);
            return groupSlugName ?? string.Empty;
        }


public async   Task<IEnumerable<UserProfileDto>> GetAllUserProfilesWithoutGroupsAsync()=>await _repoManager.UserProfileRepo.GetAllUserProfilesWithoutGroups();

        public async Task<IEnumerable<UserProfileDto>> GetSellerUserProfilesAsync()=>await _repoManager.UserProfileRepo.GetSellerUserProfiles();




        private UserOtp CreateUserOtp(ApplicationUser user)
        {
            var otp = GenerateOtp.GenerateOtpEndPoint();

            var userOtp = new UserOtp
            {
                UserId = user.Id,
                CodeHash = GenerateOtp.HashOtp(otp),
                Purpose = "VerifyPhone",
                ExpiresAt = DateTime.UtcNow.AddMinutes(2),
                AttemptCount = 0,
                Used = false
            };
            return userOtp;
        }

        public async Task<LoggedInUserDataDto?> GetLoggedInUserDataDtoAsync(Guid UserProfileId)=> await _repoManager.UserProfileRepo.GetLoggedInUserDataDto(UserProfileId);
            public async Task<Guid> GetUserProfileIdFromIdentityUserAsync(string IdentityUserId) => await _repoManager.UserProfileRepo.GetUserProfileIdFromIdentityUser(IdentityUserId);

    }

}




   

