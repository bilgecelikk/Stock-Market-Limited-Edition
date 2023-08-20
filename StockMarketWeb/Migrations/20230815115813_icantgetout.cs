using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarketWeb.Migrations
{
    /// <inheritdoc />
    public partial class icantgetout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickers",
                table: "Tickers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tickers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickers",
                table: "Tickers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickers",
                table: "Tickers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tickers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tickers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickers",
                table: "Tickers",
                column: "Name");
        }
    }
}
