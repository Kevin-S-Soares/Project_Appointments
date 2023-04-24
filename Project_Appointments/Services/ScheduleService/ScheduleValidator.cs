using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Exceptions;

namespace Project_Appointments.Services.ScheduleService
{
    public class ScheduleValidator
    {
        private readonly ApplicationContext _context;
        public ScheduleValidator(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Schedule schedule)
        {
            BaseMethod(schedule);
        }

        public void Update(Schedule schedule)
        {
            BaseMethod(schedule, isToUpdate: true);
        }

        private void BaseMethod(Schedule schedule, bool isToUpdate = false)
        {
            bool condition = DoesOdontologistExists(schedule);
            if (condition is false)
            {
                throw new ModelException("Invalid referred odontologist");
            }
            condition = IsWithinOtherSchedule(schedule, isToUpdate);
            if (condition is true)
            {
                throw new ModelException("Schedule overlaps other schedules");
            }
        }

        private bool DoesOdontologistExists(Schedule schedule)
        {
            try
            {
                _context.Odontologists.Where(x => x.Id == schedule.OdontologistId).First();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool IsWithinOtherSchedule(Schedule schedule, bool isToUpdate = false)
        {
            var structure = _context.Schedules
                .Where(x => x.OdontologistId == schedule.OdontologistId)
                .ToList();

            if (isToUpdate)
            {
                structure.Remove(schedule);
            }

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
