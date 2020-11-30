using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
   public interface IProductRepositry
    {
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetAllProductUserId(string userId);
        Product GetProductById(int id);
        void Insert(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
