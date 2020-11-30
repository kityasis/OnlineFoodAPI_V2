using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineFood.API.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        public String Titel { get; set; }
        [Required]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [Required]
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        [Required]
        public String PhoneNumber { get; set; }
        [Required]
        public String Zipcode { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String Country { get; set; }
        public String AdditionalInformation { get; set; }
    }
}
