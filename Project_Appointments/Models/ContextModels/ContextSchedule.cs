using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Appointments.Models.ContextModels
{

    [Table(name: "Schedules")]
    public class ContextSchedule : ITimeRepresentation
    {
        public long Id { get; set; }
        public long OdontologistId { get; set; }
        public ContextOdontologist Odontologist { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public DayOfWeek StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DayOfWeek EndDay { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<ContextAppointment> Appointments { get; set; } = default!;
        public ICollection<ContextBreakTime> BreakTimes { get; set; } = default!;

        public override bool Equals(object? obj)
        {
            return obj is Schedule schedule &&
                   Id == schedule.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public DayOfWeek GetStartDay() => StartDay;
        public DayOfWeek GetEndDay() => EndDay;
        public TimeSpan GetStartTime() => StartTime;
        public TimeSpan GetEndTime() => EndTime;

        public Schedule ToModel()
        {
            return new()
            {
                Id = Id,
                OdontologistId = OdontologistId,
                Name = Name,
                StartDay = StartDay,
                StartTime = StartTime,
                EndDay = EndDay,
                EndTime = EndTime,
            };
        }
    }
}
