using FlightServiceAPI.Models;

namespace FlightServiceAPI.DTO
{
    public class FlightDTO
    {
        public int? PassengerLimit { get; set; }
        public string? AircraftType { get; set; }
        public string? DepartureDate { get; set; }
        public string? DepartureTime { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalDate { get; set; }
        public string? ArrivalTime { get; set; }        
        public string? ArrivalAirport { get; set; }        
        //public ICollection<Passenger>? Passengers { get; set; } = new List<Passenger>();
        //public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        //public int TotalPassengers => Passengers == null ? 0 : Passengers.Count;
    }
}
