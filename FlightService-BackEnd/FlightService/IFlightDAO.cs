using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightServiceEF
{
    public interface IFlightDAO
    {
        public List<Flight> GetAllFlights();
        public Flight GetFlight(int flightId);
        public Flight AddFlight(Flight flight);
    }
}
