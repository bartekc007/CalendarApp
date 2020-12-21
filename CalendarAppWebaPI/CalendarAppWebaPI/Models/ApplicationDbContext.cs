using CalendarAppWebaPI.DBConfigurationModels;
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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new UserConfiguration());
            modelbuilder.ApplyConfiguration(new EventConfiguration());
            modelbuilder.ApplyConfiguration(new UserFriendshipRequestSendersConfiguration());
            modelbuilder.ApplyConfiguration(new FriendshipsConfiguration());
            modelbuilder.ApplyConfiguration(new EventRequestSendersConfiguration());
            modelbuilder.ApplyConfiguration(new EventMembersConfiguration());
        }
    }
}
