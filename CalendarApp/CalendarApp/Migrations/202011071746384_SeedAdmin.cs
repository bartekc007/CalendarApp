namespace CalendarApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8ba06d34-5337-4e92-b489-dd71c4273b86', N'admin@gmail.com', 0, N'ALgxK4Q5mbjKkfbzs7mWShie5Ti5e6K8sTQoTzR3OC7jwvkEi/umNdJ1WwOAnl1vJA==', N'98da95d3-a531-4c23-a289-ce58be6e3afc', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c8fd5cef-1e62-42c9-837c-6fea66cb629c', N'Administrator')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8ba06d34-5337-4e92-b489-dd71c4273b86', N'c8fd5cef-1e62-42c9-837c-6fea66cb629c')
");
        }
        
        public override void Down()
        {
        }
    }
}
