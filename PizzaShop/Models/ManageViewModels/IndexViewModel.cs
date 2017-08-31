using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "You have to add a name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "You have to add a delivery address.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "You have to add a zipcode.")]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "You have to add a city.")]
        [Display(Name = "City")]
        public string City { get; set; }

        public string StatusMessage { get; set; }
    }
}
