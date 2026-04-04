using Contracts.Repo;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.context;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class AddressRepo:RepositoryBase<Address>,IAddressRepo
    {
        public AddressRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
      public async  Task<IEnumerable<AddressDto>> GetAllAddresses()
        {
            return await FindAll(false)
                .Select(a => new AddressDto
                {
                    AddressId = a.AddressId,
                    City = a.City,
                    Region = a.Region,
                    Country = a.Country,
                    Company = a.Company,
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<AddressDto>> SearchAddresses(string identifier)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                return [];

            var pattern = $"%{identifier.Trim()}%";

            return await FindByCondition(a =>
                    EF.Functions.Like(a.City, pattern) ||
                    EF.Functions.Like(a.Company, pattern) ||
                    EF.Functions.Like(a.Country, pattern) ||
                    EF.Functions.Like(a.Region, pattern),
                    false)
                .Select(a => new AddressDto
                {
                    AddressId = a.AddressId,
                    City = a.City,
                    Region = a.Region,
                    Country = a.Country,
                    Company = a.Company
                })
                .ToListAsync();
        }













        public async Task<AddressDto?> FindAddressById(bool tracking, int addressId)
        {
            var address = await FindByCondition(p => p.AddressId == addressId, tracking)
                .Select(a => new AddressDto
                {
                    AddressId = a.AddressId,
                    City = a.City,
                    Region = a.Region,
                    Country = a.Country,
                    Company = a.Company,
                })
                .FirstOrDefaultAsync();
            return address;
        }
        public async Task<Address?> FindAddressForUpdate(int addressId)
        {
            return await FindByCondition(p=>p.AddressId == addressId, true).FirstOrDefaultAsync();
        }
        public Address CreateAddress(Address address) { CreateBase(address); return address; }
        public void UpdateAddress(Address address)=>UpdateBase(address);
        public void DeleteAddress(Address address)=>DeleteBase(address);


    }
}
