using Server.Contexts;
using Server.Models;

namespace Server.Services.AppointmentService
{
    public class AppointmentValidator
    {
        private readonly ApplicationContext _context;

        public AppointmentValidator(ApplicationContext context)
        {
            _context = context;
        }

        public Validator Add(Appointment appointment)
        {
            return BaseMethod(appointment);
        }

        public Validator Update(Appointment appointment)
        {
            return BaseMethod(appointment);
        }

        private Validator BaseMethod(Appointment appointment)
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

            condition = IsAppointmentWithinOtherAppointments(appointment);
            if (condition is true)
            {
                return new("Appointment overlaps other appointments");
            }

            return new(true);
        }

        private bool DoesScheduleExist(Appointment appointment)
        {
            var query = _context.Schedules.FirstOrDefault(x => x.Id == appointment.ScheduleId);
            return query is not null;
        }

        private bool IsAppointmentWithinSchedule(Appointment appointment)
        {
            var schedule = _context.Schedules.First(x => x.Id == appointment.ScheduleId);
            return TimeRepresentation.IsCompletelyInserted(
                contained: appointment, contains: schedule);
        }

        private bool IsAppointmentWithinBreakTimes(Appointment appointment)
        {
            var structure =
                _context.BreakTimes
                .Where(x => x.ScheduleId == appointment.ScheduleId)
                .ToList();

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

        private bool IsAppointmentWithinOtherAppointments(Appointment appointment)
        {
            var structure = GetAppointmentsFromTheSameDay(appointment);

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

        private IEnumerable<Appointment> GetAppointmentsFromTheSameDay(Appointment appointment)
        {
            return _context.Appointments
                .Where(x => x.Id != appointment.Id
                && x.ScheduleId == appointment.ScheduleId
                && ((x.Start.Year == appointment.Start.Year
                && x.Start.Month == appointment.Start.Month
                && x.Start.Day == appointment.Start.Day)
                || (x.End.Year == appointment.End.Year
                && x.End.Month == appointment.End.Month
                && x.End.Day == appointment.End.Day)));
        }
    }
}
