using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce_web.Data.Migrations
{
    public partial class addOfferTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OfferId",
                table: "Products",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Offers_OfferId",
                table: "Products",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Offers_OfferId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Products_OfferId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Products");

        }
    }
}
