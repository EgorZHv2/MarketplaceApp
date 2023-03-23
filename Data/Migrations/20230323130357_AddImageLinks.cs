using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddImageLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Shops",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "ImagesLinks",
                table: "Products",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ImagesLinks",
                table: "Products");
        }
    }
}
