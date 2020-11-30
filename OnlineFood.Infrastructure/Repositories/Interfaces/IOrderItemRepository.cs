using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
   public interface IOrderItemRepository
    {
        IEnumerable<OrderItem> GetAllItem();
        IEnumerable<OrderItem> GetAllItemUserId(string userId);
        OrderItem GetItemById(int id);
        void Insert(OrderItem entity);
        void Update(OrderItem entity);
        void Delete(OrderItem entity);
    }
}
