using Project_Appointments.Models;
using Project_Appointments.Services.ScheduleService;

namespace Project_Appointments.Services.BreakTimeService
{
    public class BreakTimeValidator
    {
        private readonly IBreakTimeService _breakTimeService;
        private readonly IScheduleService _scheduleService;
        public BreakTimeValidator(IBreakTimeService breakTimeService,
            IScheduleService scheduleService)
        {
            _breakTimeService = breakTimeService;
            _scheduleService = scheduleService;
        }

        public Validator Add(BreakTime breakTime)
        {
            return BaseMethod(breakTime);
        }

        public Validator Update(BreakTime breakTime)
        {
            return BaseMethod(breakTime, isToUpdate: true);
        }

        private Validator BaseMethod(BreakTime breakTime, bool isToUpdate = false)
        {
            bool condition = DoesScheduleExist(breakTime);
            if (condition is false)
            {
                return new("Invalid referred schedule");
            }
            condition = IsWithinSchedule(breakTime);
            if (condition is false)
            {
                return new(errorMessage: "BreakTime is not within its referred schedule");
            }
            condition = IsWithinOtherBreakTimes(breakTime, isToUpdate);
            if (condition is true)
            {
                return new(errorMessage: "BreakTime overlaps other breakTimes");
            }
            return new(isValid: true);
        }

        private bool DoesScheduleExist(BreakTime breakTime)
        {
            var query = _scheduleService.FindById(breakTime.ScheduleId).Data;
            return query is not null;
        }

        private bool IsWithinSchedule(BreakTime breakTime)
        {
            var schedule = _scheduleService.FindById(breakTime.Id).Data!;
            return TimeRepresentation.IsCompletelyInserted(
                contained: breakTime, contains: schedule);
        }

        private bool IsWithinOtherBreakTimes(BreakTime breakTime, bool isToUpdate = false)
        {
            var structure = _breakTimeService.FindAllFromSameSchedule(breakTime).Data!.ToList();

            if (isToUpdate)
            {
                structure.Remove(breakTime);
            }

            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(
                    contained: breakTime, contains: element);
                condition = condition || TimeRepresentation.IsPartiallyInserted(
                    contained: element, contains: breakTime);
                if (condition is true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
