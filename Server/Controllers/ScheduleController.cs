using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.ScheduleService;

namespace Server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
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
        public async Task<ActionResult> Post(Schedule schedule)
        {
            return await _service.CreateAsync(schedule);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Schedule schedule)
        {
            return await _service.UpdateAsync(schedule);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
