using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class EventRequestSendersConfiguration : IEntityTypeConfiguration<EventRequestSender>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EventRequestSender> builder)
        {
            builder.ToTable("EventRequestSenders");
            builder.Property(e => e.EventId)
                .IsRequired(true);
            builder.Property(e => e.UserId)
                .IsRequired(true);

            builder.HasData
            (
                new EventRequestSender
                {
                    EventRequestSenderId = 1,
                    EventId = 2,
                    UserId = 4
                },
                new EventRequestSender
                {
                    EventRequestSenderId = 2,
                    EventId = 3,
                    UserId = 5
                },
                new EventRequestSender
                {
                    EventRequestSenderId = 3,
                    EventId = 3,
                    UserId = 6
                },
                new EventRequestSender
                {
                    EventRequestSenderId = 4,
                    EventId = 4,
                    UserId = 9
                }
            );
        }
    }
}
