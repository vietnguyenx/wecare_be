using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wecare.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_dish_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Dish");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Menu",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Menu",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Menu");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Dish",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
