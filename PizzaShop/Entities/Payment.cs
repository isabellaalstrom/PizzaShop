using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace PizzaShop.Entities
{
    public class Payment
    {
        [Required]
        [DisplayName("Card Holder")]
        public string CardHolder { get; set; }
        [Required]
        [DisplayName("Credit Card Number")]
        public string CreditCardNumber { get; set; }
        [Required]
        [DisplayName("Month")]
        public int ExpireMonth { get; set; }
        [Required]
        [DisplayName("Year")]
        public int ExpireYear { get; set; }
        [Required]
        [DisplayName("CVV")]
        public string Cvv { get; set; }
    }
}
