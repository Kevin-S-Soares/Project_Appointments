using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Contexts;
using Project_Appointments.Models;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class BreakTimeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BreakTimeController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<BreakTime> Get()
        {
            return _context.BreakTimes.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BreakTime>> Get(long id)
        {
            BreakTime? result = await _context.BreakTimes.FindAsync(id);
            if (result is null)
            {
                return BadRequest("BreakTime not found");
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(BreakTime breakTime)
        {
            _context.BreakTimes.Add(breakTime);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = breakTime.Id }, breakTime);
        }

        [HttpPut]
        public async Task<ActionResult> Put(BreakTime breakTime)
        {
            BreakTime? result = await _context.BreakTimes.FindAsync(breakTime.Id);
            if (result is null)
            {
                return BadRequest("BreakTime not found");
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            BreakTime? result = await _context.BreakTimes.FindAsync(id);
            if (result is null)
            {
                return BadRequest("BreakTime not found");
            }
            _context.BreakTimes.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
