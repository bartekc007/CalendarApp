using CalendarApp.DTO;
using CalendarApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CalendarApp.Controllers
{
    public class EventFriendsController : Controller
    {
        // GET: EventFriends
        ApplicationDbContext _context;
        EventMembersController _eventMembersController;
        EventRequestSendersController _eventRequestSendersController;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListFriendEventInvitations()
        {
            _context = new ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            List<EventRequestSender> invitations = _context.EventRequestSenders.Where(e => e.UserId == userId).ToList();
            List<EventMemberRequestDTO> InvitationsModel = new List<EventMemberRequestDTO>();
            EventMemberRequestDTO Emrd = new EventMemberRequestDTO();
            foreach (var item in invitations)
            {
                Emrd.EventID = item.EventId;
                Emrd.UserId = item.UserId;
                Emrd.UserName = _context.Users.Where(u => u.Id == item.UserId).Select(u => u.UserName).First();
                InvitationsModel.Add(Emrd);
            }

            return View("ListFriendEventInvitations", InvitationsModel);
        }

        public async Task<ActionResult> GetInvitation(EventMemberRequestDTO Emrd)
        {
            _eventMembersController = new EventMembersController();
            _eventRequestSendersController = new EventRequestSendersController();
            EventMembers eventMember = new EventMembers();
            if(Emrd.IsAccepted == true)
            {
                eventMember.EventID = Emrd.EventID;
                eventMember.UserID = Emrd.UserId;
                await _eventMembersController.PostEventMembers(eventMember);
                await _eventRequestSendersController.DeleteEventRequestSender(Emrd.EventID, Emrd.UserId);
            }
            else
            {
                await _eventRequestSendersController.DeleteEventRequestSender(Emrd.EventID, Emrd.UserId);
            }
            return RedirectToAction("ListFriendEventInvitations");
        }

    }
}