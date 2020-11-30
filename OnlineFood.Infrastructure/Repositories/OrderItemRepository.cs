using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public void Delete(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAllItem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAllItemUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public OrderItem GetItemById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderItem GetShopById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
