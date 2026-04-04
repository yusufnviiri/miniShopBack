using Entities.Models;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repo
{
    public interface IOrderRepo
    {
        Task<IEnumerable<ShowOrderDto>> GetAllOrders();
        Task<Order?> FindOrderById(Guid OrderId, bool tracking);
        Task<IEnumerable<ShowOrderDto>> FindOrdersByDate(DateTime orderDate);
        void CreateOrder(Order order);
        void UpdateOrder(Order order );
        void DeleteOrder(Order order);
    }
}
