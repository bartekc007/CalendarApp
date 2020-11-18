namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEventRequestSender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventRequestSenders",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.EventId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventRequestSenders");
        }
    }
}
