using Server.Models.Attributes;

namespace Server.Models
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
    }
}
