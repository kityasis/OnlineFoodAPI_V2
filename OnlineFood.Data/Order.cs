using OnlineFood.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class Order : Entity<int>
    {
        public DateTime Date { get; set; }
        public string Number { get; set; }        
        public string UserId { get; set; }
        public decimal SumTotal { get; set; }
        public bool approved { get; set; }
        public ICollection<OrderItem> OrderItem { get; set; }
    }
}
