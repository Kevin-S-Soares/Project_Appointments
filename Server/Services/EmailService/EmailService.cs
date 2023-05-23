using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Server.Models;

namespace Server.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public ServiceResponse<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<bool>> SendEmailAsync(Email email)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email.From));
            message.To.Add(MailboxAddress.Parse(email.To));
            message.Subject = email.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("alexzander.jakubowski@ethereal.email", "KT1UCAJTbxsfHt2AVZ");
            await smtp.SendAsync(message);

            return new(data: true, statusCode: StatusCodes.Status200OK);
        }
    }
}
