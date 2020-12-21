using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.Property(e => e.UserID)
                .IsRequired(true);
            builder.Property(e => e.TimeEnd)
                .HasDefaultValue(null);

            builder.HasData
            (
                new Event
                {
                    EventId = 1,
                    Subject = "Wyjazd na narty",
                    Description = "Wyjazd na narty z Julka",
                    TimeStart = new DateTime(2020, 12, 20, 10, 30, 0),
                    TimeEnd = new DateTime(2020, 12, 28, 19, 0, 0),
                    IsFullDay = false,
                    ThemeColor = "blue",
                    UserID = 1,
                    IsPublic = false
                },
                new Event
                {
                    EventId = 2,
                    Subject = "Wigilia",
                    Description = "Rodzinna wigilia",
                    TimeStart = new DateTime(2020, 12, 24, 16, 30, 0),
                    TimeEnd = null,
                    IsFullDay = true,
                    ThemeColor = "red",
                    UserID = 2,
                    IsPublic = false
                },
                new Event
                {
                    EventId = 3,
                    Subject = "Sylwester",
                    Description = "Impreza sylwestrowa",
                    TimeStart = new DateTime(2020, 12, 31, 15, 30, 0),
                    TimeEnd = new DateTime(2021, 01, 01, 19, 0, 0),
                    IsFullDay = false,
                    ThemeColor = "blue",
                    UserID = 2,
                    IsPublic = true
                },
                new Event
                {
                    EventId = 4,
                    Subject = "Wieczor literacki",
                    Description = "Spotkanie trzech wieszczy",
                    TimeStart = new DateTime(2021, 01, 15, 19, 30, 0),
                    TimeEnd = null,
                    IsFullDay = true,
                    ThemeColor = "black",
                    UserID = 10,
                    IsPublic = true
                }
            );
        }
    }
}
