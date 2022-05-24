using FlightServiceAPI.Data;
using FlightServiceAPI.DTO;
using FlightServiceAPI.Models;
#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly FSContext _context;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ILogger<TicketsController> logger, FSContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Tickets
        [HttpGet]
        [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            return await _context.Tickets.ToListAsync();
        }

        // GET: api/Ticket/ConfirmationNumber
        [HttpGet("{confirmationNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Ticket>> GetTicket (int ConfirmationNumber)
        {
            var ticket = await _context.Tickets.FindAsync(ConfirmationNumber);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Ticket/ConfirmationNumber
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkConfirmationNumber=2123754
        [HttpPut("{confirmationNumber}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutTicket(int ticketId, TicketDTO ticket)
        {
            var t = await _context.Tickets.FindAsync(ticketId);
            if (t != null)
            {
                t.TicketClass = ticket.TicketClass;
                t.TicketPrice = ticket.TicketPrice;
                t.PassengerId = ticket.PassengerId;
                t.FlightId = ticket.FlightId;

                _context.Entry(t).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticketId))
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

        // POST: api/Ticketa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Passenger>> PostTicket(TicketDTO ticket)
        {
            var t = new Ticket
            {
                TicketClass = ticket.TicketClass,
                TicketPrice = ticket.TicketPrice,
                PassengerId = ticket.PassengerId,
                FlightId = ticket.FlightId
            };

            _context.Tickets.Add(t);
            await _context.SaveChangesAsync();

            return Ok(t);
        }

        // DELETE: api/Ticket/ConfirmationNumber
        [HttpDelete("{confirmationNumber}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteTicket(int ConfirmationNumber)
        {
            var ticket = await _context.Tickets.FindAsync(ConfirmationNumber);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int ConfirmationNumber)
        {
            return _context.Tickets.Any(e => e.ConfirmationNumber == ConfirmationNumber);
        }
    }
}
