using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.AuthService;

namespace Project_Appointments.Services.BreakTimeService
{
    public class BreakTimeService : IBreakTimeService
    {
        private readonly ApplicationContext _context;
        private readonly BreakTimeValidator _validator;
        private readonly IAuthService _authService;

        public BreakTimeService(ApplicationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
            _validator = new(_context);
        }

        public ServiceResponse<BreakTime> Create(BreakTime breakTime)
        {
            if (IsAuthorizedToCreate(resource: breakTime.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var validator = _validator.Add(breakTime);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _context.BreakTimes.Add(breakTime);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: breakTime, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<BreakTime>> CreateAsync(BreakTime breakTime)
        {
            if (IsAuthorizedToCreate(resource: breakTime.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var validator = _validator.Add(breakTime);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _context.BreakTimes.Add(breakTime);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: breakTime, statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<BreakTime> FindById(long id)
        {
            var result = _context.BreakTimes.FirstOrDefault(x => x.Id == id);
            if (result is null)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            if (IsAuthorizedToRead(resource: result.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<BreakTime>> FindAll()
        {
            if (IsAuthorizedToReadAll() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var result = _context.BreakTimes.ToList();
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<BreakTime> Update(BreakTime breakTime)
        {
            if (IsAuthorizedToUpdate(resource: breakTime.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            bool condition = _context.BreakTimes.Any(x => x.Id == breakTime.Id);
            if (condition is false)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _validator.Update(breakTime);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.BreakTimes.Update(breakTime);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: breakTime, statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<BreakTime>> UpdateAsync(BreakTime breakTime)
        {
            if (IsAuthorizedToUpdate(resource: breakTime.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            bool condition = _context.BreakTimes.Any(x => x.Id == breakTime.Id);
            if (condition is false)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _validator.Update(breakTime);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.BreakTimes.Update(breakTime);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: breakTime, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<string> Delete(long id)
        {
            var query = _context.BreakTimes.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            if (IsAuthorizedToDelete(resource: query.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            _context.BreakTimes.Remove(query);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "BreakTime deleted", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> DeleteAsync(long id)
        {
            var query = _context.BreakTimes.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            if (IsAuthorizedToDelete(resource: query.ScheduleId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            _context.BreakTimes.Remove(query);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "BreakTime deleted", statusCode: StatusCodes.Status200OK);
        }

        private bool IsAuthorizedToCreate(long resource)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == resource);
            long resourceId = -1L;
            if (query is not null)
            {
                resourceId = query.OdontologistId;
            }
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToRead(long resource)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resource) || _authService.IsAttendant();
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
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToDelete(long resource)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == resource);
            long resourceId = -1L;
            if (query is not null)
            {
                resourceId = query.OdontologistId;
            }
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
    }
}
