﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Authorization;
using CalendarAppWebaPI.Contracts;
using CalendarAppWebaPI.DTO;

namespace CalendarAppWebaPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerService _logger;

        public UsersController(ApplicationDbContext context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            _logger.LogInfo("Fetching all Users from the storage");
            var users = await _context.Users.ToListAsync();
            var usersDTO = new List<UserDTO>();
            foreach(var user in users)
            {
                UserDTO userDTO = new UserDTO(user);
                usersDTO.Add(userDTO);
            }
            _logger.LogInfo($"Returning {users.Count} users");
            return usersDTO;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            _logger.LogInfo($"Fetching User with id: {id} from the storage");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogInfo($"No user with id: {id} found");
                return NotFound();
            }
            UserDTO userDTO = new UserDTO(user);
            _logger.LogInfo($"Returning user with id: {id}");
            return userDTO;
        }

        // GET: api/Users/5/Name
        [HttpGet("{id}/Name")]
        public async Task<ActionResult<string>> GetUserName(int id)
        {
            var user = await _context.Users.Where(u=>u.UserId == id).Select(u=>u.Name).FirstOrDefaultAsync();
            _logger.LogDebug("Debug sth");

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5/Lastname
        [HttpGet("{id}/Lastname")]
        public async Task<ActionResult<string>> GetUserLastname(int id)
        {
            var user = await _context.Users.Where(u => u.UserId == id).Select(u => u.LastName).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

       

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                
                if (!UserExists(id))
                {
                    _logger.LogError($"User doesn't exist, {ex.Message}");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"User exist, {ex.Message}");
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInfo("Model is Valid");
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                UserDTO userDTO = new UserDTO(user);
                return CreatedAtAction("GetUser", new { id = user.UserId }, userDTO);
            }
            else
            {
                _logger.LogInfo("Model is not Valid");
                return BadRequest();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarn("No user found");
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
