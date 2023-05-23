using Server.Contexts;
using Server.Models;

namespace Server.Services.BreakTimeService
{
    public class BreakTimeValidator
    {
        public readonly ApplicationContext _context;
        public BreakTimeValidator(ApplicationContext context)
        {
            _context = context;
        }

        public Validator Add(BreakTime breakTime)
        {
            return BaseMethod(breakTime);
        }

        public Validator Update(BreakTime breakTime)
        {
            return BaseMethod(breakTime);
        }

        private Validator BaseMethod(BreakTime breakTime)
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
            condition = IsWithinOtherBreakTimes(breakTime);
            if (condition is true)
            {
                return new(errorMessage: "BreakTime overlaps other breakTimes");
            }
            return new(isValid: true);
        }

        private bool DoesScheduleExist(BreakTime breakTime)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == breakTime.ScheduleId);
            return query is not null;
        }

        private bool IsWithinSchedule(BreakTime breakTime)
        {
            var schedule = _context.Schedules.First(x => x.Id == breakTime.ScheduleId);
            return TimeRepresentation.IsCompletelyInserted(
                contained: breakTime, contains: schedule);
        }

        private bool IsWithinOtherBreakTimes(BreakTime breakTime)
        {
            var structure = _context.BreakTimes
                .Where(x => x.ScheduleId == breakTime.ScheduleId
                && x.Id != breakTime.Id)
                .ToList();

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
