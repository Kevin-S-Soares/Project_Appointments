using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;
using System.Xml.Linq;

namespace Project_Appointments.Models.Services.Validators
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
            bool condition = IsWithinOtherSchedule(schedule);
            if (condition is true)
            {
                throw new ModelException("Schedule overlaps other schedules");
            }
        }

        public void Update(Schedule schedule)
        {
            bool condition = IsWithinOtherSchedule(schedule, isToUpdate: true);
            if (condition is true)
            {
                throw new ModelException("Schedule overlaps other schedules");
            }
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
                bool condition = TimeRepresentation.IsPartiallyInserted(schedule, element);
                if (condition is true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
