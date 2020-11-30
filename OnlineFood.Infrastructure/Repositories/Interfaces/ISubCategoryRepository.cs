using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Infrastructure.Repositories.Interfaces
{
   public interface ISubCategoryRepository
    {
        IEnumerable<SubCategory> GetAllSubCategory();
        IEnumerable<SubCategory> GetAllSubCategoryUserId(string userId);
        SubCategory GetSubCategoryById(int id);
        void Insert(SubCategory entity);
        void Update(SubCategory entity);
        void Delete(SubCategory entity);
    }
}
