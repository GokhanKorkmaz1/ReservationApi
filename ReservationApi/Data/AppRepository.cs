using ReservationApi.Models;

namespace ReservationApi.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository()
        {
            _context = new DataContext();
        }

        public void Add<T>(T entity)
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity)
        {
            _context.Entry(entity).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public List<PlacementDetail> GetPlacementDetails(int reservationResponseId)
        {
            var placementDetails = _context.PlacementDetails
                .Where(p => p.ReservationResponseId == reservationResponseId).ToList();
            return placementDetails;
        }

        public Reservation GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.SingleOrDefault(r => r.Id == reservationId);
            return reservation;
        }

        public ReservationResponse GetReservationResponseById(int reservationId)
        {
            var reservationResponse = _context.ReservationResponses.SingleOrDefault(r => r.ReservationId == reservationId);
            reservationResponse.PlacementDetails = _context.PlacementDetails.Where(p => p.ReservationResponseId == reservationResponse.Id).ToList();
            return reservationResponse;
        }

        public Train GetTrainById(int trainId)
        {
            var train = _context.Trains.SingleOrDefault(t => t.Id == trainId);
            train.Waggons = _context.Waggons.Where(w => w.TrainId == train.Id).ToList();
            return train;
        }

        public List<Train> GetTrains()
        {
            var trains = _context.Trains.ToList();
            foreach (var train in trains)
            {
                train.Waggons = _context.Waggons.Where(w => w.TrainId == train.Id).ToList();
            }
            return trains;
        }

        public Waggon GetWaggonById(int trainId)
        {
            var waggon = _context.Waggons.SingleOrDefault(w => w.Id == trainId);
            return waggon;
        }

        public List<Waggon> GetWaggons()
        {
            var waggons = _context.Waggons.ToList();
            return waggons;
        }

        public List<Waggon> GetWaggonsByTrainId(int trainId)
        {
            var waggons = _context.Waggons.Where(w => w.TrainId == trainId).ToList();
            return waggons;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Reservation> GetReservations()
        {
            var reservations = _context.Reservations.ToList();
            return reservations;
        }
    }
}
