﻿using Appointments_Project.Models.Attributes;
using Project_Appointments.Models.ContextModels;

namespace Project_Appointments.Models
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

        public ContextOdontologist ToContextModel()
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
