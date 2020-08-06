using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiKala.DataAccessLayer.Migrations
{
    public partial class migUpdateStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Store");

            migrationBuilder.AddColumn<bool>(
                name: "MailActivate",
                table: "Store",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MobileActivate",
                table: "Store",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MailActivate",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "MobileActivate",
                table: "Store");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Store",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
