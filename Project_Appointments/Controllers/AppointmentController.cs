using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Contexts;
using Project_Appointments.Models;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AppointmentController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return _context.Appointments.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> Get(long id)
        {
            Appointment? result = await _context.Appointments.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Appointment not found");
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = appointment.Id }, appointment);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Appointment appointment)
        {
            Appointment? result = await _context.Appointments.FindAsync(appointment.Id);
            if (result is null)
            {
                return BadRequest("Appointment not found");
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            Appointment? result = await _context.Appointments.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Appointment not found");
            }
            _context.Appointments.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
