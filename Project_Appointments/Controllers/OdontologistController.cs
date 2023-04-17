using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Contexts;
using Project_Appointments.Models;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OdontologistController : ControllerBase
    {

        private readonly ApplicationContext _context;

        public OdontologistController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Odontologist> Get()
        {
            return _context.Odontologists.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Odontologist>> Get(long id)
        {
            Odontologist? result = await _context.Odontologists.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Odontologist not found");
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Odontologist odontologist)
        {
            _context.Odontologists.Add(odontologist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = odontologist.Id }, odontologist);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Odontologist odontologist)
        {
            Odontologist? result = await _context.Odontologists.FindAsync(odontologist.Id);
            if (result is null)
            {
                return BadRequest("Odontologist not found");
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            Odontologist? result = await _context.Odontologists.FindAsync(id);
            if (result is null)
            {
                return BadRequest("Odontologist not found");
            }
            _context.Odontologists.Remove(result);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
