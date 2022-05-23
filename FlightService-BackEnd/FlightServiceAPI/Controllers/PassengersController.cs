using FlightServiceAPI.Data;
using FlightServiceAPI.DTO;
using FlightServiceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly FSContext _context;
        private readonly ILogger<PassengersController> _logger;


        public PassengersController(ILogger<PassengersController> logger, FSContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Passengers
        [HttpGet]
        [ProducesResponseType(typeof(Passenger), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers() => await _context.Passengers.ToListAsync();

        // GET: api/Passengers/passengerId
        [HttpGet("{passengerId}")]
        [ProducesResponseType(typeof(Passenger), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Passenger>> GetPassenger(int passengerId)
        {

            var passenger = await _context.Passengers
                .FirstAsync(p => p.PassengerId == passengerId);

            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Passenger>> PostPassenger(PassengerDTO passenger)
        {
            var p = new Passenger
            {
                Name = passenger.Name,
                Job = passenger.Job,
                Email = passenger.Email,
                Age = passenger.Age
            };

            _context.Passengers.Add(p);
            await _context.SaveChangesAsync();

            return Ok(p);
            //CreatedAtAction("GetPassenger", new { passengerId = p.PassengerId });
        }


        // PUT: api/Passengers/passengerId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{passengerId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutPassenger(int passengerId, PassengerDTO passenger)
        {
            var p = await _context.Passengers.FindAsync(passengerId);
            if (p != null)
            {
                p.Age = passenger.Age;
                p.Name = passenger.Name;
                p.Email = passenger.Email;
                p.Job = passenger.Job;

                _context.Entry(p).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passengerId))
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

        // PUT: api/Passengers/passengerId/flightId
        [HttpPut("{passengerId}/{flightId}/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutFlight(int passengerId, int flightId)
        {
            var passenger = await _context.Passengers.FindAsync(passengerId);
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null)
            {
                return NotFound();
            }
            if (passenger == null)
            {
                return NotFound();
            }
            if (flight.Passengers?.Count < flight.PassengerLimit)
            {
                passenger.Flights.Add(flight);
                Ticket ticket = new Ticket();
                passenger.Tickets?.Add(ticket);
                flight.Passengers.Add(passenger);

            }
            await _context.SaveChangesAsync();
            return Ok(passenger);
        }


        // DELETE: api/Passengers/passengerId
        [HttpDelete("{passengerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePassenger(int passengerId)
        {
            var passenger = await _context.Passengers.FindAsync(passengerId);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PassengerExists(int passengerId)
        {
            return _context.Passengers.Any(e => e.PassengerId == passengerId);
        }
    }
}