using Server.Models;

namespace Server.Services.EmailService
{
    public interface IEmailService
    {
        public ServiceResponse<bool> SendEmail(Email email);
        public Task<ServiceResponse<bool>> SendEmailAsync(Email email);
    }
}
