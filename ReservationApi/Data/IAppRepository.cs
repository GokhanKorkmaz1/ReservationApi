using ReservationApi.Models;

namespace ReservationApi.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity);
        void Update<T>(T entity);
        void Delete<T>(T entity);
        bool SaveAll();

        List<Train> GetTrains();
        List<Waggon> GetWaggons();
        List<Waggon> GetWaggonsByTrainId(int trainId);
        List<PlacementDetail> GetPlacementDetails(int reservationResponseId);
        Train GetTrainById(int trainId);
        Waggon GetWaggonById(int trainId);
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservations();
        ReservationResponse GetReservationResponseById(int reservationResponseId);
    }
}
