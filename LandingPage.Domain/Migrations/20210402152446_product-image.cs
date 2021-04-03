using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LandingPage.Domain.Migrations
{
    public partial class productimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UrlMainImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "URLImage",
                table: "ProductCategories");

            migrationBuilder.AddColumn<string>(
                name: "ParentCode",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Base64",
                table: "ProductImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCode",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Base64",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UrlMainImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "ProductCategories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLImage",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
