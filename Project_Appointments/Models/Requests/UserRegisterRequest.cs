﻿namespace Project_Appointments.Models.Requests
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
