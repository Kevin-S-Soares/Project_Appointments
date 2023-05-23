namespace Project_Appointments.Models
{
    public interface ITimeRepresentation
    {
        public DayOfWeek GetStartDay();
        public DayOfWeek GetEndDay();
        public TimeSpan GetStartTime();
        public TimeSpan GetEndTime();
    }
}
