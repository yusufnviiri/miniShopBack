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
    public class OrderItemRepo : RepositoryBase<OrderItem>, IOrderItemRepo
    {
        public OrderItemRepo(ApplicationDbContext _db) : base(_db)
        {

        }

       public async Task<IEnumerable<ShowOrderItemDto>> GetAllOrderItems()
        {
            return await FindAll(false).Select(oi => new ShowOrderItemDto
            {
                OrderItemId = oi.OrderItemId,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                ProductName = oi.ProductName,
                UnitPrice = oi.UnitPrice,
                Quantity = oi.Quantity,
                Total = oi.OrderItemTotal
            }).ToListAsync();
        }
        public  Task<OrderItem?> FindOrderItemById(Guid OrderItemId, bool tracking)
        {
            return  FindByCondition(p => p.OrderItemId == OrderItemId, tracking).FirstOrDefaultAsync();        }
        public void CreateOrderItem(OrderItem orderItem)=> CreateBase(orderItem);
        public void UpdateOrderItem(OrderItem orderItem)=> UpdateBase(orderItem);
        public void DeleteOrderItem(OrderItem orderItem)=> DeleteBase(orderItem);
    }
}