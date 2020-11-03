namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EndTimeEventCanBeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "TimeEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "TimeEnd", c => c.DateTime(nullable: false));
        }
    }
}
