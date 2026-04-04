using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<ShowOrderDto>> GetAllOrdersAsync();
        Task<Order?> FindOrderByIdAsync(Guid OrderId, bool tracking);
        Task<IEnumerable<ShowOrderDto>> FindOrdersByDateAsync(DateTime orderDate);
        Task CreateOrderAsync(NewOrderDto order);
        Task UpdateOrderAsync(NewOrderDto order);
        Task DeleteOrderAsync(Guid orderId);
    }
}
