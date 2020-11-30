
using OnlineFood.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class OrderItem : Entity<int>
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
