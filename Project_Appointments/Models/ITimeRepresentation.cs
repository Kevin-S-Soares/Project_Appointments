namespace Project_Appointments.Models
{
    public interface ITimeRepresentation
    {
        public DayOfWeek StartDay { get; }
        public DayOfWeek EndDay { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
    }
}
