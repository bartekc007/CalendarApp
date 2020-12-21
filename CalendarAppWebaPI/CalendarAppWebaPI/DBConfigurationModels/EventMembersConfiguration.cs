using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class EventMembersConfiguration : IEntityTypeConfiguration<EventMembers>
    {
        public void Configure(EntityTypeBuilder<EventMembers> builder)
        {
            builder.ToTable("EventMembers");
            builder.Property(e => e.EventID)
                .IsRequired(true);
            builder.Property(e => e.UserID)
                .IsRequired(true);

            builder.HasData
            (
                new EventMembers
                {
                    EventMembersId = 1,
                    UserID = 2,
                    EventID = 1
                },
                new EventMembers
                {
                    EventMembersId = 2,
                    UserID = 3,
                    EventID = 2
                },
                new EventMembers
                {
                    EventMembersId = 3,
                    UserID = 7,
                    EventID = 3
                },
                new EventMembers
                {
                    EventMembersId = 4,
                    UserID = 8,
                    EventID = 4
                }
            );
        }
    }
}
