using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce_web.Data.Migrations
{
    public partial class assignAllRolesToAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "INSERT INTO [security].[UserRoles] (UserId,RoleId) SELECT 'c4743e8c-4503-48fe-90ae-604b157bbbaf',Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DELETE FROM [security].[UserRoles] WHERE UserId ='c4743e8c-4503-48fe-90ae-604b157bbbaf'");
        }
    }
}
