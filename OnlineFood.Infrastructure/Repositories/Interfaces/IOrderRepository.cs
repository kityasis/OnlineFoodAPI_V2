using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
   public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrder();
        IEnumerable<Order> GetAllOrderUserId(string userId);
        IEnumerable<OrderItem> GetOrderItemById(int id);
        OrderItem GetItemById(int id);
        Order GetOrderById(int id);
        void Insert(Order entity, List<OrderItem> enties);
        void Update(Order entity);
        void Delete(OrderItem entity);
    }
}
