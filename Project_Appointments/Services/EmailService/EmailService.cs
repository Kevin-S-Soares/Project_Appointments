using Project_Appointments.Models;

namespace Project_Appointments.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public ServiceResponse<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> SendEmailAsync(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
