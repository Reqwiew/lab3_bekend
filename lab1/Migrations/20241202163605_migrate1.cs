using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab1.Migrations
{
    /// <inheritdoc />
    public partial class migrate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entrances",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarchSize = table.Column<int>(type: "int", nullable: false),
                    BarchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BarchPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrances", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Tariffses",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostTariff = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffses", x => x.ProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrances");

            migrationBuilder.DropTable(
                name: "Tariffses");
        }
    }
}
