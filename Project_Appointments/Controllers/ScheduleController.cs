using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Models.Services;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _service;

        public ScheduleController(ScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Schedule> Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Schedule> Get(long id)
        {
            Schedule result;
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
        public ActionResult Post(Schedule schedule)
        {
            try
            {
                _service.Add(schedule);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = schedule.Id }, schedule);
        }

        [HttpPut]
        public ActionResult Put(Schedule schedule)
        {
            try
            {
                _service.Update(schedule);
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
