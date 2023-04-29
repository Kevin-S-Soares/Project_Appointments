using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.OdontologistService;

namespace Project_Appointments.Services.ScheduleService
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationContext _context;
        private readonly ScheduleValidator _scheduleValidator;

        public ScheduleService(ApplicationContext context,
            IOdontologistService odontologistService)
        {
            _context = context;
            _scheduleValidator = new(this, odontologistService);
        }

        public ServiceResponse<Schedule> Create(Schedule schedule)
        {
            var validator = _scheduleValidator.Add(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _context.Schedules.Add(schedule);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: schedule, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<Schedule>> CreateAsync(Schedule schedule)
        {
            var validator = _scheduleValidator.Add(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            await _context.Schedules.AddAsync(schedule);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: schedule, statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<string> Delete(long id)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Schedules.Remove(query);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Schedule deleted", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> DeleteAsync(long id)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Schedules.Remove(query);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Schedule deleted", statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<Schedule>> FindAll()
        {
            return new(data: _context.Schedules,
                statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<Schedule>> FindAllFromSameOdontologist(Schedule schedule)
        {
            var data = _context.Schedules
                .Where(x => x.OdontologistId == schedule.OdontologistId && x != schedule)
                .ToList();
            return new(data: data, StatusCodes.Status200OK);
        }

        public ServiceResponse<Schedule> FindById(long id)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            return new(data: query, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Schedule> Update(Schedule schedule)
        {
            bool condition = _context.Schedules.Any(x => x.Id == schedule.Id);
            if (condition is false)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _scheduleValidator.Update(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.Schedules.Update(schedule);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: schedule, statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<Schedule>> UpdateAsync(Schedule schedule)
        {
            bool condition = _context.Schedules.Any(x => x.Id == schedule.Id);
            if (condition is false)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            var validator = _scheduleValidator.Update(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            try
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: schedule, statusCode: StatusCodes.Status200OK);
        }
    }
}
