using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandingPage.Domain.Migrations
{
    public partial class productimageV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateUserId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductImages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedUserId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierUserId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductImages",
                type: "datetime2",
                nullable: true);
        }
    }
}
