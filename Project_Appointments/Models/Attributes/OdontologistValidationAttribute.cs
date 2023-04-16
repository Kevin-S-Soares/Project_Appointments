using Project_Appointments.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Appointments_Project.Models.Attributes
{
    public class OdontologistValidationAttribute : ValidationAttribute
    {

        private static readonly string s_regexName = @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$";
        private static readonly string s_regexEmail = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        private static readonly string s_regexPhone = @"^\(0[1-9][1-9]\)\s9?\d{4}-\d{4}$";
        private static readonly TimeSpan s_timeout = TimeSpan.FromMilliseconds(200);

        private Odontologist _model = default!;

        public override bool IsValid(object? value)
        {
            if (value is not Odontologist)
            {
                ErrorMessage = "Invalid Odontologist";
                return false;
            }
            _model = (Odontologist)value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsNameValid()
                && IsEmailValid()
                && IsTelephoneNumberValid();
        }

        private bool IsIdValid()
        {
            bool condition = _model.Id >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Odontologist.Id";
            }
            return condition;
        }

        private bool IsNameValid()
        {
            bool condition =
                Regex.IsMatch(input: _model.Name, pattern: s_regexName,
                    options: RegexOptions.None, matchTimeout: s_timeout);

            if (condition is false)
            {
                ErrorMessage = "Invalid Odontologist.Name";
            }
            return condition;
        }

        private bool IsEmailValid()
        {
            bool condition =
                Regex.IsMatch(input: _model.Email, pattern: s_regexEmail,
                    options: RegexOptions.None, matchTimeout: s_timeout);

            if (condition is false)
            {
                ErrorMessage = "Invalid Odontologist.Email";
            }
            return condition;
        }

        private bool IsTelephoneNumberValid()
        {
            bool condition =
                Regex.IsMatch(input: _model.Phone, pattern: s_regexPhone,
                    options: RegexOptions.None, matchTimeout: s_timeout);

            if (condition is false)
            {
                ErrorMessage = "Invalid Odontologist.Phone";
            }
            return condition;
        }
    }
}
