﻿using System.Threading.Tasks;

namespace PizzaShop.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
