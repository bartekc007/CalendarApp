using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DBConfigurationModels
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasData
            (
                new User
                {
                    UserId = 1,
                    Name = "Bartek",
                    LastName = "Ciesinski",
                    Email = "bartek@ciesinski.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 2,
                    Name = "Julia",
                    LastName = "Szandula",
                    Email = "julia@szandula.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 3,
                    Name = "Dominika",
                    LastName = "Bazula",
                    Email = "dominika@bazula.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 4,
                    Name = "Krystian",
                    LastName = "Nowak",
                    Email = "krystian@nowak.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 5,
                    Name = "Paula",
                    LastName = "Mroz",
                    Email = "paula@mroz.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 6,
                    Name = "Ola",
                    LastName = "Krason",
                    Email = "ola@krason.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 7,
                    Name = "Albert",
                    LastName = "Gmyr",
                    Email = "albert@gmyr.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 8,
                    Name = "Adam",
                    LastName = "Mickiewicz",
                    Email = "adam@mickiewicz.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 9,
                    Name = "Henryk",
                    LastName = "Sienkiewicz",
                    Email = "henryk@sienkiewicz.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 10,
                    Name = "Juliusz",
                    LastName = "Slowacki",
                    Email = "juliusz@slowacki.com",
                    Password = "Password123."
                },
                new User
                {
                    UserId = 100,
                    Name = "Admin",
                    LastName = "Admin",
                    Email = "admin@calendarapp.com",
                    Password = "Password123.",
                    Role = "Admin"
                }
            );
        }
    }
}
