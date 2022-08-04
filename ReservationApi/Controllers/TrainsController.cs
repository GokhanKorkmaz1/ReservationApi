using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Data;

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private IAppRepository _appRepository;

        public TrainsController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }
        
        [HttpGet]
        public ActionResult GetTrains()
        {
            var trains = _appRepository.GetTrains();
            return Ok(trains);
        }

    }
}
