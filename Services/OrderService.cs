using AutoMapper;
using Contracts;
using Contracts.Repo;
using Contracts.Service;
using Entities.Exceptions;
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
    internal sealed class OrderService:IOrderService
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repoManager;
        private readonly IMapper _mapper;
        private ApplicationUser? _user = new();
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
            _repoManager = repository;
            _mapper = mapper;
        }

      public async  Task<IEnumerable<ShowOrderDto>> GetAllOrdersAsync()=>
            await _repoManager.OrderRepo.GetAllOrders();
        public  Task<Order?> FindOrderByIdAsync(Guid OrderId, bool tracking)=>
             _repoManager.OrderRepo.FindOrderById(OrderId, tracking);
        public async Task<IEnumerable<ShowOrderDto>> FindOrdersByDateAsync(DateTime orderDate)=>
            await _repoManager.OrderRepo.FindOrdersByDate(orderDate);
        public async Task CreateOrderAsync(NewOrderDto order)
            {
                var orderEntity = _mapper.Map<Order>(order);
                _repoManager.OrderRepo.CreateOrder(orderEntity);
                await _repoManager.SaveRepoDataAsync();
            }
        public async Task UpdateOrderAsync(NewOrderDto order)
        {
            var existingOrder = await _repoManager.OrderRepo.FindOrderById(order.OrderId, true);
            if (existingOrder == null)
            {
                throw new ItemNotFoundException(order.OrderId);
            }
            _mapper.Map(order, existingOrder);
            _repoManager.OrderRepo.UpdateOrder(existingOrder);
            await _repoManager.SaveRepoDataAsync();
        }
        public async Task DeleteOrderAsync(Guid orderId)
        {
            var existingOrder = await _repoManager.OrderRepo.FindOrderById(orderId, true);
            if (existingOrder == null)
            {
                throw new ItemNotFoundException(orderId);
            }
            _repoManager.OrderRepo.DeleteOrder(existingOrder);
            await _repoManager.SaveRepoDataAsync();

        }

    }
}
