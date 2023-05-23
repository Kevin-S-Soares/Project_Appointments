using System.Security.Claims;

namespace Project_Appointments.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAdmin()
        {
            bool condition = false;
            if (_httpContextAccessor.HttpContext is not null)
            {
                string role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                condition = role.Equals("Admin");
            }
            return condition;
        }
        public bool IsOdontologist(long resourceId)
        {
            bool condition = false;
            if (_httpContextAccessor.HttpContext is not null)
            {
                string role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                long id = Convert.ToInt64(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Actor));
                condition = role.Equals("Odontologist") && resourceId == id;
            }
            return condition;
        }
        public bool IsAttendant()
        {
            bool condition = false;
            if (_httpContextAccessor.HttpContext is not null)
            {
                string role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                condition = role.Equals("Attendant");
            }
            return condition;
        }
    }
}
