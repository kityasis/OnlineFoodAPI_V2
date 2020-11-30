using OnlineFood.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class Category
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> SubCategory { get; set; }    
    }
}
