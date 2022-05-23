using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightServiceEF
{
    public partial class Passenger
    {
        public int Id { get; set; }        
        public int ConfirmationNumber { get; set; }
        [NotMapped]
        public string? FirstName { get; set; }
        [NotMapped]
        public string? LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string? Job { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }


        public virtual Seat ConfirmationNumberNavigation { get; set; } = null!;
        public ICollection<Seat> seats { get; set; } = null!;
        public ICollection<Flight> Flights { get; set; } = null!;
    }
}
