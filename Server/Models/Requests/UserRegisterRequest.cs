using System.ComponentModel.DataAnnotations;

namespace Server.Models.Requests
{
    public class UserRegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$",
            MatchTimeoutInMilliseconds = 200,
            ErrorMessage = "Password must contain at least one letter, one number and one special character")]
        public string Password { get; set; } = string.Empty;
    }
}
