using OnlineFood.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineFood.Data
{
    public class UserSetting : Entity<int>
    {
        public string Pincodes { get; set; }
        public decimal FreeShipingCost { get; set; }
    }
}
