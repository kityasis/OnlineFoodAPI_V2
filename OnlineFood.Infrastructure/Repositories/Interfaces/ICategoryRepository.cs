using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategoriesWithSubCategory();
        IEnumerable<Category> GetAllCategory();
        IEnumerable<Category> GetAllCategoryUserId(string userId);
        Category GetCategoryById(int id);
        void Insert(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
