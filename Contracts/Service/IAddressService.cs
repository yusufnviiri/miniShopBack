using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAllAddressesAsync();
        Task<IEnumerable<AddressDto>> SearchAddressesAsync(string identifier);
        Task<AddressDto?> FindAddressByIdAsync(bool tracking, int addressId);
        Task<int> CreateAddressAsync(AddressDto address);
        Task UpdateAddressAsync(AddressDto address);
        Task DeleteAddressAsync(int addressId);
    }
}
