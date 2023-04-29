using Project_Appointments.Models;
using Project_Appointments.Services.OdontologistService;

namespace Project_Appointments.Services.ScheduleService
{
    public class ScheduleValidator
    {
        private readonly IScheduleService _scheduleService;
        private readonly IOdontologistService _odontologistService;
        public ScheduleValidator(IScheduleService scheduleService,
            IOdontologistService odontologistService)
        {
            _scheduleService = scheduleService;
            _odontologistService = odontologistService;
        }

        public Validator Add(Schedule schedule)
        {
            return BaseMethod(schedule);
        }

        public Validator Update(Schedule schedule)
        {
            return BaseMethod(schedule, isToUpdate: true);
        }

        private Validator BaseMethod(Schedule schedule, bool isToUpdate = false)
        {
            var result = new Validator();

            bool condition = DoesOdontologistExists(schedule);
            if (condition is false)
            {
                result.ErrorMessage = "Invalid referred odontologist";
                return result;
            }

            condition = IsWithinOtherSchedule(schedule, isToUpdate);
            if (condition is true)
            {
                result.ErrorMessage = "Schedule overlaps other schedules";
                return result;
            }

            result.IsValid = true;
            return result;
        }

        private bool DoesOdontologistExists(Schedule schedule)
        {
            var query = _odontologistService.FindById(schedule.Id);
            return query.Value is not null;
        }

        private bool IsWithinOtherSchedule(Schedule schedule, bool isToUpdate = false)
        {
            var structure =
                _scheduleService.FindAllFromSameOdontologist(schedule).Data!.ToList();

            if (isToUpdate)
            {
                structure.Remove(schedule);
            }

            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(
                    contained: schedule, contains: element);
                condition = condition || TimeRepresentation.IsPartiallyInserted(
                    contained: element, contains: schedule);

                if (condition is true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
