using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.AuthService;

namespace Project_Appointments.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext _context;
        private readonly AppointmentValidator _validator;
        private readonly IAuthService _authService;

        public AppointmentService(ApplicationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
            _validator = new(_context);
        }

        public ServiceResponse<Appointment> Create(Appointment appointment)
        {
            if (IsAuthorizedToCreate(resource: appointment.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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
            if (IsAuthorizedToCreate(resource: appointment.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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

            if (IsAuthorizedToDelete(resource: query.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
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

            if (IsAuthorizedToDelete(resource: query.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
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
            if (IsAuthorizedToReadAll() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var result = _context.Appointments;
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
            if (IsAuthorizedToRead(resource: query.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            return new(data: query, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Appointment> Update(Appointment appointment)
        {
            if (IsAuthorizedToUpdate(resource: appointment.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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
            if (IsAuthorizedToUpdate(resource: appointment.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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
        private bool IsAuthorizedToCreate(long resource)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == resource);
            long resourceId = -1;
            if (query is not null)
            {
                resourceId = query.OdontologistId;
            }
            return _authService.IsAdmin() || _authService.IsAttendant()
                || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToRead(long resource)
        {
            return _authService.IsAdmin() || _authService.IsAttendant()
                || _authService.IsOdontologist(resource);
        }
        private bool IsAuthorizedToReadAll()
        {
            return _authService.IsAdmin() || _authService.IsAttendant();
        }
        private bool IsAuthorizedToUpdate(long resource)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == resource);
            long resourceId = -1L;
            if (query is not null)
            {
                resourceId = query.OdontologistId;
            }
            return _authService.IsAdmin() || _authService.IsAttendant()
                || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToDelete(long resource)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == resource);
            long resourceId = -1L;
            if (query is not null)
            {
                resourceId = query.OdontologistId;
            }
            return _authService.IsAdmin() || _authService.IsAttendant()
                || _authService.IsOdontologist(resourceId);
        }
    }
}
