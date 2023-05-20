﻿using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.AuthService;

namespace Project_Appointments.Services.ScheduleService
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;
        private readonly ScheduleValidator _scheduleValidator;

        public ScheduleService(ApplicationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
            _scheduleValidator = new(_context);
        }

        public ServiceResponse<Schedule> Create(Schedule schedule)
        {
            if (IsAuthorizedToCreate(resourceId: schedule.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var validator = _scheduleValidator.Add(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            var contextModel = schedule.ToContextModel();
            _context.Schedules.Add(contextModel);
            try
            {
                _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: contextModel.ToModel(), statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<Schedule>> CreateAsync(Schedule schedule)
        {
            if (IsAuthorizedToCreate(resourceId: schedule.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var validator = _scheduleValidator.Add(schedule);
            if (validator.IsValid is false)
            {
                return new(errorMessage: validator.ErrorMessage,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            var contextModel = schedule.ToContextModel();
            await _context.Schedules.AddAsync(contextModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: contextModel.ToModel(), statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<string> Delete(long id)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            if (IsAuthorizedToDelete(resourceId: query.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
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
            if (IsAuthorizedToDelete(resourceId: query.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
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
            if (IsAuthorizedToReadAll() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var result = _context.Schedules.Select(x => x.ToModel());
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Schedule> FindById(long id)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Schedule does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            if (IsAuthorizedToRead(resourceId: query.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            return new(data: query.ToModel(), statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Schedule> Update(Schedule schedule)
        {
            if (IsAuthorizedToUpdate(resourceId: schedule.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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
            var contextModel = schedule.ToContextModel();
            try
            {
                _context.Schedules.Update(contextModel);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: contextModel.ToModel(), statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<Schedule>> UpdateAsync(Schedule schedule)
        {
            if (IsAuthorizedToUpdate(resourceId: schedule.OdontologistId) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
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
            var contextModel = schedule.ToContextModel();
            try
            {
                _context.Schedules.Update(contextModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: contextModel.ToModel(), statusCode: StatusCodes.Status200OK);
        }
        private bool IsAuthorizedToCreate(long resourceId)
        {

            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToRead(long resourceId)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId)
                || _authService.IsAttendant();
        }
        private bool IsAuthorizedToReadAll()
        {
            return _authService.IsAdmin() || _authService.IsAttendant();
        }
        private bool IsAuthorizedToUpdate(long resourceId)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToDelete(long resourceId)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
    }
}
