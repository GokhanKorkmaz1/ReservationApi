namespace ReservationApi.Models
{
    public class PlacementDetail : IEntity
    {
        public int Id { get; set; }
        public int ReservationResponseId { get; set; }
        public int WaggonId { get; set; }
        public int NumberofPeople { get; set; }

        public ReservationResponse ReservationResponse { get; set; }
    }
}
