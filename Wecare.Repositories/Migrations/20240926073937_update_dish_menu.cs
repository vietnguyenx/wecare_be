using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wecare.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class update_dish_menu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Dish");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Dish",
                newName: "Description");

            migrationBuilder.AddColumn<float>(
                name: "TotalCalories",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalCarbohydrates",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalCholesterol",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalFat",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalFiber",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalProtein",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalPurine",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalSugar",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Protein",
                table: "Dish",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<float>(
                name: "Fat",
                table: "Dish",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<float>(
                name: "Calories",
                table: "Dish",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "Carbohydrates",
                table: "Dish",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Cholesterol",
                table: "Dish",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Fiber",
                table: "Dish",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Purine",
                table: "Dish",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Sugar",
                table: "Dish",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalCarbohydrates",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalCholesterol",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalFat",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalFiber",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalProtein",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalPurine",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "TotalSugar",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "Cholesterol",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "Purine",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "Sugar",
                table: "Dish");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Dish",
                newName: "Ingredients");

            migrationBuilder.AlterColumn<decimal>(
                name: "Protein",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Fat",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Dish",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<decimal>(
                name: "Carbs",
                table: "Dish",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
