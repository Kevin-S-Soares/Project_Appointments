using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Services.BreakTimeService;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class BreakTimeController : ControllerBase
    {
        private readonly IBreakTimeService _service;

        public BreakTimeController(IBreakTimeService service)
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
        public async Task<ActionResult> Post(BreakTime breakTime)
        {
            return await _service.CreateAsync(breakTime);
        }

        [HttpPut]
        public async Task<ActionResult> Put(BreakTime breakTime)
        {
            return await _service.UpdateAsync(breakTime);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
