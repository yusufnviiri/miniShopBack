using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class GroupFeaturedProductService : IGroupFeaturedProductService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;

        public GroupFeaturedProductService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }






        public async Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductsAsync() => await _repoManager.GroupFeaturedProductRepo.GetAllGroupFeaturedProducts();

        //Task<IEnumerable<GroupFeaturedProductDto>> GetAllGroupFeaturedProductDtos();
        public async Task<GroupFeaturedProduct?> FindGroupFeaturedProductByIdAsync(int groupFeaturedProductId, bool tracking) => await _repoManager.GroupFeaturedProductRepo.FindGroupFeaturedProductById(groupFeaturedProductId, tracking);
        public async Task CreateGroupFeaturedProductAsync(NewGroupFeaturedProductDto featuredProductDto)
        {
            var isgroupFeatured= await  _repoManager.GroupFeaturedProductRepo.IsGroupFeaturedProduct(featuredProductDto.ProductId, featuredProductDto.UserGroupId);

            if (!isgroupFeatured)
            {
                GroupFeaturedProduct groupFeaturedProduct = new GroupFeaturedProduct
                {
                    ProductId = featuredProductDto.ProductId,
                    UserGroupId = featuredProductDto.UserGroupId
                };
                _repoManager.GroupFeaturedProductRepo.CreateGroupFeaturedProduct(groupFeaturedProduct);
                await _repoManager.SaveRepoDataAsync();
            }
            else
            {
                throw new ObjectBadRequestExeption($"GroupFeaturedProduct with ProductId {featuredProductDto.ProductId} and UserGroupId {featuredProductDto.UserGroupId} already exists.");
            }
            }
        public async Task UpdateGroupFeaturedProductAsync(GroupFeaturedProduct groupFeaturedProduct)
        {
            var existingGroupFeaturedProduct = await _repoManager.GroupFeaturedProductRepo.FindGroupFeaturedProductForUpdate(groupFeaturedProduct.GroupFeaturedProductId);
            if (existingGroupFeaturedProduct == null)
            {
                _logger.LogError($"GroupFeaturedProduct with id {groupFeaturedProduct.GroupFeaturedProductId} not found for update.");
                throw new ObjectBadRequestExeption($"GroupFeaturedProduct with id {groupFeaturedProduct.GroupFeaturedProductId} not found.");
            }
            else
            {
                existingGroupFeaturedProduct.ProductId = groupFeaturedProduct.ProductId;
                existingGroupFeaturedProduct.UserGroupId = groupFeaturedProduct.UserGroupId;

                _repoManager.GroupFeaturedProductRepo.UpdateGroupFeaturedProduct(groupFeaturedProduct);
                await _repoManager.SaveRepoDataAsync();
            }
        }
        public async Task DeleteGroupFeaturedProductAsync(int groupFeaturedProductId)
        {
            var groupFeaturedProduct = await _repoManager.GroupFeaturedProductRepo.FindGroupFeaturedProductForUpdate(groupFeaturedProductId);
            if (groupFeaturedProduct == null)
            {
                _logger.LogError($"GroupFeaturedProduct with id {groupFeaturedProductId} not found for deletion.");
                throw new ObjectBadRequestExeption($"GroupFeaturedProduct with id {groupFeaturedProductId} not found.");
            }
            else
            {
                _repoManager.GroupFeaturedProductRepo.DeleteGroupFeaturedProduct(groupFeaturedProduct);
                await _repoManager.SaveRepoDataAsync();
            }
        }

        public async Task<IEnumerable<GroupFeaturedProductDto>> GetGroupFeaturedProductsByUserGroupIdAsync(Guid userGroupId)
        {
            return await _repoManager.GroupFeaturedProductRepo
                .GroupFeaturedProductsQueryData()
                .Where(g => g.UserGroupId == userGroupId)
                .Select(p => new GroupFeaturedProductDto
                {
                    GroupFeaturedProductId = p.GroupFeaturedProductId,
                    ProductId = p.ProductId,
                    UserGroupId = p.UserGroupId,

                    ProductName = p.Product!.ProductName,
                    UserGroupName = p.UserGroup!.UserGroupName,

                    ProductImageId = p.Product!.Images
                        .Select(i => (Guid?)i.ProductImageId)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}