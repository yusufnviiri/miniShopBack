using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class AddressService : IAddressService
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }
        public async  Task<IEnumerable<AddressDto>> GetAllAddressesAsync()=>
            await _repoManager.AddressRepo.GetAllAddresses();
        public async Task<IEnumerable<AddressDto>> SearchAddressesAsync(string identifier)=>
            await _repoManager.AddressRepo.SearchAddresses(identifier);
        public async Task<AddressDto?> FindAddressByIdAsync(bool tracking, int addressId)=>
            await _repoManager.AddressRepo.FindAddressById(tracking, addressId);
        public async Task<int> CreateAddressAsync(AddressDto address)
        {
            var addressEntity = _mapper.Map<Address>(address);
            _repoManager.AddressRepo.CreateAddress(addressEntity);
            await _repoManager.SaveRepoDataAsync();
            return addressEntity.AddressId;

        }
        public async Task UpdateAddressAsync(AddressDto address)
        {
            var addressEntity = await _repoManager.AddressRepo.FindAddressForUpdate(address.AddressId);
            if (addressEntity == null)
            {
                throw new ArgumentNullException(nameof(addressEntity), $"Address with ID {address.AddressId} not found.");
            }
            _mapper.Map(address, addressEntity);
            _repoManager.AddressRepo.UpdateAddress(addressEntity);
        }
        public async Task DeleteAddressAsync(int addressId)
        {
            var addressEntity = await _repoManager.AddressRepo.FindAddressForUpdate(addressId);
            if (addressEntity == null)
            {
                throw new ArgumentNullException(nameof(addressEntity), $"Address with ID {addressId} not found.");
            }
            _repoManager.AddressRepo.DeleteAddress(addressEntity);
            await _repoManager.SaveRepoDataAsync();
        }

    }
}