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
    public class OrderRepo : RepositoryBase<Order>, IOrderRepo
    {
        public OrderRepo(ApplicationDbContext _db) : base(_db)
        {

        }
       public async Task<IEnumerable<ShowOrderDto>> GetAllOrders()
        {
            return await FindAll(false).Select(o=>new ShowOrderDto { OrderId = o.OrderId,
                ApplicationUserId = o.ApplicationUserId,
                DestinationAddress = o.DestinationAddress,
                ContactPerson = o.ContactPerson,
                OrderDate = o.OrderDate,
                OrderStatusDetails = o.OrderStatusDetails,
                Subtotal = o.SubTotal,
                DeliveryFee = o.DeliveryFee,
                DiscountAmount = o.DiscountAmount,
                TotalAmount = o.TotalAmount,
                PayementMode = o.PayementMode,
                Payment = o.Payment,
                OrderItems = o.OrderItems.Select(oi=> new ShowOrderItemDto {
                    OrderItemId= oi.OrderItemId,
                    OrderId= oi.OrderId,
                    ProductId= oi.ProductId,
                    ProductName= oi.ProductName!,
                    UnitPrice= oi.UnitPrice,
                    Quantity= oi.Quantity,
                    Total= oi.OrderItemTotal
                }).ToList()
            }).ToListAsync();
        }
        public  Task<Order?> FindOrderById(Guid OrderId, bool tracking)
        {
            return  FindByCondition(o => o.OrderId.Equals(OrderId), tracking)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ShowOrderDto>> FindOrdersByDate(DateTime orderDate)
        {
            var start = orderDate.Date;
            var end = start.AddDays(1);

            var query = FindByCondition(o => o.OrderDate >= start && o.OrderDate < end, false);
            return await query.Select(o => new ShowOrderDto
                
                  { OrderId = o.OrderId,
                ApplicationUserId = o.ApplicationUserId,
                DestinationAddress = o.DestinationAddress,
                ContactPerson = o.ContactPerson,
                OrderDate = o.OrderDate,
                OrderStatusDetails = o.OrderStatusDetails,
                Subtotal = o.SubTotal,
                DeliveryFee = o.DeliveryFee,
                DiscountAmount = o.DiscountAmount,
                TotalAmount = o.TotalAmount,
                PayementMode = o.PayementMode,
                Payment = o.Payment,
                OrderItems = o.OrderItems.Select(oi=> new ShowOrderItemDto {
                    OrderItemId= oi.OrderItemId,
                    OrderId= oi.OrderId,
                    ProductId= oi.ProductId,
                    ProductName= oi.ProductName!,
                    UnitPrice= oi.UnitPrice,
                    Quantity= oi.Quantity,
                    Total= oi.OrderItemTotal
                    }).ToList()
                }).ToListAsync();
        }
        public void CreateOrder(Order order)=> CreateBase(order);
        public void UpdateOrder(Order order)=> UpdateBase(order);
        public void DeleteOrder(Order order)=> DeleteBase(order);
    }
}