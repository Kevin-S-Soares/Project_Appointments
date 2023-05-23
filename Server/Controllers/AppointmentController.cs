using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.AppointmentService;

namespace Server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Appointment appointment)
        {
            return await _service.CreateAsync(appointment);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Appointment appointment)
        {
            return await _service.UpdateAsync(appointment);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
