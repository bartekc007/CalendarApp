namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimaryKeyChanges : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EventMembers");
            DropPrimaryKey("dbo.EventRequestSenders");
            DropPrimaryKey("dbo.UserFriendshipRequestSenders");
            AddColumn("dbo.EventMembers", "EventMembersId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.EventRequestSenders", "EventRequestSenderId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.UserFriendshipRequestSenders", "UserFriendshipRequestSenderId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.EventMembers", "UserID", c => c.String(nullable: false));
            AlterColumn("dbo.EventRequestSenders", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.UserFriendshipRequestSenders", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.UserFriendshipRequestSenders", "Person2Id", c => c.String(nullable: false));
            AddPrimaryKey("dbo.EventMembers", "EventMembersId");
            AddPrimaryKey("dbo.EventRequestSenders", "EventRequestSenderId");
            AddPrimaryKey("dbo.UserFriendshipRequestSenders", "UserFriendshipRequestSenderId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserFriendshipRequestSenders");
            DropPrimaryKey("dbo.EventRequestSenders");
            DropPrimaryKey("dbo.EventMembers");
            AlterColumn("dbo.UserFriendshipRequestSenders", "Person2Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserFriendshipRequestSenders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EventRequestSenders", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EventMembers", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserFriendshipRequestSenders", "UserFriendshipRequestSenderId");
            DropColumn("dbo.EventRequestSenders", "EventRequestSenderId");
            DropColumn("dbo.EventMembers", "EventMembersId");
            AddPrimaryKey("dbo.UserFriendshipRequestSenders", new[] { "UserId", "Person2Id" });
            AddPrimaryKey("dbo.EventRequestSenders", new[] { "EventId", "UserId" });
            AddPrimaryKey("dbo.EventMembers", new[] { "UserID", "EventID" });
        }
    }
}
