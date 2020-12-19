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
    public class UserFriendshipRequestSendersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserFriendshipRequestSendersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFriendshipRequestSenders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFriendshipRequestSender>>> GetUserFriendshipRequestSenders()
        {
            return await _context.UserFriendshipRequestSenders.ToListAsync();
        }

        // GET: api/UserFriendshipRequestSenders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFriendshipRequestSender>> GetUserFriendshipRequestSender(int id)
        {
            var userFriendshipRequestSender = await _context.UserFriendshipRequestSenders.FindAsync(id);

            if (userFriendshipRequestSender == null)
            {
                return NotFound();
            }

            return userFriendshipRequestSender;
        }

        // PUT: api/UserFriendshipRequestSenders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFriendshipRequestSender(int id, UserFriendshipRequestSender userFriendshipRequestSender)
        {
            if (id != userFriendshipRequestSender.UserFriendshipRequestSenderId)
            {
                return BadRequest();
            }

            _context.Entry(userFriendshipRequestSender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFriendshipRequestSenderExists(id))
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

        // POST: api/UserFriendshipRequestSenders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserFriendshipRequestSender>> PostUserFriendshipRequestSender(UserFriendshipRequestSender userFriendshipRequestSender)
        {
            _context.UserFriendshipRequestSenders.Add(userFriendshipRequestSender);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFriendshipRequestSender", new { id = userFriendshipRequestSender.UserFriendshipRequestSenderId }, userFriendshipRequestSender);
        }

        // DELETE: api/UserFriendshipRequestSenders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFriendshipRequestSender(int id)
        {
            var userFriendshipRequestSender = await _context.UserFriendshipRequestSenders.FindAsync(id);
            if (userFriendshipRequestSender == null)
            {
                return NotFound();
            }

            _context.UserFriendshipRequestSenders.Remove(userFriendshipRequestSender);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserFriendshipRequestSenderExists(int id)
        {
            return _context.UserFriendshipRequestSenders.Any(e => e.UserFriendshipRequestSenderId == id);
        }
    }
}
