using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Appointments.Models.ContextModels
{

    [Table(name: "Odontologists")]
    public class ContextOdontologist
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<ContextSchedule> Schedules { get; set; } = default!;

        public override bool Equals(object? obj)
        {
            return obj is ContextOdontologist odontologist &&
                   Id == odontologist.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public Odontologist ToModel()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Phone = Phone,
                Email = Email
            };
        }
    }
}
