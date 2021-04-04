using Microsoft.EntityFrameworkCore.Migrations;

namespace LandingPage.Domain.Migrations
{
    public partial class removeparentproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCode",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentCode",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
