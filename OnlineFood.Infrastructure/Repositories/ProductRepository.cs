using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepositry, IDisposable
    {
        private readonly OnlineFoodContext _context;
        public ProductRepository(OnlineFoodContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Delete(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Products.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IEnumerable<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetAllProductUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Include("SubCategory").Where(s => s.Id == id).FirstOrDefault();
        }

        public void Insert(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Products.Update(entity);
            _context.SaveChanges();
        }
    }
}
