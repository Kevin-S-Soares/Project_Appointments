using Microsoft.AspNetCore.Mvc;

namespace Project_Appointments.Services
{
    public class ServiceResponse : ObjectResult
    {
        public ServiceResponse() : base(new()) { }

        public ServiceResponse(object value) : base(value) { }
    }
}
