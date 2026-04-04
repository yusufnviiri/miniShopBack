using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<ShowOrderItemDto>> GetAllOrderItems();
        Task<OrderItem?> FindOrderItemById(Guid OrderId, bool tracking);
        void CreateOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);

    }
}
