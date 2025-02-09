using Microsoft.AspNetCore.Identity.UI.Services;

namespace SmprMvcApp.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //görevi döndür
            return Task.CompletedTask;
        }
    }
}