using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Authorization;
using CalendarAppWebaPI.Contracts;

namespace CalendarAppWebaPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User, Admin")]
    public class FriendshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerService _logger;

        public FriendshipsController(ApplicationDbContext context,ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendships()
        {
            _logger.LogInfo("Access Friendship Controller");
            return await _context.Friendships.ToListAsync();
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friendship>> GetFriendship(int id)
        {
            _logger.LogDebug("Got A value");
            var friendship = await _context.Friendships.FindAsync(id);

            if (friendship == null)
            {
                return NotFound();
            }

            return friendship;
        }

        // GET: api/Friendships/5/10/AreTheyFriends
        [HttpGet("{id1}/{id2}/AreTheyFriends")]
        public async Task<ActionResult<bool>> GetAreTheyFriends(int id1, int id2)
        {
            _logger.LogError("This is an Error");
            var areTheyFriends = await _context.Friendships
                .Where(f => (f.Person1Id == id1 && f.Person2Id == id2) || (f.Person1Id == id2 && f.Person2Id == id1))
                .FirstOrDefaultAsync();
            if (areTheyFriends == null)
            {
                return false;
            }
            else return true;
        }


        // PUT: api/Friendships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(int id, Friendship friendship)
        {
            _logger.LogWarn("This is a warrning");
            if (id != friendship.FriendshipId)
            {
                return BadRequest();
            }

            var person1 = await _context.UserFriendshipRequestSenders.Where(u => u.UserId == friendship.Person1Id).FirstOrDefaultAsync();
            if (person1 == null)
            {
                return BadRequest();
            }

            var person2 = await _context.UserFriendshipRequestSenders.Where(u => u.UserId == friendship.Person2Id).FirstOrDefaultAsync();
            if (person2 == null)
            {
                return BadRequest();
            }

            _context.Entry(friendship).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
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

        // POST: api/Friendships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
            var person1 = await _context.Users.Where(u => u.UserId == friendship.Person1Id).FirstOrDefaultAsync();
            if (person1 == null)
            {
                return BadRequest();
            }

            var person2 = await _context.Users.Where(u => u.UserId == friendship.Person2Id).FirstOrDefaultAsync();
            if (person2 == null)
            {
                return BadRequest();
            }

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetFriendship", new { id = friendship.FriendshipId }, friendship);
        }

        // DELETE: api/Friendships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendship(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendshipExists(int id)
        {
            return _context.Friendships.Any(e => e.FriendshipId == id);
        }
    }
}
