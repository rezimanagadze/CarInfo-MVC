using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarInfo.Migrations
{
    public partial class AlterTableSuperCarsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuperCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Years = table.Column<int>(type: "int", nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HorsePower = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TopSpeed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Acceleration = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Consumption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperCars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperCars");
        }
    }
}
