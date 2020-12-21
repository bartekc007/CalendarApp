using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class FriendshipsConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships");
            builder.Property(f => f.Person1Id)
                .IsRequired(true);
            builder.Property(f => f.Person2Id)
                .IsRequired(true);
            builder.Property(f => f.isBlocked)
                .HasDefaultValue(false);

            builder.HasData
            (
                new Friendship
                {
                    FriendshipId = 1,
                    Person1Id = 1,
                    Person2Id = 2,
                    isBlocked = false
                },
                new Friendship
                {
                    FriendshipId = 2,
                    Person1Id = 3,
                    Person2Id = 4,
                    isBlocked = false
                },
                new Friendship
                {
                    FriendshipId = 3,
                    Person1Id = 2,
                    Person2Id = 7,
                    isBlocked = false
                },
                new Friendship
                {
                    FriendshipId = 4,
                    Person1Id = 5,
                    Person2Id = 6,
                    isBlocked = false
                },
                new Friendship
                {
                    FriendshipId = 5,
                    Person1Id = 8,
                    Person2Id = 9,
                    isBlocked = true
                }
            );
        }
    }
}
