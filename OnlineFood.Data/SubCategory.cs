using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
