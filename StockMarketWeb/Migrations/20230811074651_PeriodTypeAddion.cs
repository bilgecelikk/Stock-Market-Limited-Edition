using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class PeriodTypeAddion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodType",
                table: "IndividualData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "IndividualData");
        }
    }
}
