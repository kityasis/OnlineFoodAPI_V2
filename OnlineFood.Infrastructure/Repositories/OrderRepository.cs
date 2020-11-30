using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories
{

    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly OnlineFoodContext _context;
        public OrderRepository(OnlineFoodContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }       

        public void Delete(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.OrderItems.Remove(entity);
            _context.SaveChanges();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return _context.Orders.Include("OrderItem").ToList();
        }

        public IEnumerable<Order> GetAllOrderUserId(string userId)
        {
            return _context.Orders.Include("OrderItem").Where(x=>x.UserId==userId).ToList();
        }

        public OrderItem GetItemById(int id)
        {
            return _context.OrderItems.Where(x=>x.Id==id).FirstOrDefault();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<OrderItem> GetOrderItemById(int id)
        {
            return _context.OrderItems.Include("Product").ToList().Where(s => s.OrderId == id);
        }

        public void Insert(Order entity, List<OrderItem> items)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Orders.Add(entity);
                _context.SaveChanges();
                items.ForEach(x => x.OrderId = entity.Id);
                _context.OrderItems.AddRange(items);
                _context.SaveChanges();
                transaction.Commit();
            }
        }

        public void Update(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.OrderItems.Update(entity);
            _context.SaveChanges();
        }

        public void Update(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Orders.Update(entity);
            _context.SaveChanges();
        }
    }
}
