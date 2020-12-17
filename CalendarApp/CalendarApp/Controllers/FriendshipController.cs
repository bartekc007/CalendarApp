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
using CalendarApp.Controllers.api;
using System.Threading.Tasks;

namespace CalendarApp.Controllers
{
    public class FriendshipController : Controller
    {
        api.FriendshipsController _friendshipsController = new api.FriendshipsController();
        api.UserFriendshipRequestSendersController _userFriendshipRequestSendersController = new api.UserFriendshipRequestSendersController();

        #region delete
        /*private ApplicationDbContext _context = new ApplicationDbContext();

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
        }*/
        #endregion
        public async Task<ActionResult> Search()
        {
            string userId = User.Identity.GetUserId();
            var users = await _friendshipsController.GetFriendship(userId);
            return View(users);
        }
        public ActionResult SendRequest(UserDTO user)
        {
            string thisUserId = User.Identity.GetUserId();
            UserFriendshipRequestSender ufrs = new UserFriendshipRequestSender();
            ufrs.UserId = thisUserId;
            ufrs.Person2Id = user.Id;
            if(!AreTheyFriends(ufrs))
            {
                if(!_userFriendshipRequestSendersController.SentFriendRequest(ufrs))
                {
                    _userFriendshipRequestSendersController.PostUserFriendshipRequestSender(ufrs);
                }
            }
            
            return RedirectToAction("Search");
        }

        public async Task<ActionResult> FriendshipRequests()
        {
            
            string userId = User.Identity.GetUserId();
            var requests = await _userFriendshipRequestSendersController.GetUserFriendshipRequestSender(userId);
            List<UserFriendshipRequestSenderDTO> data = new List<UserFriendshipRequestSenderDTO>();
            UserFriendshipRequestSenderDTO objectDTO = new UserFriendshipRequestSenderDTO();
            foreach (var item in requests)
            {
                objectDTO.Person2Name = await _friendshipsController.GetUserName(item.UserId);
                objectDTO.UserId = item.Person2Id;
                objectDTO.Person2Id = item.UserId;
                objectDTO.IsAccepetd = false;
                data.Add(objectDTO);
            }

            return View(data);
        }

        public async Task<ActionResult> AddFriend(UserFriendshipRequestSenderDTO objectDTO)
        {
            
            string thisUserId = User.Identity.GetUserId();
            Friendship friendship = new Friendship();
            if(objectDTO.IsAccepetd == true)
            {
                friendship.Person1Id = thisUserId;
                friendship.Person2Id = objectDTO.Person2Id;
                friendship.isBlocked = false;
                _friendshipsController.PostFriendship(friendship);

                if (!_userFriendshipRequestSendersController.SentFriendRequest(new UserFriendshipRequestSender() { UserId = friendship.Person1Id, Person2Id = friendship.Person2Id }))
                {
                    await _friendshipsController.PostFriendship(friendship);
                    _userFriendshipRequestSendersController.DeleteUserFriendRequestSenderByUsersIds(friendship.Person1Id, friendship.Person2Id);
                }     
            }
            else
            {
                if (!_userFriendshipRequestSendersController.SentFriendRequest(new UserFriendshipRequestSender() { UserId = friendship.Person1Id, Person2Id = friendship.Person2Id })) ;
                    
                
            }
            return RedirectToAction("Search");
        }

        public bool AreTheyFriends(UserFriendshipRequestSender ufrs)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            if (!_context.Friendships.Where(u => u.Person1Id == ufrs.UserId && u.Person2Id == ufrs.Person2Id).Any()
                && !_context.Friendships.Where(u => u.Person1Id == ufrs.Person2Id && u.Person2Id == ufrs.UserId).Any())
                return false;
            else
                return true;
        }
    }
}
