using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace PizzaShop.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required, MinLength(10), MaxLength(16)]
        public string CreditCardNumber { get; set; }
        [Required]
        public int ExpireMonth { get; set; }
        [Required]
        public int ExpireYear { get; set; }
        [Required, MinLength(3), MaxLength(3)]
        public string Cvv { get; set; }

        public int Amount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
