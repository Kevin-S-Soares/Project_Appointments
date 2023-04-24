using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Models.Services.Validators;

namespace Project_Appointments.Models.Services
{
    public class AppointmentService
    {
        private readonly ApplicationContext _context;
        private readonly AppointmentValidator _validator;

        public AppointmentService(ApplicationContext context)
        {
            _context = context;
            _validator = new(_context);
        }

        public Appointment Add(Appointment appointment)
        {
            try
            {
                _validator.Add(appointment);
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception adding appointment: " + e.Message);
            }
            return appointment;
        }

        public void Update(Appointment appointment)
        {
            try
            {
                Appointment result = FindById(appointment.Id);
                _validator.Update(appointment);
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception updating appointment: Appointment not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception updating appointment: " + e.Message);
            }
        }

        public void Delete(long id)
        {
            try
            {
                Appointment result = FindById(id);
                _context.Appointments.Remove(result);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception deleting appointment: Appointment not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception deleting appointment: " + e.Message);
            }
        }

        public Appointment FindById(long id)
        {
            var result = _context.Appointments.FirstOrDefault(x => x.Id == id);
            return result is null ? throw new ServiceException("Exception: Appointment not found") : result;
        }

        public ICollection<Appointment> FindAll()
        {
            return _context.Appointments.ToList();
        }
    }
}
