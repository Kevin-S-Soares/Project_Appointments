using Project_Appointments.Models;

namespace Project_Appointments.Services.EmailService
{
    public interface IEmailService
    {
        public ServiceResponse<bool> SendEmail(Email email);
        public Task<ServiceResponse<bool>> SendEmailAsync(Email email);
    }
}
