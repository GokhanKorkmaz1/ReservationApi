namespace ReservationApi.Models
{
    public class Waggon : IEntity
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int NumberofReservedSeats { get; set; }

        public Train Train { get; set; }
    }
}
