using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FlightServiceEF
{
    public partial class FlightServiceContext : DbContext
    {
        //private readonly string _connectionString;

        //public FlightServiceContext()
        //{
        //}

        //public FlightServiceContext(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public FlightServiceContext(DbContextOptions<FlightServiceContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.AircraftType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureAirport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalAirport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");
                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureTime).HasColumnType("time");
                entity.Property(e => e.ArrivalTime).HasColumnType("time");
            });

            modelBuilder.Entity<Aircraft>(entity =>
            {
                entity.HasKey(e => e.SerialNumber);

                entity.Property(e => e.SerialNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.AircraftType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SeatClass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.SerialNumberNavigation)
                    .WithOne(p => p.Aircraft)
                    .HasForeignKey<Aircraft>(d => d.SerialNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flights_Aircrafts");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.ConfirmationNumber);

                entity.Property(e => e.ConfirmationNumber).ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConfirmationNumberNavigation)
                    .WithOne(p => p.Passenger)
                    .HasForeignKey<Passenger>(d => d.ConfirmationNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Passengers_Seats");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(e => e.SeatNumber);

                entity.Property(e => e.SeatNumber).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.PassengerConfirmationNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.SeatNumberNavigation)
                    .WithOne(p => p.Seat)
                    .HasForeignKey<Seat>(d => d.SeatNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aircrafts_Seats");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public DbSet<Aircraft> Aircrafts { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<Passenger> Passengers { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public class FlightServiceContextFactory : IDesignTimeDbContextFactory<FlightServiceContext>
    {
        private readonly string _connectionString;

        public FlightServiceContextFactory()
        {
            _connectionString = "Data Source=DESKTOP-EVNC5PC;Initial Catalog=FlightService; Integrated Security=True' Microsoft.EntityFrameworkCore.SqlServer";
        }

        public FlightServiceContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FlightServiceContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlightServiceContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new FlightServiceContext(optionsBuilder.Options);
        }
    }
}
