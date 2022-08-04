namespace ReservationApi.Models
{
    public class Reservation : IEntity
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public int NumberofPassenger { get; set; }
        public bool IsAllowDifferentWaggons { get; set; }
    }
}
