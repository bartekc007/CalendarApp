using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarAppWebaPI.Models;

namespace CalendarAppWebaPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRequestSendersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventRequestSendersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventRequestSenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventRequestSender>>> GetEventRequestSenders()
        {
            return await _context.EventRequestSenders.ToListAsync();
        }

        // GET: api/EventRequestSenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventRequestSender>> GetEventRequestSender(int id)
        {
            var eventRequestSender = await _context.EventRequestSenders.FindAsync(id);

            if (eventRequestSender == null)
            {
                return NotFound();
            }

            return eventRequestSender;
        }

        // PUT: api/EventRequestSenders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventRequestSender(int id, EventRequestSender eventRequestSender)
        {
            if (id != eventRequestSender.EventRequestSenderId)
            {
                return BadRequest();
            }

            _context.Entry(eventRequestSender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventRequestSenderExists(id))
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

        // POST: api/EventRequestSenders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventRequestSender>> PostEventRequestSender(EventRequestSender eventRequestSender)
        {
            _context.EventRequestSenders.Add(eventRequestSender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventRequestSender", new { id = eventRequestSender.EventRequestSenderId }, eventRequestSender);
        }

        // DELETE: api/EventRequestSenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventRequestSender(int id)
        {
            var eventRequestSender = await _context.EventRequestSenders.FindAsync(id);
            if (eventRequestSender == null)
            {
                return NotFound();
            }

            _context.EventRequestSenders.Remove(eventRequestSender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventRequestSenderExists(int id)
        {
            return _context.EventRequestSenders.Any(e => e.EventRequestSenderId == id);
        }
    }
}
