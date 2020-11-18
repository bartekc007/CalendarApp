namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserFriendshipRequestSender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFriendshipRequestSenders",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Person2Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.Person2Id });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserFriendshipRequestSenders");
        }
    }
}
