using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Services
{ 
    public class FakeEmailSender : IEmailSender
    {
        //todo injecta i controller som ska skicka mail, skulle nåt mer göras här också?
        public Task SendEmailAsync(string email, string subject, string message)
        {
            Debug.WriteLine($"{email}{subject}{message}");
            return Task.CompletedTask;
        }
    }
}
