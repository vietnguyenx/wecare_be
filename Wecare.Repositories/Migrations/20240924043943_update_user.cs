using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wecare.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthMetricId",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "DiseaseType",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiseaseType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "HealthMetricId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
