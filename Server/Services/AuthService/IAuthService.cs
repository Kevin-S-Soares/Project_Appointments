﻿namespace Project_Appointments.Services.AuthService
{
    public interface IAuthService
    {
        public bool IsAdmin();
        public bool IsOdontologist(long resourceId);
        public bool IsAttendant();
    }
}
