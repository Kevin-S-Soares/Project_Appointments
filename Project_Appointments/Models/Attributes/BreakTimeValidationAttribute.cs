using System.ComponentModel.DataAnnotations;

namespace Project_Appointments.Models.Attributes
{
    public class BreakTimeValidationAttribute : ValidationAttribute
    {
        private BreakTime model = default!;
        public override bool IsValid(object? value)
        {
            if(value is not BreakTime)
            {
                ErrorMessage = "Invalid BreakTime";
                return false;
            }
            model = (BreakTime) value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsScheduleIdValid()
                && AreTimesValid();
        }

        private bool IsIdValid()
        {
            bool condition = model.Id >= 0L;
            if(condition is false)
            {
                ErrorMessage = "Invalid BreakTime.Id";
            }
            return condition;
        }

        private bool IsScheduleIdValid()
        {
            bool condition = model.ScheduleId >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.ScheduleId";
            }
            return condition;
        }

        private bool AreTimesValid()
        {
            bool condition = model.StartTime < model.EndTime;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.StartTime or BreakTime.EndTime";
            }
            return condition;
        }
    }
}
