using OnlineFood.Data;
using OnlineFood.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository, IDisposable
    {
        private readonly OnlineFoodContext _context;
        public SubCategoryRepository(OnlineFoodContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Delete(SubCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.SubCategories.Remove(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public IEnumerable<SubCategory> GetAllSubCategory()
        {
            return _context.SubCategories.Include("Category").ToList();
        }

        public IEnumerable<SubCategory> GetAllSubCategoryUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public SubCategory GetSubCategoryById(int id)
        {
            return _context.SubCategories.Where(s => s.Id == id).FirstOrDefault();
        }

        public void Insert(SubCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.SubCategories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(SubCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.SubCategories.Update(entity);
            _context.SaveChanges();
        }
    }
}
