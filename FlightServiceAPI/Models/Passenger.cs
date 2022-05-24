using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightServiceAPI.Models
{    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PassengerId { get; set; }
        public string? Name { get; set; }
        public string? Job { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }
        public List<Flight>? Flights { get; set; } = new List<Flight>();
        public List<Ticket>? Tickets { get; set; } =  new List<Ticket>();
    }
}
