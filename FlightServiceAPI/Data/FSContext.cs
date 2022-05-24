using FlightServiceAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace FlightServiceAPI.Data
{
    public class FSContext : DbContext
    {
        public FSContext()
        {

        }
        public FSContext(DbContextOptions<FSContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; } 
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasMany(f => f.Passengers)
                    .WithMany(p => p.Flights)
                    .UsingEntity<Ticket>();
            });


            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasMany(p => p.Flights)
                    .WithMany(p => p.Passengers)
                    .UsingEntity<Ticket>();
            });
        }
    }

}
