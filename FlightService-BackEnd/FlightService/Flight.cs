using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace FlightServiceEF
{
    public partial class Flight
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public string AircraftType { get; set; } = null!;

        public virtual Aircraft Aircraft { get; set; } = null!;

        public Flight()
        {
            Id = 0;
            FlightNumber = 0;
            DepartureDate = DateTime.Now;
            DepartureTime = DateTime.UtcNow;
            ArrivalDate = DateTime.Now;
            ArrivalTime = DateTime.UtcNow;
            DepartureAirport = "";
            ArrivalAirport = "";
            AircraftType = "";
        }

        public Flight(SqlDataReader reader)
        {
            Id = Convert.ToInt32(reader["Id"] ?? 0);
            FlightNumber = Convert.ToInt32(reader["FlightNumber"] ?? 0);
            DepartureDate = Convert.ToDateTime(reader["DepartureDate"] ?? DateTime.Now);
            DepartureTime = Convert.ToDateTime(reader["DepartureTime"] ?? DateTime.UtcNow);
            ArrivalDate = Convert.ToDateTime(reader["ArrivalDate"] ?? DateTime.Now);
            DepartureTime = Convert.ToDateTime(reader["ArrivalTime"] ?? DateTime.UtcNow);
            DepartureAirport = reader["DepartureAirport"].ToString() ?? "";
            ArrivalAirport = reader["ArrivalAirport"].ToString() ?? "";
        }

        public override string ToString()
        {
            return $"[Flight: {FlightNumber} {DepartureDate} {DepartureTime} {ArrivalDate} {ArrivalTime} {DepartureAirport} {ArrivalAirport}";
        }
    }
}
