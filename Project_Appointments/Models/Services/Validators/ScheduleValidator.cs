using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;

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
            var structure = _context.Schedules
                .Where(x => x.OdontologistId == schedule.OdontologistId)
                .ToList();

            foreach(var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(schedule, element);
                if(condition is true)
                {
                    throw new ModelException("Schedule overlaps other schedules");
                }
            }
        }

        public void Update(Schedule schedule)
        {
            var structure = _context.Schedules
                .Where(x => x.OdontologistId == schedule.OdontologistId)
                .ToList();

            structure.Remove(schedule);
            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(schedule, element);
                if (condition is true)
                {
                    throw new ModelException("Schedule overlaps other schedules");
                }
            }
        }
    }
}
