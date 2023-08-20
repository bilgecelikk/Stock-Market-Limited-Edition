using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationfordata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataExtractionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndividualData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    WClose = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualData_AllStocks_StockDataId",
                        column: x => x.StockDataId,
                        principalTable: "AllStocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualData_StockDataId",
                table: "IndividualData",
                column: "StockDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndividualData");

            migrationBuilder.DropTable(
                name: "AllStocks");
        }
    }
}
