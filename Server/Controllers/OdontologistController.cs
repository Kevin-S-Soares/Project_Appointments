using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services.OdontologistService;

namespace Server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
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
