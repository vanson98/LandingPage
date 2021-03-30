using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandingPage.Domain.Migrations
{
    public partial class productv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlMainImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLImage",
                table: "ProductCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlMainImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "URLImage",
                table: "ProductCategories");
        }
    }
}
