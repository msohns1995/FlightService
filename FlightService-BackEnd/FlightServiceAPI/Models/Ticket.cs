using FlightServiceAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightServiceAPI.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfirmationNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? SeatNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string? GateNumber { get; set; }
        public string? TicketClass { get; set; }
        public string? TicketPrice { get; set; }
        public int? PassengerId { get; set; }
        public int? FlightId { get; set; }
        public Flight? Flight { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
