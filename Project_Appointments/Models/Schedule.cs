using Project_Appointments.Models.Attributes;
using Project_Appointments.Models.ContextModels;

namespace Project_Appointments.Models
{
    [ScheduleValidation]
    public class Schedule : ITimeRepresentation
    {
        public long Id { get; set; }
        public long OdontologistId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DayOfWeek StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DayOfWeek EndDay { get; set; }
        public TimeSpan EndTime { get; set; }

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

        public ContextSchedule ToContextModel()
        {
            return new()
            {
                Id = Id,
                OdontologistId = OdontologistId,
                StartDay = StartDay,
                StartTime = StartTime,
                Name = Name,
                EndDay = EndDay,
                EndTime = EndTime,
            };
        }
    }
}
