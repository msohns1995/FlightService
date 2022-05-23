using System;
using System.Collections.Generic;

namespace FlightServiceEF
{
    public partial class Seat
    {
        public DateTime? Date { get; set; }
        public int? FlightNumber { get; set; }
        public int SeatNumber { get; set; }
        public string? PassengerConfirmationNumber { get; set; }
        public virtual Aircraft SeatNumberNavigation { get; set; } = null!;
        public virtual Passenger Passenger { get; set; } = null!;
    }
}
