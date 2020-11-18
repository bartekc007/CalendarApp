namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingEventMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventMembers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.EventID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventMembers");
        }
    }
}
