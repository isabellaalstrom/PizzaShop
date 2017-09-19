using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
