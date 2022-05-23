using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightServiceAPI.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }
        public int? PassengerLimit { get; set; }
        public string? AircraftType { get; set; }
        public string? DepartureDate { get; set; }
        public string? DepartureTime { get; set; }
        public string? DepartureAirport { get; set; }
        public string? ArrivalDate { get; set; }
        public string? ArrivalTime { get; set; }        
        public string? ArrivalAirport { get; set; }      
        

        //Collection to keep track of the passengers on the flight itself
        //One flight with many passengers and each passenger can have many flights
        public ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
        [NotMapped]
        public int? TotalTickets => Tickets == null ? 0 : Tickets.Count;
        //public int? TotalTickets => Tickets == null ? 0 : Tickets.Count;
        public List<Passenger>? Passengers { get; set; } //= new List<Passenger>();
        [NotMapped]
        public int? TotalPassengers => Passengers == null ? 0 : Passengers.Count;
    }
}
