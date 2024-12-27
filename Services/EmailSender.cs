using Microsoft.AspNetCore.Identity.UI.Services;

namespace SCRSApplication.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email,string subject,string htmlMessage)
        {
             //Email Sending logic has to implement here
            Console.WriteLine($"Sending email to '{email}' with subject '{subject}' ");
            return Task.CompletedTask;
        }
    }
}
