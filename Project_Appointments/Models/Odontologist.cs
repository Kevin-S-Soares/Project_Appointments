using Appointments_Project.Models.Attributes;

namespace Project_Appointments.Models
{
    [OdontologistValidation]
    public class Odontologist
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
