using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarInfo.Migrations
{
    public partial class AlterTableSuperCarsCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "SuperCars",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "SuperCars");
        }
    }
}
