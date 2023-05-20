using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Appointments.Models.ContextModels
{
    [Table(name: "Appointments")]
    public class ContextAppointment : ITimeRepresentation
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public ContextSchedule Schedule { get; set; } = default!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            return obj is Appointment appointment &&
                   Id == appointment.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public DayOfWeek GetStartDay() => Start.DayOfWeek;

        public DayOfWeek GetEndDay() => End.DayOfWeek;

        public TimeSpan GetStartTime() => Start.TimeOfDay;

        public TimeSpan GetEndTime() => End.TimeOfDay;

        public Appointment ToModel()
        {
            return new()
            {
                Id = Id,
                ScheduleId = ScheduleId,
                Start = Start,
                End = End,
                PatientName = PatientName,
                Description = Description
            };
        }
    }
}
