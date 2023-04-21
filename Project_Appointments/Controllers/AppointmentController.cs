using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Models.Services;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _service;

        public AppointmentController(AppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> Get(long id)
        {
            Appointment result;
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
        public ActionResult Post(Appointment appointment)
        {
            try
            {
                _service.Add(appointment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = appointment.Id }, appointment);
        }

        [HttpPut]
        public ActionResult Put(Appointment appointment)
        {
            try
            {
                _service.Update(appointment);
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
