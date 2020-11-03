namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserIdToEventTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "UserID", c => c.Int(nullable: false));
        }
    }
}
