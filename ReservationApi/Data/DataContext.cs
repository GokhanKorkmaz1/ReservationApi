using Microsoft.EntityFrameworkCore;
using ReservationApi.Models;

namespace ReservationApi.Data
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLOCALDB;Database=ReservationDatabase;Trusted_Connection=true");
            }
        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<Waggon> Waggons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationResponse> ReservationResponses { get; set; }
        public DbSet<PlacementDetail> PlacementDetails { get; set; }
    }
}
