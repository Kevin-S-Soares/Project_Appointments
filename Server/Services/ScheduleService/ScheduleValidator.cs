using Server.Contexts;
using Server.Models;

namespace Server.Services.ScheduleService
{
    public class ScheduleValidator
    {
        private readonly ApplicationContext _context;
        public ScheduleValidator(ApplicationContext context)
        {
            _context = context;
        }

        public Validator Add(Schedule schedule)
        {
            return BaseMethod(schedule);
        }

        public Validator Update(Schedule schedule)
        {
            return BaseMethod(schedule);
        }

        private Validator BaseMethod(Schedule schedule)
        {
            bool condition = DoesOdontologistExists(schedule);
            if (condition is false)
            {
                return new("Invalid referred odontologist");
            }

            condition = IsWithinOtherSchedule(schedule);
            if (condition is true)
            {
                return new("Schedule overlaps other schedules");
            }

            return new(true);
        }

        private bool DoesOdontologistExists(Schedule schedule)
        {
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == schedule.OdontologistId);
            return query is not null;
        }

        private bool IsWithinOtherSchedule(Schedule schedule)
        {
            var structure = _context.Schedules
                .Where(x => x.Id != schedule.Id
                && x.OdontologistId == schedule.OdontologistId)
                .ToList();


            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(
                    contained: schedule, contains: element);
                condition = condition || TimeRepresentation.IsPartiallyInserted(
                    contained: element, contains: schedule);

                if (condition is true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
