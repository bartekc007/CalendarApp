using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarApp.DTO;
using CalendarApp.Models;
using Microsoft.AspNet.Identity;

namespace CalendarApp.Controllers
{
    public class FriendshipsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Friendships
        public ActionResult Index()
        {
            return View(_context.Friendships.ToList());
        }


        // GET: Friendships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friendship friendship = _context.Friendships.Find(id);
            if (friendship == null)
            {
                return HttpNotFound();
            }
            return View(friendship);
        }

        // GET: Friendships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Friendships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FriendshipId,Person1Id,Person2Id,isBlocked")] Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                _context.Friendships.Add(friendship);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(friendship);
        }

        // GET: Friendships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friendship friendship = _context.Friendships.Find(id);
            if (friendship == null)
            {
                return HttpNotFound();
            }
            return View(friendship);
        }

        // POST: Friendships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FriendshipId,Person1Id,Person2Id,isBlocked")] Friendship friendship)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(friendship).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(friendship);
        }

        // GET: Friendships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Friendship friendship = _context.Friendships.Find(id);
            if (friendship == null)
            {
                return HttpNotFound();
            }
            return View(friendship);
        }

        // POST: Friendships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Friendship friendship = _context.Friendships.Find(id);
            _context.Friendships.Remove(friendship);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search()
        {
            _context = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            var AppUsers = _context.Users.Where(u => u.Id != userId).ToList();
            List<Friendship> friends = new List<Friendship>();
            List<string> friendsIds = new List<string>();

            List<UserDTO> users = new List<UserDTO>();
            foreach (var user in AppUsers)
            {
                friends = _context.Friendships.Where(f => f.Person1Id == userId || f.Person2Id == userId).ToList();
                foreach (var item in friends)
                {
                    if (item.Person1Id == userId)
                        friendsIds.Add(item.Person2Id);
                    else
                        friendsIds.Add(item.Person1Id);
                }
                var userDTO = new UserDTO(user.Id, user.UserName,friendsIds);
                users.Add(userDTO);
            }
            return View(users);
        }
        public ActionResult SendRequest(UserDTO user)
        {
            _context = new ApplicationDbContext();
            string thisUserId = User.Identity.GetUserId();
            UserFriendshipRequestSender ufrs = new UserFriendshipRequestSender();
            ufrs.UserId = thisUserId;
            ufrs.Person2Id = user.Id;
            _context.UserFriendshipRequestSenders.Add(ufrs);
            _context.SaveChanges();
            return RedirectToAction("Search");
        }

        public ActionResult FriendshipRequests()
        {
            string userId = User.Identity.GetUserId();
            var requests = _context.UserFriendshipRequestSenders.Where(f => f.Person2Id == userId).ToList();
            List<UserFriendshipRequestSenderDTO> data = new List<UserFriendshipRequestSenderDTO>();
            UserFriendshipRequestSenderDTO objectDTO = new UserFriendshipRequestSenderDTO();
            foreach (var item in requests)
            {
                objectDTO.Person2Name = _context.Users.Where(u => u.Id == item.UserId).Select(u => u.UserName).FirstOrDefault();
                objectDTO.UserId = item.Person2Id;
                objectDTO.Person2Id = item.UserId;
                objectDTO.IsAccepetd = false;
                data.Add(objectDTO);
            }

            return View(data);
        }

        public ActionResult AddFriend(UserFriendshipRequestSenderDTO objectDTO)
        {
            _context = new ApplicationDbContext();
            string thisUserId = User.Identity.GetUserId();
            Friendship friendship = new Friendship();
            if(objectDTO.IsAccepetd == true)
            {
                friendship.Person1Id = thisUserId;
                friendship.Person2Id = objectDTO.Person2Id;
                friendship.isBlocked = false;
                _context.Friendships.Add(friendship);

                var request = _context.UserFriendshipRequestSenders.Where(r => (r.UserId == thisUserId && r.Person2Id == objectDTO.Person2Id) || (r.UserId == objectDTO.Person2Id && r.Person2Id == thisUserId)).FirstOrDefault();
                if (request != null)
                    _context.UserFriendshipRequestSenders.Remove(request);
            }
            else
            {
                var request = _context.UserFriendshipRequestSenders.Where(r => (r.UserId == thisUserId && r.Person2Id == objectDTO.Person2Id) || (r.UserId == objectDTO.Person2Id && r.Person2Id == thisUserId)).FirstOrDefault();
                if (request != null)
                    _context.UserFriendshipRequestSenders.Remove(request);
            }
            _context.SaveChanges();
            return RedirectToAction("Search");
        }
    }
}
