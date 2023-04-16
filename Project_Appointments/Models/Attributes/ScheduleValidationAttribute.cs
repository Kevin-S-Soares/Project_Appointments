using System.ComponentModel.DataAnnotations;

namespace Project_Appointments.Models.Attributes
{
    public class ScheduleValidationAttribute : ValidationAttribute
    {
        private Schedule _model = default!;
        public override bool IsValid(object? value)
        {
            if (value is not Schedule)
            {
                ErrorMessage = "Invalid Schedule";
                return false;
            }
            _model = (Schedule) value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsOdontologistIdValid();
        }

        private bool IsIdValid()
        {
            bool condition = _model.Id >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Schedule.Id";
            }
            return condition;
        }

        private bool IsOdontologistIdValid()
        {
            bool condition = _model.OdontologistId >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Schedule.OdontologistId";
            }
            return condition;
        }
    }
}
