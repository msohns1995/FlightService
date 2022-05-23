using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FlightServiceEF
{
    public partial class FlightServiceDBContext : DbContext
    {
        public FlightServiceDBContext()
        {
        }

        public FlightServiceDBContext(DbContextOptions<FlightServiceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aircraft> Aircrafts { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-EVNC5PC;Initial Catalog=FlightServiceDB; Integrated Security=True; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.FlightNumber);

                entity.Property(e => e.AircraftType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalAirport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArrivalDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureAirport)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");
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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
