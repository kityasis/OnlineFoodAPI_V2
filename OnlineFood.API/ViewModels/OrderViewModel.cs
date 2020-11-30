using OnlineFood.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineFood.API.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        [MinLength(4)]
        public string OrderNumber { get; set; }
        public decimal SumTotal { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
