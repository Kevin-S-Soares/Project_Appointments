using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Services.OdontologistService;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OdontologistController : ControllerBase
    {

        private readonly OdontologistService _service;

        public OdontologistController(OdontologistService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Odontologist> Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Odontologist> Get(long id)
        {
            Odontologist result;
            try
            {
                result = _service.FindById(id);
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            return result;
        }

        [HttpPost]
        public ActionResult Post(Odontologist odontologist)
        {
            try
            {
                _service.Add(odontologist);
            }
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = odontologist.Id }, odontologist);
        }

        [HttpPut]
        public ActionResult Put(Odontologist odontologist)
        {
            try
            {
                _service.Update(odontologist);
            }
            catch (ServiceException e)
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
            catch (ServiceException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
