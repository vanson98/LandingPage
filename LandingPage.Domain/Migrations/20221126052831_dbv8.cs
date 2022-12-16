using Microsoft.EntityFrameworkCore.Migrations;

namespace LandingPage.Domain.Migrations
{
    public partial class dbv8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "CustomerContacts");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CustomerContacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "CustomerContacts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "CustomerContacts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketAmount",
                table: "CustomerContacts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketType",
                table: "CustomerContacts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "TicketAmount",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "TicketType",
                table: "CustomerContacts");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CustomerContacts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "CustomerContacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
