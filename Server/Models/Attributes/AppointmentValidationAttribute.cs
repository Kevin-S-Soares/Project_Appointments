using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Server.Models.Attributes
{
    public class AppointmentValidationAttribute : ValidationAttribute
    {
        private static readonly string s_regexName = @"^[A-Z][a-z]*(\s[A-Z][a-z]*)+$";
        private static readonly TimeSpan s_timeout = TimeSpan.FromMilliseconds(200);

        private Appointment _model = default!;

        public override bool IsValid(object? value)
        {
            if (value is not Appointment)
            {
                ErrorMessage = "Invalid Appointment";
                return false;
            }
            _model = (Appointment)value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsScheduleIdValid()
                && IsPatientNameValid();
        }

        private bool IsIdValid()
        {
            bool condition = _model.Id >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Appointment.Id";
            }
            return condition;
        }

        private bool IsScheduleIdValid()
        {
            bool condition = _model.ScheduleId >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Appointment.ScheduleId";
            }
            return condition;
        }

        private bool IsPatientNameValid()
        {
            bool condition =
                Regex.IsMatch(input: _model.PatientName, pattern: s_regexName,
                    options: RegexOptions.None, matchTimeout: s_timeout);

            if (condition is false)
            {
                ErrorMessage = "Invalid Appointment.PatientName";
            }
            return condition;
        }
    }
}
