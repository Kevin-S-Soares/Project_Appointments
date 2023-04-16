using System.ComponentModel.DataAnnotations;

namespace Project_Appointments.Models.Attributes
{
    public class ScheduleValidationAttribute : ValidationAttribute
    {
        private Schedule model = default!;
        public override bool IsValid(object? value)
        {
            if (value is not Schedule)
            {
                ErrorMessage = "Invalid Schedule";
                return false;
            }
            model = (Schedule) value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsOdontologistIdValid()
                && IsDayValid()
                && AreTimesValid();
        }

        private bool IsIdValid()
        {
            bool condition = model.Id >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Schedule.Id";
            }
            return condition;
        }

        private bool IsOdontologistIdValid()
        {
            bool condition = model.OdontologistId >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid Schedule.OdontologistId";
            }
            return condition;
        }

        private bool IsDayValid()
        {
            int aux = Convert.ToInt32(model.Day);
            bool condition = aux >= 0 && aux <= 6;
            if(condition is false)
            {
                ErrorMessage = "Invalid Schedule.Day";
            }
            return condition;
        }

        private bool AreTimesValid()
        {
            bool condition = model.StartTime < model.EndTime;
            if(condition is false)
            {
                ErrorMessage = "Invalid Schedule.StartTime or Schedule.EndTime";
            }
            return condition;
        }
    }
}
