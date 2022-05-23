using System;
using System.Collections.Generic;

namespace FlightServiceEF
{
    public partial class Aircraft
    {
        public string AircraftType { get; set; } = null!;
        public int? SeatNumber { get; set; }
        public string? SeatClass { get; set; }
        public int SerialNumber { get; set; }
        public int PassengerLimit { get; set; }

        public virtual Flight SerialNumberNavigation { get; set; } = null!;
        public virtual Seat Seat { get; set; } = null!;
        public ICollection<Seat> Seats { get; set; } = null!;
    }
}
