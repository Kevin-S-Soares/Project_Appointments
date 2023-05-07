using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models.Requests;
using Project_Appointments.Services.UserService;

namespace Project_Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> New(UserRegisterRequest userRequest)
        {
            return await _service.AddAsync(userRequest);
        }
    }
}
