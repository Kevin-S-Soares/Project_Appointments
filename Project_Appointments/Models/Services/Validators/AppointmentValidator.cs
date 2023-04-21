using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;

namespace Project_Appointments.Models.Services.Validators
{
    public class AppointmentValidator
    {
        private readonly ApplicationContext _context = default!;

        public AppointmentValidator(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Appointment appointment)
        {
            BaseMethod(appointment);
        }

        public void Update(Appointment appointment)
        {
            BaseMethod(appointment, isToUpdate: true);
        }

        private void BaseMethod(Appointment appointment, bool isToUpdate = false)
        {
            bool condition = IsAppointmentWithinSchedule(appointment);
            if (condition is false)
            {
                throw new ModelException("Appointment is not within its referred schedule");
            }

            condition = IsAppointmentWithinBreakTimes(appointment);
            if (condition is true)
            {
                throw new ModelException("Appointment overlaps breakTimes");
            }

            condition = IsAppointmentWithinOtherAppointments(appointment, isToUpdate);
            if (condition is true)
            {
                throw new ModelException("Appointment overlaps other appointments");
            }
        }

        private bool IsAppointmentWithinSchedule(Appointment appointment)
        {
            Schedule schedule;
            try
            {
                schedule = _context.Schedules
                    .Where(x => x.Id == appointment.ScheduleId)
                    .First();
            }
            catch (Exception)
            {
                throw new ModelException("Invalid referred schedule");
            }
            return TimeRepresentation.IsCompletelyInserted(
                contained: appointment, contains: schedule);
        }

        private bool IsAppointmentWithinBreakTimes(Appointment appointment)
        {
            var structure = _context.BreakTimes
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

        private bool IsAppointmentWithinOtherAppointments(Appointment appointment, bool isToUpdate = false)
        {
            var structure = GetAppointments(appointment);

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

        private List<Appointment> GetAppointments(Appointment appointment)
        {
            return
                _context.Appointments
                .Where(x => x.ScheduleId == appointment.ScheduleId
                && ((x.Start.Year == appointment.Start.Year
                && x.Start.Month == appointment.Start.Month
                && x.Start.Day == appointment.Start.Day)
                || (x.End.Year == appointment.End.Year
                && x.End.Month == appointment.End.Month
                && x.End.Day == appointment.End.Day)))
                .ToList();
        }
    }
}
