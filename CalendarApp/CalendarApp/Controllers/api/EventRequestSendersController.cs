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
using System.Web.Mvc;
using CalendarApp.DTO;
using CalendarApp.Models;
using Microsoft.AspNet.Identity;

namespace CalendarApp.Controllers.api
{
    public class EventRequestSendersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/EventRequestSenders
        public IQueryable<EventRequestSender> GetEventRequestSenders()
        {
            return _context.EventRequestSenders;
        }

        // GET: api/EventRequestSenders/5
        [ResponseType(typeof(EventRequestSender))]
        public async Task<IHttpActionResult> GetEventRequestSender(int id)
        {
            EventRequestSender eventRequestSender = await _context.EventRequestSenders.FindAsync(id);
            if (eventRequestSender == null)
            {
                return NotFound();
            }

            return Ok(eventRequestSender);
        }

        // PUT: api/EventRequestSenders/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEventRequestSender(int id, EventRequestSender eventRequestSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventRequestSenders
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(EventRequestSender))]
        public async Task<IHttpActionResult> PostEventRequestSender(EventRequestSender eventRequestSender)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EventRequestSenders.Add(eventRequestSender);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventRequestSenderExists(eventRequestSender.EventRequestSenderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventRequestSender.EventId }, eventRequestSender);
        }

        // DELETE: api/EventRequestSenders/5
        [System.Web.Http.HttpDelete]
        [ResponseType(typeof(EventRequestSender))]
        public async Task<IHttpActionResult> DeleteEventRequestSender(int id)
        {

            EventRequestSender eventRequestSender = await _context.EventRequestSenders.Where(e=>e.EventRequestSenderId==id).FirstAsync();
            if (eventRequestSender == null)
            {
                return NotFound();
            }

            _context.EventRequestSenders.Remove(eventRequestSender);
            await _context.SaveChangesAsync();

            return Ok(eventRequestSender);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventRequestSenderExists(int id)
        {
            return _context.EventRequestSenders.Count(e => e.EventRequestSenderId == id) > 0;
        }

        
    }
}