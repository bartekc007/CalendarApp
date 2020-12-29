using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarAppWebaPI.Migrations
{
    public partial class SeedAdministratorUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "LastName", "Name", "Password", "Role" },
                values: new object[] { 100, "admin@calendarapp.com", "Admin", "Admin", "Password123.", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 100);
        }
    }
}
