using ReservationApi.Models;

namespace ReservationApi.Business.Abstract
{
    public interface IReservationService
    {
        void Add(Reservation reservation);
    }
}
