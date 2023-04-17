using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Contexts;
using Project_Appointments.Models;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ScheduleController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Schedule> Get()
        {
            return _context.Schedules.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> Get(long id)
        {
            Schedule? result = await _context.Schedules.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Schedule not found");
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = schedule.Id }, schedule);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Schedule schedule)
        {
            Schedule? result = await _context.Schedules.FindAsync(schedule.Id);
            if (result is null)
            {
                return BadRequest("Schedule not found");
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            Schedule? result = await _context.Schedules.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Schedule not found");
            }
            _context.Schedules.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
