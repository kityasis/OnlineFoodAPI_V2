using OnlineFood.Data.Common;
using System;

namespace OnlineFood.API.ViewModels
{
    public class ProductViewModel : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Product { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Mrp { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int StarRating { get; set; }
        public int categoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
