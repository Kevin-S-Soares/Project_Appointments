using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Models.Services.Validators;

namespace Project_Appointments.Models.Services
{
    public class BreakTimeService
    {
        private readonly ApplicationContext _context;
        private readonly BreakTimeValidator _validator;

        public BreakTimeService(ApplicationContext context)
        {
            _context = context;
            _validator = new(_context);
        }

        public BreakTime Add(BreakTime breakTime)
        {
            try
            {
                _validator.Add(breakTime);
                _context.BreakTimes.Add(breakTime);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception adding breakTime: " + e.Message);
            }
            return breakTime;
        }

        public void Update(BreakTime breakTime)
        {
            try
            {
                BreakTime result = FindById(breakTime.Id);
                _validator.Update(breakTime);
                _context.BreakTimes.Update(breakTime);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception updating breakTime: BreakTime not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception updating breakTime: " + e.Message);
            }
        }

        public void Delete(long id)
        {
            try
            {
                BreakTime result = FindById(id);
                _context.BreakTimes.Remove(result);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception deleting breakTime: BreakTime not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception deleting breakTime: " + e.Message);
            }
        }

        public BreakTime FindById(long id)
        {
            var result = _context.BreakTimes.FirstOrDefault(x => x.Id == id);
            return result is null ? throw new ServiceException("Exception: BreakTime not found") : result;
        }

        public ICollection<BreakTime> FindAll()
        {
            return _context.BreakTimes.ToList();
        }
    }
}
