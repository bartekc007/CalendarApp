using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class UserFriendshipRequestSendersConfiguration : IEntityTypeConfiguration<UserFriendshipRequestSender>
    {
        public void Configure(EntityTypeBuilder<UserFriendshipRequestSender> builder)
        {
            builder.ToTable("UserFriendshipRequestSenders");
            builder.Property(u => u.UserId)
                .IsRequired(true);
            builder.Property(u => u.Person2Id)
                .IsRequired(true);

            builder.HasData
            (
                new UserFriendshipRequestSender
                {
                    UserFriendshipRequestSenderId = 1,
                    UserId = 1,
                    Person2Id = 5
                },
                new UserFriendshipRequestSender
                {
                    UserFriendshipRequestSenderId = 2,
                    UserId = 7,
                    Person2Id = 1
                },
                new UserFriendshipRequestSender
                {
                    UserFriendshipRequestSenderId = 3,
                    UserId = 3,
                    Person2Id = 7
                },
                new UserFriendshipRequestSender
                {
                    UserFriendshipRequestSenderId = 4,
                    UserId = 10,
                    Person2Id = 9
                }
            );
        }
    }
}
