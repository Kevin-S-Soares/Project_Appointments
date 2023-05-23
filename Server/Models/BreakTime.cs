using Server.Models.Attributes;

namespace Server.Models
{
    [BreakTimeValidation]
    public class BreakTime : ITimeRepresentation
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DayOfWeek StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DayOfWeek EndDay { get; set; }
        public TimeSpan EndTime { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is BreakTime time &&
                   Id == time.Id;
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
