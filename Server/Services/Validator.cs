namespace Server.Services
{
    public class Validator
    {
        public Validator() { }
        public Validator(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public Validator(bool isValid)
        {
            IsValid = isValid;
        }
        public bool IsValid { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
