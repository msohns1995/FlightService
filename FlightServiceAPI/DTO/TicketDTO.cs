using FlightServiceAPI.Models;

namespace FlightServiceAPI.DTO
{
    public class TicketDTO
    {
        public string? TicketClass { get; set; }
        public string? TicketPrice { get; set; } 
        public int? PassengerId { get; set; }
        public int? FlightId { get; set; }
    }
}