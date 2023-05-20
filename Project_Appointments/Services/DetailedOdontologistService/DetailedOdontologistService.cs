using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.AuthService;

namespace Project_Appointments.Services.DetailedOdontologistService
{
    public class DetailedOdontologistService : IDetailedOdontologistService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;

        public DetailedOdontologistService(ApplicationContext context, 
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public ServiceResponse<IEnumerable<DetailedOdontologist>> FindAll()
        {
            if(IsAuthorizedToReadAll() is false)
            {
                /*
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
                */
            }
            var result = new List<DetailedOdontologist>();
            var list =
                (from odontologists in _context.Odontologists
                select odontologists).ToList();
            
            foreach(var element in list)
            {
                result.Add(GetDetailedOdontologist(element));
            }

            return new (data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<DetailedOdontologist> FindById(long id)
        {
            if (IsAuthorizedToRead(resourceId: id) is false)
            {
                /*
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
                */
            }
            var element =
                (from odontologists in _context.Odontologists
                 where odontologists.Id == id
                 select odontologists).FirstOrDefault();

            if(element is null)
            {
                {
                    
                    return new(errorMessage: "Odontologist does not exist",
                        statusCode: StatusCodes.Status404NotFound);
                    
                }
            }

            var result = GetDetailedOdontologist(element);

            return new(data: result, statusCode: StatusCodes.Status200OK);
        }


        private DetailedOdontologist GetDetailedOdontologist(Odontologist element)
        {
            return new()
            {
                Odontologist = element,
                Schedules = GetSchedules(element),
                Appointments = GetAppointments(element),
                BreakTimes = GetBreakTimes(element)
            };
        }

        private IEnumerable<Schedule> GetSchedules(Odontologist element)
        {
            return (from odontologists in _context.Odontologists
                    join schedules in _context.Schedules
                    on odontologists.Id equals schedules.OdontologistId
                    where odontologists.Id == element.Id
                    select schedules).ToList();
        }

        private IEnumerable<Appointment> GetAppointments(Odontologist element)
        {
            return (from odontologists in _context.Odontologists
             join schedules in _context.Schedules
             on odontologists.Id equals schedules.OdontologistId
             join appointments in _context.Appointments
             on schedules.Id equals appointments.ScheduleId
             where odontologists.Id == element.Id
             select appointments).ToList();
        }

        private IEnumerable<BreakTime> GetBreakTimes(Odontologist element)
        {
           return (from odontologists in _context.Odontologists
             join schedules in _context.Schedules
             on odontologists.Id equals schedules.OdontologistId
             join breakTimes in _context.BreakTimes
             on schedules.Id equals breakTimes.ScheduleId
             where odontologists.Id == element.Id
             select breakTimes).ToList();
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
    }
}
