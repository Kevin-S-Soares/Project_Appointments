using Microsoft.EntityFrameworkCore;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.OdontologistService;

namespace Project_Appointments.Services.ScheduleService
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationContext _context;
        private readonly ScheduleValidator _validator;

        public ScheduleService(ApplicationContext context, 
            IOdontologistService odontologistService)
        {
            _context = context;
            _validator = new(this, odontologistService);
        }

        public ServiceResponse Create(Schedule schedule)
        {
            var result = new ServiceResponse(value: schedule);
            var validator = _validator.Add(schedule);
            if(validator.IsValid is false)
            {
                result.Value = validator.ErrorMessage;
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
            _context.Schedules.Add(schedule);
            try
            {
                _context.SaveChanges();
                result.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> CreateAsync(Schedule schedule)
        {
            var result = new ServiceResponse(value: schedule);
            var validator = _validator.Add(schedule);
            if (validator.IsValid is false)
            {
                result.Value = validator.ErrorMessage;
                result.StatusCode = StatusCodes.Status500InternalServerError;
                return result;
            }
            await _context.Schedules.AddAsync(schedule);
            try
            {
                await _context.SaveChangesAsync();
                result.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public ServiceResponse Delete(long id)
        {
            var result = new ServiceResponse();
            var obj = FindById(id).Value;
            if (obj is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Schedule does not exist";
                return result;
            }
            _context.Schedules.Remove((Schedule) obj);
            try
            {
                _context.SaveChanges();
                result.Value = "Schedule deleted";
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> DeleteAsync(long id)
        {
            var result = new ServiceResponse();
            var obj = FindById(id).Value;
            if (obj is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Schedule does not exist";
                return result;
            }
            _context.Schedules.Remove((Schedule) obj);
            try
            {
                await _context.SaveChangesAsync();
                result.Value = "Schedule deleted";
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public ServiceResponse FindAll()
        {
            return new ServiceResponse(_context.Schedules);
        }

        public ServiceResponse FindAllFromOdontologistId(long id)
        {
            var test = _context.Schedules.Where(x => x.OdontologistId == id).ToList();
            return new ServiceResponse(
                _context.Schedules.Where(x => x.OdontologistId == id).ToList());
        }

        public ServiceResponse FindById(long id)
        {
            var result = new ServiceResponse();
            result.Value = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (result.Value is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Schedule does not exist";
            }
            return result;
        }

        public ServiceResponse Update(Schedule schedule)
        {
            var result = new ServiceResponse();
            var query = _context.Schedules.FirstOrDefault(x => x.Id == schedule.Id);
            if (query is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Schedule does not exist";
                return result;
            }
            var validator = _validator.Update(schedule);
            _context.Entry(query).State = EntityState.Detached;
            try
            {
                _context.Schedules.Update(schedule);
                _context.SaveChanges();
                result.Value = schedule;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> UpdateAsync(Schedule schedule)
        {
            var result = new ServiceResponse();
            var query = _context.Schedules.FirstOrDefault(x => x.Id == schedule.Id);
            if (query is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Schedule does not exist";
                return result;
            }
            var validator = _validator.Update(schedule);
            _context.Entry(query).State = EntityState.Detached;
            try
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
                result.Value = schedule;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }
    }
}
