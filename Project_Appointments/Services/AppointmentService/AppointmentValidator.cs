using Project_Appointments.Models;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.ScheduleService;

namespace Project_Appointments.Services.AppointmentService
{
    public class AppointmentValidator
    {
        private readonly IScheduleService _scheduleService;
        private readonly IBreakTimeService _breakTimeService;
        private readonly IAppointmentService _appointmentService;

        public AppointmentValidator(IScheduleService scheduleService,
            IBreakTimeService breakTimeService, IAppointmentService appointmentService)
        {
            _scheduleService = scheduleService;
            _breakTimeService = breakTimeService;
            _appointmentService = appointmentService;
        }

        public Validator Add(Appointment appointment)
        {
            return BaseMethod(appointment);
        }

        public Validator Update(Appointment appointment)
        {
            return BaseMethod(appointment, isToUpdate: true);
        }

        private Validator BaseMethod(Appointment appointment, bool isToUpdate = false)
        {
            bool condition = DoesScheduleExist(appointment);
            if (condition is false)
            {
                return new("Invalid referred schedule");
            }

            condition = IsAppointmentWithinSchedule(appointment);
            if (condition is false)
            {
                return new("Appointment is not within its referred schedule");
            }

            condition = IsAppointmentWithinBreakTimes(appointment);
            if (condition is true)
            {
                return new("Appointment overlaps breakTimes");
            }

            condition = IsAppointmentWithinOtherAppointments(appointment, isToUpdate);
            if (condition is true)
            {
                return new("Appointment overlaps other appointments");
            }

            return new(true);
        }

        private bool DoesScheduleExist(Appointment appointment)
        {
            var query = _scheduleService.FindById(appointment.ScheduleId).Data;
            return query is not null;
        }

        private bool IsAppointmentWithinSchedule(Appointment appointment)
        {
            var schedule = _scheduleService.FindById(appointment.ScheduleId).Data!;
            return TimeRepresentation.IsCompletelyInserted(
                contained: appointment, contains: schedule);
        }

        private bool IsAppointmentWithinBreakTimes(Appointment appointment)
        {
            var structure = _breakTimeService.FindAllFromSameSchedule(appointment).Data!;

            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsPartiallyInserted(
                    contained: appointment, contains: element);
                condition = condition || TimeRepresentation.IsPartiallyInserted(
                    contained: element, contains: appointment);
                if (condition is true)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsAppointmentWithinOtherAppointments(Appointment appointment, bool isToUpdate = false)
        {
            var structure = _appointmentService
                .FindAppointmentsFromSameDay(appointment).Data!.ToList();

            if (isToUpdate)
            {
                structure.Remove(appointment);
            }

            foreach (var element in structure)
            {
                bool condition = TimeRepresentation.IsAppointmentPartiallyInserted(
                    contained: appointment, contains: element);
                condition = condition || TimeRepresentation.IsAppointmentPartiallyInserted(
                    contained: element, contains: appointment);
                if (condition is true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
