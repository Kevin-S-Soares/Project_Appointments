using Server.Models.Requests;

namespace Server.Services.UserService
{
    public interface IUserService
    {
        public ServiceResponse<UserRegisterRequest> Register(UserRegisterRequest request);
        public Task<ServiceResponse<UserRegisterRequest>> RegisterAsync(UserRegisterRequest request);
        public ServiceResponse<string> Verify(string token);
        public Task<ServiceResponse<string>> VerifyAsync(string token);
        public ServiceResponse<string> ForgetPassword(string email);
        public Task<ServiceResponse<string>> ForgetPasswordAsync(string email);
        public ServiceResponse<string> ResetPassword(UserResetPasswordRequest request);
        public Task<ServiceResponse<string>> ResetPasswordAsync(UserResetPasswordRequest request);
        public ServiceResponse<string> Login(UserLoginRequest request);
    }
}
