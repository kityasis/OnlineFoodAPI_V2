using OnlineFood.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int SubCategoryId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Mrp { get; set; }
        public decimal Price { get; set; }        
        public DateTime ReleaseDate { get; set; }
        public SubCategory SubCategory { get; set; }

    }
}
