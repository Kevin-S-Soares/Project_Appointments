using Project_Appointments.Models.Attributes;

namespace Project_Appointments.Models
{
    [AppointmentValidation]
    public class Appointment
    {
        public long Id { get; set; }
        public long ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsPartiallyInserted(Appointment other)
        {
            return (other.StartTime <= StartTime && other.EndTime >= StartTime)
                || (other.StartTime <= EndTime && other.EndTime >= EndTime);
        }

        public bool IsCompletelyInserted(Appointment other)
        {
            return (other.StartTime <= StartTime && other.EndTime >= StartTime)
                && (other.StartTime <= EndTime && other.EndTime >= EndTime);
        }
    }
}
