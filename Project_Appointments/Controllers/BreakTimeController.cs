using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Services.BreakTimeService;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class BreakTimeController : ControllerBase
    {
        private readonly BreakTimeService _service;

        public BreakTimeController(BreakTimeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<BreakTime> Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<BreakTime> Get(long id)
        {
            BreakTime result;
            try
            {
                result = _service.FindById(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return result;
        }

        [HttpPost]
        public ActionResult Post(BreakTime breakTime)
        {
            try
            {
                _service.Add(breakTime);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = breakTime.Id }, breakTime);
        }

        [HttpPut]
        public ActionResult Put(BreakTime breakTime)
        {
            try
            {
                _service.Update(breakTime);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
