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
using System.Web.Http.Results;
using System.Web.Mvc;
using CalendarApp.Models;
using Microsoft.AspNet.Identity;

namespace CalendarApp.Controllers
{
    public class EventsController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        
        // GET: api/Events
                
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEvents()
        {
            string userID = User.Identity.GetUserId();
            var events = _context.Events.Where(c => c.UserID == userID);
            return Ok(events);
        }

        // GET: api/Events/5
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetEvent(int id)
        {
            Event @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [System.Web.Http.HttpPut]
        public async Task<IHttpActionResult> PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventId)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [System.Web.Http.HttpDelete]
        public async Task<JsonResult> DeleteEvent(int id)
        {
            bool status = false;
            Event @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            status = true;
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return new JsonResult { Data = new { status = status } };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Count(e => e.EventId == id) > 0;
        }
    }
}