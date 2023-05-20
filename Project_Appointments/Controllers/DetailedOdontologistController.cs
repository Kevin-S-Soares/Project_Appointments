using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Project_Appointments.Services.DetailedOdontologistService;

namespace Project_Appointments.Controllers
{
    [Route("api/[controller]"), ApiController, AllowAnonymous]

    public class DetailedOdontologistController : ControllerBase
    {
        private readonly IDetailedOdontologistService _detailedOdontologistService;
        public DetailedOdontologistController(
            IDetailedOdontologistService detailedOdontologistService)
        {
            _detailedOdontologistService = detailedOdontologistService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return _detailedOdontologistService.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            return _detailedOdontologistService.FindById(id);
        }
    }
}
