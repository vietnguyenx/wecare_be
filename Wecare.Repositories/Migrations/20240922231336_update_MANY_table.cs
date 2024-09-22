using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wecare.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_MANY_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_Menu_MenuId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Dietitian_DietitianId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuDish_Dish_DishId",
                table: "MenuDish");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuDish_Menu_MenuId",
                table: "MenuDish");

            migrationBuilder.DropTable(
                name: "UserDietPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuDish",
                table: "MenuDish");

            migrationBuilder.DropIndex(
                name: "IX_HealthMetric_UserId",
                table: "HealthMetric");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "DietPlan",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_DietPlan_MenuId",
                table: "DietPlan",
                newName: "IX_DietPlan_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "HealthMetricId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MenuDish",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "SuitableFor",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MenuName",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Menu",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "DietitianId",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuDish",
                table: "MenuDish",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MenuDietPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWId()"),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DietPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDietPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuDietPlan_DietPlan_DietPlanId",
                        column: x => x.DietPlanId,
                        principalTable: "DietPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuDietPlan_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuDish_MenuId",
                table: "MenuDish",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetric_UserId",
                table: "HealthMetric",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuDietPlan_DietPlanId",
                table: "MenuDietPlan",
                column: "DietPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDietPlan_MenuId",
                table: "MenuDietPlan",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_User_UserId",
                table: "DietPlan",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Dietitian_DietitianId",
                table: "Menu",
                column: "DietitianId",
                principalTable: "Dietitian",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDish_Dish_DishId",
                table: "MenuDish",
                column: "DishId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDish_Menu_MenuId",
                table: "MenuDish",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietPlan_User_UserId",
                table: "DietPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Dietitian_DietitianId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuDish_Dish_DishId",
                table: "MenuDish");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuDish_Menu_MenuId",
                table: "MenuDish");

            migrationBuilder.DropTable(
                name: "MenuDietPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuDish",
                table: "MenuDish");

            migrationBuilder.DropIndex(
                name: "IX_MenuDish_MenuId",
                table: "MenuDish");

            migrationBuilder.DropIndex(
                name: "IX_HealthMetric_UserId",
                table: "HealthMetric");

            migrationBuilder.DropColumn(
                name: "HealthMetricId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DietPlan",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_DietPlan_UserId",
                table: "DietPlan",
                newName: "IX_DietPlan_MenuId");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MenuDish",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWId()");

            migrationBuilder.AlterColumn<string>(
                name: "SuitableFor",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MenuName",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Menu",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DietitianId",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Menu",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuDish",
                table: "MenuDish",
                columns: new[] { "MenuId", "DishId" });

            migrationBuilder.CreateTable(
                name: "UserDietPlan",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DietPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDietPlan", x => new { x.UserId, x.DietPlanId });
                    table.ForeignKey(
                        name: "FK_UserDietPlan_DietPlan_DietPlanId",
                        column: x => x.DietPlanId,
                        principalTable: "DietPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDietPlan_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthMetric_UserId",
                table: "HealthMetric",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDietPlan_DietPlanId",
                table: "UserDietPlan",
                column: "DietPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietPlan_Menu_MenuId",
                table: "DietPlan",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Dietitian_DietitianId",
                table: "Menu",
                column: "DietitianId",
                principalTable: "Dietitian",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDish_Dish_DishId",
                table: "MenuDish",
                column: "DishId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuDish_Menu_MenuId",
                table: "MenuDish",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
