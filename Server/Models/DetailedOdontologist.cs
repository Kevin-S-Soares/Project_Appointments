namespace Server.Models
{
    public class DetailedOdontologist
    {
        public Odontologist Odontologist { get; set; } = default!;
        public IEnumerable<DetailedSchedule> Schedules { get; set; } = default!;
    }
}
