using System.Diagnostics;
using System.Threading.Tasks;

namespace PizzaShop.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Debug.WriteLine($"Confirmation email sent to {email}, with subject: \"{subject}\" and message \"{message}\"");
            return Task.CompletedTask;
        }
    }
}