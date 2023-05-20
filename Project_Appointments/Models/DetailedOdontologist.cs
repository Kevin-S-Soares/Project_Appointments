namespace Project_Appointments.Models
{
    public class DetailedOdontologist
    {
        public Odontologist Odontologist { get; set; } = default!;
        public IEnumerable<Schedule> Schedules { get; set; } = default!;
        public IEnumerable<BreakTime> BreakTimes { get; set; } = default!;
        public IEnumerable<Appointment> Appointments { get; set; } = default!;
    }
}
