using Project_Appointments.Models.Attributes;

namespace Project_Appointments.Models
{
    [BreakTimeValidation]
    public class BreakTime
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsPartiallyInserted(BreakTime other)
        {
            return (other.StartTime <= StartTime && other.EndTime >= StartTime) 
                || (other.StartTime <= EndTime && other.EndTime >= EndTime);
        }

        public bool IsCompletelyInserted(BreakTime other)
        {
            return (other.StartTime <= StartTime && other.EndTime >= StartTime)
                && (other.StartTime <= EndTime && other.EndTime >= EndTime);
        }
    }
}
