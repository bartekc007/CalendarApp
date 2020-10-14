namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSomeEventsToDbContext : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[Events] ( [Subject], [Description], [TimeStart], [TimeEnd], [IsFullDay], [ThemeColor]) VALUES ( N'Friend''s birthday', N'', N'2020-12-12 19:00:00', N'2020-12-12 22:00:00', 0, N'green')
INSERT INTO[dbo].[Events]( [Subject], [Description], [TimeStart], [TimeEnd], [IsFullDay], [ThemeColor]) VALUES( N'Trip', N'we''re going for a few days  guys', N'2020-12-23 00:00:00', N'2020-12-27 00:00:00', 0, N'red')");
        }
        
        public override void Down()
        {
        }
    }
}
