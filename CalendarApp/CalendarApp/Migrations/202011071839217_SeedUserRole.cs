namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cd5cad5e-bcfb-4360-b2ae-d3357ce99460', N'User')");
        }
        
        public override void Down()
        {
        }
    }
}
