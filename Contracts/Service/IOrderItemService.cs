using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Service
{
    public interface IOrderItemService
    {
        Task<IEnumerable<ShowOrderItemDto>> GetAllOrderItemsAsync();
        Task<OrderItem?> FindOrderItemByIdAsync(Guid OrderId, bool tracking);
        Task CreateOrderItemAsync(NewOrderItemDto orderItem);
        Task UpdateOrderItemAsync(NewOrderItemDto orderItem);
        Task DeleteOrderItemAsync(Guid orderItemId);
    }
}
