namespace ReservationApi.Models
{
    public class ReservationResponse : IEntity
    {
        public ReservationResponse()
        {
            PlacementDetails = new List<PlacementDetail>();
        }
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public bool IsReservable { get; set; }

        public List<PlacementDetail> PlacementDetails { get; set; }
    }
}
