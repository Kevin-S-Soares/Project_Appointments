using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Models.Services.Validators;

namespace Project_Appointments.Models.Services
{
    public class ScheduleService
    {
        private readonly ApplicationContext _context;
        private readonly ScheduleValidator _validator;

        public ScheduleService(ApplicationContext context)
        {
            _context = context;
            _validator = new(_context);
        }

        public Schedule Add(Schedule schedule)
        {      
            try
            {
                _validator.Add(schedule);
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception adding schedule: " + e.Message);
            }
            return schedule;
        }

        public void Update(Schedule schedule)
        {
            try
            {
                Schedule result = FindById(schedule.Id);
                _validator.Update(result);
                _context.Schedules.Update(schedule);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception updating schedule: Schedule not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception updating schedule: " + e.Message);
            }
        }

        public void Delete(long id)
        {
            try
            {
                Schedule result = FindById(id);
                _context.Schedules.Remove(result);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception deleting schedule: Schedule not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception deleting schedule: " + e.Message);
            }
        }

        public Schedule FindById(long id)
        {
            var result = _context.Schedules.FirstOrDefault(x => x.Id == id);
            return result is null ? throw new ServiceException("Exception: Schedule not found") : result;
        }

        public ICollection<Schedule> FindAll()
        {
            return _context.Schedules.ToList();
        }
    }
}
