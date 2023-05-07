using Project_Appointments.Models;
using Project_Appointments.Models.Requests;

namespace Project_Appointments.Services.UserService
{
    public interface IUserService
    {
        public ServiceResponse<UserRegisterRequest> Add(UserRegisterRequest userRegisterRequest);
        public Task<ServiceResponse<UserRegisterRequest>> AddAsync(UserRegisterRequest userRegisterRequest);
    }
}
