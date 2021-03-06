﻿using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.AccountViewModels
{
    public class RegisterViewModel
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

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
    }
}
