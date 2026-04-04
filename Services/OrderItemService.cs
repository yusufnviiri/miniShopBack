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
    internal sealed class OrderItemService:IOrderItemService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderItemService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }


         public async Task<IEnumerable<ShowOrderItemDto>> GetAllOrderItemsAsync()=>
            await _repoManager.OrderItemRepo.GetAllOrderItems();
        public  Task<OrderItem?> FindOrderItemByIdAsync(Guid OrderId, bool tracking)=>
             _repoManager.OrderItemRepo.FindOrderItemById(OrderId, tracking);
        public async Task CreateOrderItemAsync(NewOrderItemDto orderItem)
            {
            var orderItemEntity = _mapper.Map<OrderItem>(orderItem);
            _repoManager.OrderItemRepo.CreateOrderItem(orderItemEntity);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task UpdateOrderItemAsync(NewOrderItemDto orderItem)
        {
            var existingOrderItem = await _repoManager.OrderItemRepo.FindOrderItemById(orderItem.OrderItemId, true);
            if (existingOrderItem != null) {
                _mapper.Map(orderItem, existingOrderItem);
                _repoManager.OrderItemRepo.UpdateOrderItem(existingOrderItem);
                await _repoManager.SaveRepoDataAsync(); }
        }
        public async Task DeleteOrderItemAsync(Guid orderItemId)
        {
            var existingOrderItem = await _repoManager.OrderItemRepo.FindOrderItemById(orderItemId, true);
            if (existingOrderItem != null)
            {
                _repoManager.OrderItemRepo.DeleteOrderItem(existingOrderItem);
                await _repoManager.SaveRepoDataAsync();

            }
        }

    }
}
