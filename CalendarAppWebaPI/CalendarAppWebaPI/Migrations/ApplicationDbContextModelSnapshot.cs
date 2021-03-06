﻿// <auto-generated />
using System;
using CalendarAppWebaPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CalendarAppWebaPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CalendarAppWebaPI.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsFullDay")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ThemeColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Description = "Wyjazd na narty z Julka",
                            IsFullDay = false,
                            IsPublic = false,
                            Subject = "Wyjazd na narty",
                            ThemeColor = "blue",
                            TimeEnd = new DateTime(2020, 12, 28, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeStart = new DateTime(2020, 12, 20, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            UserID = 1
                        },
                        new
                        {
                            EventId = 2,
                            Description = "Rodzinna wigilia",
                            IsFullDay = true,
                            IsPublic = false,
                            Subject = "Wigilia",
                            ThemeColor = "red",
                            TimeStart = new DateTime(2020, 12, 24, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        },
                        new
                        {
                            EventId = 3,
                            Description = "Impreza sylwestrowa",
                            IsFullDay = false,
                            IsPublic = true,
                            Subject = "Sylwester",
                            ThemeColor = "blue",
                            TimeEnd = new DateTime(2021, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeStart = new DateTime(2020, 12, 31, 15, 30, 0, 0, DateTimeKind.Unspecified),
                            UserID = 2
                        },
                        new
                        {
                            EventId = 4,
                            Description = "Spotkanie trzech wieszczy",
                            IsFullDay = true,
                            IsPublic = true,
                            Subject = "Wieczor literacki",
                            ThemeColor = "black",
                            TimeStart = new DateTime(2021, 1, 15, 19, 30, 0, 0, DateTimeKind.Unspecified),
                            UserID = 10
                        });
                });

            modelBuilder.Entity("CalendarAppWebaPI.Models.EventMembers", b =>
                {
                    b.Property<int>("EventMembersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("EventMembersId");

                    b.ToTable("EventMembers");

                    b.HasData(
                        new
                        {
                            EventMembersId = 1,
                            EventID = 1,
                            UserID = 2
                        },
                        new
                        {
                            EventMembersId = 2,
                            EventID = 2,
                            UserID = 3
                        },
                        new
                        {
                            EventMembersId = 3,
                            EventID = 3,
                            UserID = 7
                        },
                        new
                        {
                            EventMembersId = 4,
                            EventID = 4,
                            UserID = 8
                        });
                });

            modelBuilder.Entity("CalendarAppWebaPI.Models.EventRequestSender", b =>
                {
                    b.Property<int>("EventRequestSenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EventRequestSenderId");

                    b.ToTable("EventRequestSenders");

                    b.HasData(
                        new
                        {
                            EventRequestSenderId = 1,
                            EventId = 2,
                            UserId = 4
                        },
                        new
                        {
                            EventRequestSenderId = 2,
                            EventId = 3,
                            UserId = 5
                        },
                        new
                        {
                            EventRequestSenderId = 3,
                            EventId = 3,
                            UserId = 6
                        },
                        new
                        {
                            EventRequestSenderId = 4,
                            EventId = 4,
                            UserId = 9
                        });
                });

            modelBuilder.Entity("CalendarAppWebaPI.Models.Friendship", b =>
                {
                    b.Property<int>("FriendshipId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Person1Id")
                        .HasColumnType("int");

                    b.Property<int>("Person2Id")
                        .HasColumnType("int");

                    b.Property<bool>("isBlocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("FriendshipId");

                    b.ToTable("Friendships");

                    b.HasData(
                        new
                        {
                            FriendshipId = 1,
                            Person1Id = 1,
                            Person2Id = 2,
                            isBlocked = false
                        },
                        new
                        {
                            FriendshipId = 2,
                            Person1Id = 3,
                            Person2Id = 4,
                            isBlocked = false
                        },
                        new
                        {
                            FriendshipId = 3,
                            Person1Id = 2,
                            Person2Id = 7,
                            isBlocked = false
                        },
                        new
                        {
                            FriendshipId = 4,
                            Person1Id = 5,
                            Person2Id = 6,
                            isBlocked = false
                        },
                        new
                        {
                            FriendshipId = 5,
                            Person1Id = 8,
                            Person2Id = 9,
                            isBlocked = true
                        });
                });

            modelBuilder.Entity("CalendarAppWebaPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("User");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "bartek@ciesinski.com",
                            LastName = "Ciesinski",
                            Name = "Bartek",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 2,
                            Email = "julia@szandula.com",
                            LastName = "Szandula",
                            Name = "Julia",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 3,
                            Email = "dominika@bazula.com",
                            LastName = "Bazula",
                            Name = "Dominika",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 4,
                            Email = "krystian@nowak.com",
                            LastName = "Nowak",
                            Name = "Krystian",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 5,
                            Email = "paula@mroz.com",
                            LastName = "Mroz",
                            Name = "Paula",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 6,
                            Email = "ola@krason.com",
                            LastName = "Krason",
                            Name = "Ola",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 7,
                            Email = "albert@gmyr.com",
                            LastName = "Gmyr",
                            Name = "Albert",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 8,
                            Email = "adam@mickiewicz.com",
                            LastName = "Mickiewicz",
                            Name = "Adam",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 9,
                            Email = "henryk@sienkiewicz.com",
                            LastName = "Sienkiewicz",
                            Name = "Henryk",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 10,
                            Email = "juliusz@slowacki.com",
                            LastName = "Slowacki",
                            Name = "Juliusz",
                            Password = "Password123."
                        },
                        new
                        {
                            UserId = 100,
                            Email = "admin@calendarapp.com",
                            LastName = "Admin",
                            Name = "Admin",
                            Password = "Password123.",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("CalendarAppWebaPI.Models.UserFriendshipRequestSender", b =>
                {
                    b.Property<int>("UserFriendshipRequestSenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Person2Id")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserFriendshipRequestSenderId");

                    b.ToTable("UserFriendshipRequestSenders");

                    b.HasData(
                        new
                        {
                            UserFriendshipRequestSenderId = 1,
                            Person2Id = 5,
                            UserId = 1
                        },
                        new
                        {
                            UserFriendshipRequestSenderId = 2,
                            Person2Id = 1,
                            UserId = 7
                        },
                        new
                        {
                            UserFriendshipRequestSenderId = 3,
                            Person2Id = 7,
                            UserId = 3
                        },
                        new
                        {
                            UserFriendshipRequestSenderId = 4,
                            Person2Id = 9,
                            UserId = 10
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
