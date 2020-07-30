namespace VidlyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'100ee042-4237-41ab-8651-47628644566e', N'guest@vidly.com', 0, N'AJJBrWvH/VsnrWYxKDcMaZZZ/02XQqLZLCgAxySG59ogqIwt+4NJvXBqnVySr3wL/A==', N'b6bd12cd-2cbb-4802-89a1-06ff973bd512', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ea1150ce-4ee1-4a6d-ba3f-f2e28268361e', N'admin@vidly.com', 0, N'ANJTbT4V9NAbBHXYl2VriMSQUdjPUHf+K/shgtQMQ60v+fREE8xOFhsn485qg60rqA==', N'a6ff17fb-48a5-45a8-9fc8-1ee6312d7c08', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'eecab503-4794-49e4-863f-f49416ac3743', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ea1150ce-4ee1-4a6d-ba3f-f2e28268361e', N'eecab503-4794-49e4-863f-f49416ac3743')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
