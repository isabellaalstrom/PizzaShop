using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class CreatePaymentViewModel
    {
        public int PaymentId { get; set; }
        [Required]
        [DisplayName("Card holder's name")]
        public string CardHolder { get; set; }
        [DisplayName("Card number")]
        [Required, RegularExpression("^[0-9]{10,16}$", ErrorMessage = "Please enter your card number (10-16 digits)")]
        public string CreditCardNumber { get; set; }
        [Required]
        [DisplayName("Month of expiration")]
        public int ExpireMonth { get; set; }
        [Required]
        [DisplayName("Year of expiration")]
        public int ExpireYear { get; set; }
        [DisplayName("CVV code")]
        [Required, RegularExpression("^[0-9]{3}$", ErrorMessage = "Please enter your CVV code (3 digits)")]
        public string Cvv { get; set; }

        public int Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}