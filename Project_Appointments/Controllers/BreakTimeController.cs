using Microsoft.AspNetCore.Mvc;
using Project_Appointments.Models;
using Project_Appointments.Services;
using Project_Appointments.Services.BreakTimeService;

namespace Project_Appointments.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class BreakTimeController : ControllerBase
    {
        private readonly IBreakTimeService _service;

        public BreakTimeController(IBreakTimeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ServiceResponse<IEnumerable<BreakTime>> Get()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ServiceResponse<BreakTime> Get(long id)
        {
            return _service.FindById(id);
        }

        [HttpPost]
        public async Task<ServiceResponse<BreakTime>> Post(BreakTime breakTime)
        {
            return await _service.CreateAsync(breakTime);
        }

        [HttpPut]
        public async Task<ServiceResponse<BreakTime>> Put(BreakTime breakTime)
        {
            return await _service.UpdateAsync(breakTime);
        }

        [HttpDelete]
        public async Task<ServiceResponse<string>> Delete(long id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
