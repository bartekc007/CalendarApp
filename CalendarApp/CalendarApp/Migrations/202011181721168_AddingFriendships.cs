namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFriendships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        FriendshipId = c.Int(nullable: false, identity: true),
                        Person1Id = c.String(nullable: false),
                        Person2Id = c.String(nullable: false),
                        isBlocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FriendshipId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Friendships");
        }
    }
}
