using Server.Models.Attributes;

namespace Server.Models
{
    [OdontologistValidation]
    public class Odontologist
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public override bool Equals(object? obj)
        {
            return obj is Odontologist odontologist &&
                   Id == odontologist.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
