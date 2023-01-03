using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCowsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Cows",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Cows");
        }
    }
}
