using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;

namespace Project_Appointments.Models.Services.Validators
{
    public class BreakTimeValidator
    {
        private readonly ApplicationContext _context;
        public BreakTimeValidator(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(BreakTime breakTime)
        {
            bool condition = IsWithinSchedule(breakTime);
            if (condition is false)
            {
                throw new ModelException("BreakTime is not within its referred schedule");
            }

            condition = IsWithinOtherBreakTimes(breakTime);
            if (condition is true)
            {
                throw new ModelException("BreakTime overlaps other breakTimes");
            }
        }

        public void Update(BreakTime breakTime)
        {
            bool condition = IsWithinSchedule(breakTime);
            if (condition is false)
            {
                throw new ModelException("BreakTime is not within its referred schedule");
            }

            condition = IsWithinOtherBreakTimes(breakTime, isToUpdate: true);
            if (condition is true)
            {
                throw new ModelException("BreakTime overlaps other breakTimes");
            }
        }

        private bool IsWithinSchedule(BreakTime breakTime)
        {
            Schedule schedule;
            try
            {
                schedule = _context.Schedules
                    .Where(x => x.Id == breakTime.ScheduleId)
                    .First();
            }
            catch (Exception)
            {
                throw new ModelException("Invalid referred schedule");
            }
            return TimeRepresentation.IsCompletelyInserted(
                contained: breakTime, contains: schedule);
        }

        private bool IsWithinOtherBreakTimes(BreakTime breakTime, bool isToUpdate = false)
        {
            var structure = _context.BreakTimes
                .Where(x => x.ScheduleId == breakTime.ScheduleId)
                .ToList();

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
