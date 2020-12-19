using Microsoft.EntityFrameworkCore;

namespace CalendarAppWebaPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventMembers> EventMembers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<UserFriendshipRequestSender> UserFriendshipRequestSenders { get; set; }
        public DbSet<EventRequestSender> EventRequestSenders { get; set; }
    }
}
