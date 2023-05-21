using Project_Appointments.Models.Attributes;

namespace Project_Appointments.Models
{
    [AppointmentValidation]
    public class Appointment : ITimeRepresentation
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DayOfWeek GetStartDay() => Start.DayOfWeek;

        public DayOfWeek GetEndDay() => End.DayOfWeek;

        public TimeSpan GetStartTime() => Start.TimeOfDay;

        public TimeSpan GetEndTime() => End.TimeOfDay;

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Id == appointment.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
