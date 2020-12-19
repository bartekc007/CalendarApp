/*using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CalendarApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMembers> EventMembers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<UserFriendshipRequestSender> UserFriendshipRequestSenders { get; set; }
        public DbSet<EventRequestSender> EventRequestSenders { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
          
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Event>().HasKey(c => new { c.EventId });

            modelBuilder.Entity<EventMembers>().HasKey(c => new { c.EventMembersId });

            modelBuilder.Entity<Friendship>().HasKey(c => new { c.FriendshipId });

            modelBuilder.Entity<UserFriendshipRequestSender>().HasKey(c => new { c.UserFriendshipRequestSenderId});

            modelBuilder.Entity<EventRequestSender>().HasKey(c => new { c.EventRequestSenderId});
        }
    }
}*/