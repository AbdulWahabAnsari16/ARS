using Microsoft.EntityFrameworkCore;

namespace ARS.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AdminLogin> AdminLogin { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; } // Fixed typo
        public DbSet<City> Cities { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
		public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightRoutes> FlightRoutes { get; set; }

        public DbSet<FlightSchedule> FlightSchedules { get; set; }
        public DbSet<ItinerarySegment> ItinerarySegments { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<MileageHistory> MileageHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure FlightRoutes relationships
            modelBuilder.Entity<FlightRoutes>()
                .HasOne(fr => fr.OriginAirport)
                .WithMany(a => a.OriginRoutes)
                .HasForeignKey(fr => fr.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightRoutes>()
                .HasOne(fr => fr.DestinationAirport)
                .WithMany(a => a.DestinationRoutes)
                .HasForeignKey(fr => fr.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure FlightSchedule relationships
            modelBuilder.Entity<FlightSchedule>()
                .HasOne(fs => fs.DepartureAirport)
                .WithMany(a => a.DepartureSchedules)
                .HasForeignKey(fs => fs.DepartureAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightSchedule>()
                .HasOne(fs => fs.ArrivalAirport)
                .WithMany(a => a.ArrivalSchedules)  // Explicitly maps to Airport.ArrivalSchedules
                .HasForeignKey(fs => fs.ArrivalAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            // MileageHistory configurations
            modelBuilder.Entity<MileageHistory>()
                .HasOne(m => m.Reservation)
                .WithMany(r => r.History)
                .HasForeignKey(m => m.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MileageHistory>()
                .HasOne(m => m.User)
                .WithMany(u => u.MileageHistories)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Decimal precision configurations
            modelBuilder.Entity<CancellationPolicy>()
                .Property(c => c.RefundPercent)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PricingRule>()
                .Property(p => p.PriceMultiplier)
                .HasPrecision(18, 2);
        }


    }

}