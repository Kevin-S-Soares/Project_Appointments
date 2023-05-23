namespace Server.Models
{
    public class DetailedSchedule
    {
        public Schedule Schedule { get; set; } = default!;
        public IEnumerable<Appointment> Appointments { get; set; } = default!;
        public IEnumerable<BreakTime> BreakTimes { get; set; } = default!;
    }
}
