using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Services.ScheduleService;

namespace Project_Appointments.Services.BreakTimeService
{
    public class BreakTimeService : IBreakTimeService
    {
        private readonly ApplicationContext _context;
        private readonly BreakTimeValidator _validator;

        public BreakTimeService(ApplicationContext context, IScheduleService service)
        {
            _context = context;
            _validator = new(this, service);
        }
        /*
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
        */

        public ServiceResponse<BreakTime> Create(BreakTime breakTime)
        {
            var validator = _validator.Add(breakTime);
            if(validator.IsValid is false)
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
            if(result is null)
            {
                return new(errorMessage: "BreakTime does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<BreakTime>> FindAll()
        {
            var result = _context.BreakTimes.ToList();
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<BreakTime> Update(BreakTime breakTime)
        {
            bool condition = _context.BreakTimes.Any(x=> x.Id == breakTime.Id);
            if(condition is false)
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

        public ServiceResponse<IEnumerable<BreakTime>> FindAllFromSameSchedule(BreakTime breakTime)
        {
            var result = _context.BreakTimes
                .Where(x => x.ScheduleId == breakTime.ScheduleId && x.Id != breakTime.Id)
                .ToList();
            return new (data: result, statusCode: StatusCodes.Status200OK);
        }
    }
}
