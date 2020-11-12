using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalendarApp.Models;
using Microsoft.AspNet.Identity;

namespace CalendarApp.Controllers
{
    //[Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetEvents()
        {
            _context = new ApplicationDbContext();
            string userID = User.Identity.GetUserId();

            var events = _context.Events.Where(e=>e.UserID==userID);
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            _context = new ApplicationDbContext();

            if (e.EventId > 0)
            {
                // Update Event
                var model = _context.Events.Where(m => m.EventId == e.EventId).FirstOrDefault();
                if (model != null)
                {
                    model.Subject = e.Subject;
                    model.Description = e.Description;
                    model.ThemeColor = e.ThemeColor;
                    model.TimeStart = e.TimeStart;
                    model.TimeEnd = e.TimeEnd;
                    model.IsFullDay = e.IsFullDay;
                    model.IsPublic = e.IsPublic;
                    model.UserID = e.UserID;
                }
            }
            else
            {
                e.UserID = User.Identity.GetUserId();
                _context.Events.Add(e);
            }

            _context.SaveChanges();
            status = true;

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventId)
        {
            var status = false;
            _context = new ApplicationDbContext();

            var model = _context.Events.Where(m => m.EventId == eventId).FirstOrDefault();
            if (model != null)
            {
                _context.Events.Remove(model);
                _context.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}