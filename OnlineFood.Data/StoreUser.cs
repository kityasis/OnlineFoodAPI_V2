using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace OnlineFood.Data
{
    public class StoreUser : IdentityUser
    {
        public int UserType { get; set; }
        public String Titel { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String Zipcode { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String AdditionalInformation { get; set; }        
    }
}
