using Microsoft.AspNetCore.Mvc;
using Server.Models.Requests;
using Server.Services.UserService;

namespace Server.Controllers
{

    [ApiController, Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register(UserRegisterRequest request)
        {
            return await _service.RegisterAsync(request);
        }


        [HttpPost(nameof(VerifyToken))]
        public async Task<ActionResult> VerifyToken(string token)
        {
            return await _service.VerifyAsync(token);
        }

        [HttpPost(nameof(ForgetPassword))]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            return await _service.ForgetPasswordAsync(email);
        }

        [HttpPost(nameof(ResetPassword))]
        public async Task<ActionResult> ResetPassword(UserResetPasswordRequest request)
        {
            return await _service.ResetPasswordAsync(request);
        }

        [HttpPost(nameof(Login))]
        public ActionResult Login(UserLoginRequest request)
        {
            return _service.Login(request);
        }
    }
}
