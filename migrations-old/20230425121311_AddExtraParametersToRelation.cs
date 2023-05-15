using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraParametersToRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "FarmCows",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "FarmCows",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "FarmCows");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "FarmCows");
        }
    }
}
