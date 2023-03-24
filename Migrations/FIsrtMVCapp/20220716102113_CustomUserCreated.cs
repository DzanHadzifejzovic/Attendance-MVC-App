using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIsrtMVCapp.Migrations.FIsrtMVCapp
{
    public partial class CustomUserCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferredLanquaqe",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredLanquaqe",
                table: "AspNetUsers");
        }
    }
}
