using Project_Appointments.Models.Attributes;

namespace Project_Appointments.Models
{
    [ScheduleValidation]
    public class Schedule
    {
        public long Id { get; set; }
        public long OdontologistId { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
