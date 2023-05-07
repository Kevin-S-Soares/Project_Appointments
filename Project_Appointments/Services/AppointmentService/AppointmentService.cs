using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.ScheduleService;

namespace Project_Appointments.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext _context;
        private readonly AppointmentValidator _validator;

        public AppointmentService(ApplicationContext context,
            IBreakTimeService breakTimeService, IScheduleService scheduleService)
        {
            _context = context;
            _validator = new(scheduleService, breakTimeService, this);
        }

        public ServiceResponse<Appointment> Create(Appointment appointment)
        {
            var validator = _validator.Add(appointment);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _context.Appointments.Add(appointment);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, StatusCodes.Status500InternalServerError);
            }
            return new(data: appointment, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<Appointment>> CreateAsync(Appointment appointment)
        {
            var validator = _validator.Add(appointment);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            await _context.Appointments.AddAsync(appointment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, StatusCodes.Status500InternalServerError);
            }
            return new(data: appointment, statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<string> Delete(long id)
        {
            var query = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Appointment does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Appointments.Remove(query);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Appointment deleted", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> DeleteAsync(long id)
        {
            var query = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Appointment does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Appointments.Remove(query);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Appointment deleted", statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<Appointment>> FindAll()
        {
            var result = _context.Appointments;
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<Appointment>> FindAppointmentsFromSameDay(Appointment appointment)
        {
            var result = _context.Appointments
                .Where(x => x.Id != appointment.Id
                && x.ScheduleId == appointment.ScheduleId
                && ((x.Start.Year == appointment.Start.Year
                && x.Start.Month == appointment.Start.Month
                && x.Start.Day == appointment.Start.Day)
                || (x.End.Year == appointment.End.Year
                && x.End.Month == appointment.End.Month
                && x.End.Day == appointment.End.Day))).ToList();
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Appointment> FindById(long id)
        {
            var query = _context.Appointments.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Appointment does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            return new(data: query, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Appointment> Update(Appointment appointment)
        {
            bool condition = _context.Appointments.Any(x => x.Id == appointment.Id);
            if (condition is false)
            {
                return new(errorMessage: "Appointment does not exist",
                     statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _validator.Update(appointment);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, StatusCodes.Status500InternalServerError);
            }
            return new(data: appointment, statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<Appointment>> UpdateAsync(Appointment appointment)
        {
            bool condition = _context.Appointments.Any(x => x.Id == appointment.Id);
            if (condition is false)
            {
                return new(errorMessage: "Appointment does not exist",
                     statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _validator.Update(appointment);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message, StatusCodes.Status500InternalServerError);
            }
            return new(data: appointment, statusCode: StatusCodes.Status200OK);
        }
    }
}
