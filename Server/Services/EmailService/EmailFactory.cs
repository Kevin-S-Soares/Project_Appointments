using Server.Models;

namespace Server.Services.EmailService
{
    public class EmailFactory
    {
        public static Email CreateEmailFromNewUser(User user)
        {
            return new()
            {
                To = "alexzander.jakubowski@ethereal.email",
                From = "alexzander.jakubowski@ethereal.email",
                Subject = "Your account has been registered!",
                Body = user.VerificationToken!
            };
        }
    }
}
