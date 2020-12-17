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
using CalendarApp.Models;

namespace CalendarApp.Controllers.api
{
    public class UserFriendshipRequestSendersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/UserFriendshipRequestSenders
        public IQueryable<UserFriendshipRequestSender> GetUserFriendshipRequestSenders()
        {
            return _context.UserFriendshipRequestSenders;
        }

        // GET: api/UserFriendshipRequestSenders/5
        [ResponseType(typeof(UserFriendshipRequestSender))]
        public async Task<IHttpActionResult> GetUserFriendshipRequestSender(int id)
        {
            UserFriendshipRequestSender userFriendshipRequestSender = await _context.UserFriendshipRequestSenders.FindAsync(id);
            if (userFriendshipRequestSender == null)
            {
                return NotFound();
            }

            return Ok(userFriendshipRequestSender);
        }

        // PUT: api/UserFriendshipRequestSenders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserFriendshipRequestSender(int id, UserFriendshipRequestSender userFriendshipRequestSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserFriendshipRequestSenders
        [ResponseType(typeof(UserFriendshipRequestSender))]
        public async Task<IHttpActionResult> PostUserFriendshipRequestSender(UserFriendshipRequestSender userFriendshipRequestSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserFriendshipRequestSenders.Add(userFriendshipRequestSender);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userFriendshipRequestSender.UserFriendshipRequestSenderId }, userFriendshipRequestSender);
        }

        // DELETE: api/UserFriendshipRequestSenders/5
        [ResponseType(typeof(UserFriendshipRequestSender))]
        public async Task<IHttpActionResult> DeleteUserFriendshipRequestSender(int id)
        {
            UserFriendshipRequestSender userFriendshipRequestSender = await _context.UserFriendshipRequestSenders.FindAsync(id);
            if (userFriendshipRequestSender == null)
            {
                return NotFound();
            }

            _context.UserFriendshipRequestSenders.Remove(userFriendshipRequestSender);
            await _context.SaveChangesAsync();

            return Ok(userFriendshipRequestSender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserFriendshipRequestSenderExists(int id)
        {
            return _context.UserFriendshipRequestSenders.Count(e => e.UserFriendshipRequestSenderId == id) > 0;
        }

        public bool SentFriendRequest(UserFriendshipRequestSender ufrs)
        {
            if (!_context.UserFriendshipRequestSenders.Where(u => u.UserId == ufrs.UserId && u.Person2Id == ufrs.Person2Id).Any()
                    || !_context.UserFriendshipRequestSenders.Where(u => u.UserId == ufrs.Person2Id && u.Person2Id == ufrs.UserId).Any())
                return false;
            else
                return true;
        }

        [ResponseType(typeof(List<UserFriendshipRequestSender>))]
        public async Task<List<UserFriendshipRequestSender>> GetUserFriendshipRequestSender(string id)
        {
            List<UserFriendshipRequestSender> userFriendshipRequestSender = await _context.UserFriendshipRequestSenders.Where(u => u.Person2Id == id).ToListAsync();
            if (!userFriendshipRequestSender.Any())
            {
                return null;
            }

            return userFriendshipRequestSender;
        }

        public async void DeleteUserFriendRequestSenderByUsersIds(string id1, string id2)
        {
            var obj = await  _context.UserFriendshipRequestSenders.Where(u => (u.Person2Id == id1 && u.UserId == id2) || (u.Person2Id == id2 && u.UserId == id1)).FirstAsync();
            _context.UserFriendshipRequestSenders.Remove(obj);
            _context.SaveChanges();
        }
    }
}