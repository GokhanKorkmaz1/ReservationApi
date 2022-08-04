using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Business.Abstract;
using ReservationApi.Data;
using ReservationApi.Models;

namespace ReservationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IReservationService _reservationService;

        public ReservationsController(IAppRepository appRepository, IReservationService reservationService)
        {
            _appRepository = appRepository;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult GetReservations()
        {
            var reservations = _appRepository.GetReservations();
            return Ok(reservations);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]Reservation reservation)
        {
            _reservationService.Add(reservation);
            return Ok(_appRepository.GetReservationResponseById(reservation.Id));
        }

    }
}
