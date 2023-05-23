namespace Server.Models.Requests
{
    public class UserLoginRequest
    {
        public string Email { get; set; } = string.Empty;

        //"^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$"
        public string Password { get; set; } = string.Empty;
    }
}
