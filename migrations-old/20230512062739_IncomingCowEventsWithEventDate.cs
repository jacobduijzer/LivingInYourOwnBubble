using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncomingCowEventsWithEventDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "IncomingCowEvents",
                newName: "EventDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EventDate",
                table: "CowEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateOnly.MinValue);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "CowEvents");

            migrationBuilder.RenameColumn(
                name: "EventDate",
                table: "IncomingCowEvents",
                newName: "CreatedOn");
        }
    }
}
