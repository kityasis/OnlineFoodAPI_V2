using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly OnlineFoodContext _context;
        public CategoryRepository(OnlineFoodContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Delete(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IEnumerable<Category> GetAllCategory()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<Category> GetAllCategoryUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategoriesWithSubCategory()
        {
            return _context.Categories.Include("SubCategory").ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(s => s.Id == id).FirstOrDefault();
        }

        public void Insert(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Categories.Update(entity);
            _context.SaveChanges();
        }
    }
}
