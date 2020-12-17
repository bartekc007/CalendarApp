using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CalendarApp.DTO;
using CalendarApp.Models;

namespace CalendarApp.Controllers.api
{
    public class FriendshipsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Friendships
        public IQueryable<Friendship> GetFriendships()
        {
            return _context.Friendships;
        }

        // GET: api/Friendships/5
        [ResponseType(typeof(List<UserDTO>))]
        public async Task<List<UserDTO>> GetFriendship(string userId)
        {
            var AppUsers = await _context.Users.Where(u => u.Id != userId && u.Id != "8ba06d34-5337-4e92-b489-dd71c4273b86").ToListAsync();
            List<Friendship> friends = new List<Friendship>();
            List<string> friendsIds = new List<string>();

            List<UserDTO> friendships = new List<UserDTO>();
            foreach (var user in AppUsers)
            {
                friends = await _context.Friendships.Where(f => f.Person1Id == userId || f.Person2Id == userId).ToListAsync();
                foreach (var item in friends)
                {
                    if (item.Person1Id == userId)
                        friendsIds.Add(item.Person2Id);
                    else
                        friendsIds.Add(item.Person1Id);
                }
                var userDTO = new UserDTO(user.Id, user.UserName, friendsIds);
                friendships.Add(userDTO);
            }

            return friendships;
        }

        // PUT: api/Friendships/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFriendship(int id, Friendship friendship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendship.FriendshipId)
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Friendships
        [ResponseType(typeof(Friendship))]
        public async Task<IHttpActionResult> PostFriendship(Friendship friendship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = friendship.FriendshipId }, friendship);
        }

        // DELETE: api/Friendships/5
        [ResponseType(typeof(Friendship))]
        public async Task<IHttpActionResult> DeleteFriendship(int id)
        {
            Friendship friendship = await _context.Friendships.Where(f=>f.FriendshipId==id).FirstAsync();
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return Ok(friendship);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendshipExists(int id)
        {
            return _context.Friendships.Count(e => e.FriendshipId == id) > 0;
        }

        

        public async Task<string> GetUserName(string id)
        {
            return await _context.Users.Where(u => u.Id == id).Select(u => u.UserName).FirstAsync();
        }

    }
}