using Project_Appointments.Models.Attributes;

namespace Project_Appointments.Models
{
    [BreakTimeValidation]
    public class BreakTime : ITimeRepresentation
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public DayOfWeek StartDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public DayOfWeek EndDay { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
