using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Services.OdontologistService;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class OdontologistController : ControllerBase
    {
        private readonly IOdontologistService _service;

        public OdontologistController(IOdontologistService service)
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
        public async Task<ActionResult> Post(Odontologist odontologist)
        {
            return await _service.CreateAsync(odontologist);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Odontologist odontologist)
        {
            return await _service.UpdateAsync(odontologist);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
