using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightServiceAPI.Data;
using FlightServiceAPI.Models;
using FlightServiceAPI.DTO;

namespace FlightServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FSContext _context;

        private readonly ILogger<FlightsController> _logger;

        public FlightsController(ILogger<FlightsController> logger, FSContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Flights
        [HttpGet]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights() => await _context.Flights.ToListAsync();


        // GET: api/Flights/ID
        [HttpGet("{flightId}")]
        [ProducesResponseType(typeof(Flight), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Flight>> GetFlight(int flightId) => await _context.Flights.FindAsync(flightId);


        // GET: api/Flights/departureAirport
        [HttpGet("{departureAirport}")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlightsByDepartureAirport(string departureAirport)
        {
            var flights = await _context.Flights
                .Where(f => f.DepartureAirport.Contains(departureAirport))
                .ToListAsync();

            return flights;
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Flight>> PostFlight(FlightDTO flight)
        {
            var f = new Flight
            {
                PassengerLimit = flight.PassengerLimit,
                AircraftType = flight.AircraftType,
                DepartureDate = flight.DepartureDate,
                DepartureTime = flight.DepartureTime,
                DepartureAirport = flight.DepartureAirport,
                ArrivalDate = flight.ArrivalDate,
                ArrivalTime = flight.ArrivalTime,                
                ArrivalAirport = flight.ArrivalAirport                 
            };

            _context.Flights.Add(f);
            await _context.SaveChangesAsync();

            return Ok(f);
        }

        // PUT: api/Flights/flightId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{flightId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutFlight(int flightId, FlightDTO flight)
        {
            var f = await _context.Flights.FindAsync(flightId);
            if (f != null)
            {
                f.PassengerLimit = flight.PassengerLimit;
                f.AircraftType = flight.AircraftType;
                f.DepartureDate = flight.DepartureDate;
                f.DepartureTime = flight.DepartureTime;
                f.DepartureAirport = flight.DepartureAirport;
                f.ArrivalDate = flight.ArrivalDate;
                f.ArrivalTime = flight.ArrivalTime;                
                f.ArrivalAirport = flight.ArrivalAirport;               
               

                _context.Entry(f).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else return BadRequest();
        }

        // DELETE: api/Flights/flightId
        [HttpDelete("{flightId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteFlight(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }
   
        private bool FlightExists(int flightId)
        {
            return _context.Flights.Any(e => e.FlightId == flightId);
        }

    }

   
}
