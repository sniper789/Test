namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                    INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'891cca7a-073c-4225-a5b3-6266a023c9f9', N'CanManageMovies')
                    INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9ac56f61-d66c-4c28-9fc5-13dd1e019ba9', N'admin@vidly.com', 0, N'ACQRzyO55BnpF7CwN2EWFjjN7TKiT1jO7WGaoUigKI0pb3dPXBFRQ+HVrNz6IiVBfA==', N'4aea4dab-b8b4-451e-9c9a-a350dd10a720', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                    INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ebf457d0-c45d-4fdb-a77d-c6297daaf8b0', N'guest@vidly.com', 0, N'AGS65AkRa255brzumTCgDyE85tMSyA/o3BysASD/GalqgJf3dINCt9L6+wI963ldGw==', N'fda8730b-d200-47eb-87d2-ae9a14d2dc8c', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                    INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9ac56f61-d66c-4c28-9fc5-13dd1e019ba9', N'891cca7a-073c-4225-a5b3-6266a023c9f9')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
