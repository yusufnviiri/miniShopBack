using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IAddressRepo
    {
        Task<IEnumerable<AddressDto>> GetAllAddresses();
        Task<IEnumerable<AddressDto>> SearchAddresses(string identifier);
        Task<AddressDto?> FindAddressById(bool tracking,int addressId);
        Task<Address?> FindAddressForUpdate(int addressId);
        Address CreateAddress(Address address );
        void UpdateAddress(Address address);
        void DeleteAddress(Address address);
    }
}
