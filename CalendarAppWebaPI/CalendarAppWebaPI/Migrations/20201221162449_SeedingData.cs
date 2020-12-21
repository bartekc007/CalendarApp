using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarAppWebaPI.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "Friendships",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.InsertData(
                table: "EventMembers",
                columns: new[] { "EventMembersId", "EventID", "UserID" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 2, 3 },
                    { 3, 3, 7 },
                    { 4, 4, 8 }
                });

            migrationBuilder.InsertData(
                table: "EventRequestSenders",
                columns: new[] { "EventRequestSenderId", "EventId", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 4 },
                    { 2, 3, 5 },
                    { 3, 3, 6 },
                    { 4, 4, 9 }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Description", "IsFullDay", "IsPublic", "Subject", "ThemeColor", "TimeEnd", "TimeStart", "UserID" },
                values: new object[,]
                {
                    { 1, "Wyjazd na narty z Julka", false, false, "Wyjazd na narty", "blue", new DateTime(2020, 12, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Rodzinna wigilia", true, false, "Wigilia", "red", null, new DateTime(2020, 12, 24, 16, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, "Impreza sylwestrowa", false, true, "Sylwester", "blue", new DateTime(2021, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 31, 15, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, "Spotkanie trzech wieszczy", true, true, "Wieczor literacki", "black", null, new DateTime(2021, 1, 15, 19, 30, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.InsertData(
                table: "Friendships",
                columns: new[] { "FriendshipId", "Person1Id", "Person2Id", "isBlocked" },
                values: new object[] { 5, 8, 9, true });

            migrationBuilder.InsertData(
                table: "Friendships",
                columns: new[] { "FriendshipId", "Person1Id", "Person2Id" },
                values: new object[,]
                {
                    { 3, 2, 7 },
                    { 4, 5, 6 },
                    { 1, 1, 2 },
                    { 2, 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserFriendshipRequestSenders",
                columns: new[] { "UserFriendshipRequestSenderId", "Person2Id", "UserId" },
                values: new object[,]
                {
                    { 3, 7, 3 },
                    { 1, 5, 1 },
                    { 2, 1, 7 },
                    { 4, 9, 10 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LastName", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "bartek@ciesinski.com", "Ciesinski", "Bartek", "Password123." },
                    { 2, "julia@szandula.com", "Szandula", "Julia", "Password123." },
                    { 3, "dominika@bazula.com", "Bazula", "Dominika", "Password123." },
                    { 4, "krystian@nowak.com", "Nowak", "Krystian", "Password123." },
                    { 5, "paula@mroz.com", "Mroz", "Paula", "Password123." },
                    { 6, "ola@krason.com", "Krason", "Ola", "Password123." },
                    { 7, "albert@gmyr.com", "Gmyr", "Albert", "Password123." },
                    { 8, "adam@mickiewicz.com", "Mickiewicz", "Adam", "Password123." },
                    { 9, "henryk@sienkiewicz.com", "Sienkiewicz", "Henryk", "Password123." },
                    { 10, "juliusz@slowacki.com", "Slowacki", "Juliusz", "Password123." }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventMembers",
                keyColumn: "EventMembersId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventMembers",
                keyColumn: "EventMembersId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventMembers",
                keyColumn: "EventMembersId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventMembers",
                keyColumn: "EventMembersId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventRequestSenders",
                keyColumn: "EventRequestSenderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventRequestSenders",
                keyColumn: "EventRequestSenderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventRequestSenders",
                keyColumn: "EventRequestSenderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventRequestSenders",
                keyColumn: "EventRequestSenderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumn: "FriendshipId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumn: "FriendshipId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumn: "FriendshipId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumn: "FriendshipId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Friendships",
                keyColumn: "FriendshipId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserFriendshipRequestSenders",
                keyColumn: "UserFriendshipRequestSenderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserFriendshipRequestSenders",
                keyColumn: "UserFriendshipRequestSenderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserFriendshipRequestSenders",
                keyColumn: "UserFriendshipRequestSenderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserFriendshipRequestSenders",
                keyColumn: "UserFriendshipRequestSenderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "isBlocked",
                table: "Friendships",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
