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
            List<Schedule> schedules = _context.Schedules.ToList();
            foreach(var obj in schedules)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(schedule, obj);
                if(condition is true)
                {
                    throw new ModelException("Schedule overlaps other schedules");
                }
            }
        }

        public void Update(Schedule schedule)
        {
            List<Schedule> schedules = _context.Schedules.ToList();
            schedules.Remove(schedule);
            foreach (var obj in schedules)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(schedule, obj);
                if (condition is true)
                {
                    throw new ModelException("Schedule overlaps other schedules");
                }
            }
        }
    }
}
