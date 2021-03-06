﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Authorization;
using CalendarAppWebaPI.DTO;

namespace CalendarAppWebaPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class EventMembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventMembers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventMembers>>> GetEventMembers()
        {
            return await _context.EventMembers.ToListAsync();
        }

        // GET: api/EventMembers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventMembers>> GetEventMembers(int id)
        {
            var eventMembers = await _context.EventMembers.FindAsync(id);

            if (eventMembers == null)
            {
                return NotFound();
            }

            return eventMembers;
        }

        // GET: api/EventMembers/5/Members
        [HttpGet("{id}/Members")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetInvitations(int id)
        {
            var invitationsIds = await _context.EventMembers.Where(e => e.EventMembersId == id).Select(e => e.UserID).ToListAsync();
            if (!invitationsIds.Any())
                return NotFound();

            List<UserDTO> invitations = new List<UserDTO>();
            foreach (var userId in invitationsIds)
            {
                var user = await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
                if (user != null)
                {
                    UserDTO userDTO = new UserDTO(user);
                    invitations.Add(userDTO);
                }
            }
            return invitations;
        }

        // PUT: api/EventMembers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventMembers(int id, EventMembers eventMembers)
        {
            if (id != eventMembers.EventMembersId)
            {
                return BadRequest();
            }

            var events = await _context.Events.Where(e => e.EventId == eventMembers.EventID).FirstOrDefaultAsync();
            if(events == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.Where(u => u.UserId == eventMembers.UserID).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest();
            }

            _context.Entry(eventMembers).State = EntityState.Modified;
          
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventMembersExists(id))
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

        // POST: api/EventMembers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventMembers>> PostEventMembers(EventMembers eventMembers)
        {
            var events = await _context.Events.Where(e => e.EventId == eventMembers.EventID).FirstOrDefaultAsync();
            if (events == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.Where(u => u.UserId == eventMembers.UserID).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest();
            }

            _context.EventMembers.Add(eventMembers);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetEventMembers", new { id = eventMembers.EventMembersId }, eventMembers);
        }

        // DELETE: api/EventMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventMembers(int id)
        {
            var eventMembers = await _context.EventMembers.FindAsync(id);
            if (eventMembers == null)
            {
                return NotFound();
            }

            _context.EventMembers.Remove(eventMembers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventMembersExists(int id)
        {
            return _context.EventMembers.Any(e => e.EventMembersId == id);
        }
    }
}
