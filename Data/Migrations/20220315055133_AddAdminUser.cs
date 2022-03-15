using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce_web.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'c4743e8c-4503-48fe-90ae-604b157bbbaf', N'admin@admin.com', N'ADMIN@ADMIN.COM', N'admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAEAACcQAAAAEGDHznNajCSU/i2y+ZZsipcao2mUzouJ5uYaC8ISz8WD6NF2n+UKlOJQd59CUjHNtA==', N'IT672KXLUR55MJKYLD6JWZYMNK7IWPH3', N'1bc3f03b-f46c-4879-9ec2-7ed0212e0960', NULL, 0, 0, NULL, 1, 0, N'Mohamed', N'Ali', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[Users] WHERE Id = 'c4743e8c-4503-48fe-90ae-604b157bbbaf'");
        }
    }
}
