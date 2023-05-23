using System.ComponentModel.DataAnnotations;

namespace Server.Models.Attributes
{
    public class BreakTimeValidationAttribute : ValidationAttribute
    {
        private BreakTime _model = default!;
        public override bool IsValid(object? value)
        {
            if (value is not BreakTime)
            {
                ErrorMessage = "Invalid BreakTime";
                return false;
            }
            _model = (BreakTime)value;
            return ArePropertiesValid();
        }

        private bool ArePropertiesValid()
        {
            return IsIdValid()
                && IsScheduleIdValid()
                && IsStartDayValid()
                && IsEndDayValid();
        }

        private bool IsIdValid()
        {
            bool condition = _model.Id >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.Id";
            }
            return condition;
        }

        private bool IsScheduleIdValid()
        {
            bool condition = _model.ScheduleId >= 0L;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.ScheduleId";
            }
            return condition;
        }

        private bool IsStartDayValid()
        {
            int aux = Convert.ToInt32(_model.StartDay);
            bool condition = aux >= 0 && aux <= 6;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.StartDay";
            }
            return condition;
        }

        private bool IsEndDayValid()
        {
            int aux = Convert.ToInt32(_model.EndDay);
            bool condition = aux >= 0 && aux <= 6;
            if (condition is false)
            {
                ErrorMessage = "Invalid BreakTime.EndDay";
            }
            return condition;
        }
    }
}
