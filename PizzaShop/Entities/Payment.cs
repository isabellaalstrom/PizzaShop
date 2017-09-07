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
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public int ExpireMonth { get; set; }
        [Required]
        public int ExpireYear { get; set; }
        [Required]
        public string Cvv { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
