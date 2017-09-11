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
        [Required]
        [DisplayName("Card number")]
        public string CreditCardNumber { get; set; }
        [Required]
        [DisplayName("Month of expiration")]
        public int ExpireMonth { get; set; }
        [Required]
        [DisplayName("Year of expiration")]
        public int ExpireYear { get; set; }
        [Required]
        [DisplayName("CVV code")]
        public string Cvv { get; set; }

        public int Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
