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
    public class EventMembersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EventMembers
        public IQueryable<EventMembers> GetEventMembers()
        {
            return db.EventMembers;
        }

        // GET: api/EventMembers/5
        [ResponseType(typeof(EventMembers))]
        public async Task<IHttpActionResult> GetEventMembers(int id)
        {
            EventMembers eventMembers = await db.EventMembers.Where(e=>e.EventMembersId==id).FirstAsync();
            if (eventMembers == null)
            {
                return NotFound();
            }

            return Ok(eventMembers);
        }

        // PUT: api/EventMembers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEventMembers(int id, EventMembers eventMembers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventMembers.EventMembersId)
            {
                return BadRequest();
            }

            db.Entry(eventMembers).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventMembers
        [ResponseType(typeof(EventMembers))]
        public async Task<IHttpActionResult> PostEventMembers(EventMembers eventMembers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventMembers.Add(eventMembers);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventMembersExists(eventMembers.EventMembersId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventMembers.UserID }, eventMembers);
        }

        // DELETE: api/EventMembers/5
        [ResponseType(typeof(EventMembers))]
        public async Task<IHttpActionResult> DeleteEventMembers(int id)
        {
            EventMembers eventMembers = await db.EventMembers.FindAsync(id);
            if (eventMembers == null)
            {
                return NotFound();
            }

            db.EventMembers.Remove(eventMembers);
            await db.SaveChangesAsync();

            return Ok(eventMembers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventMembersExists(int id)
        {
            return db.EventMembers.Count(e => e.EventMembersId == id) > 0;
        }
    }
}