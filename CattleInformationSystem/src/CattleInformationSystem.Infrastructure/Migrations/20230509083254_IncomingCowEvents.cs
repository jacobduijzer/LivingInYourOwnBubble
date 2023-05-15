using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncomingCowEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomingCowEvents_Cows_CowId",
                table: "IncomingCowEvents");

            migrationBuilder.DropIndex(
                name: "IX_IncomingCowEvents_CowId",
                table: "IncomingCowEvents");

            migrationBuilder.RenameColumn(
                name: "CowId",
                table: "IncomingCowEvents",
                newName: "Gender");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "IncomingCowEvents",
                type: "date",
                nullable: false,
                defaultValue: DateOnly.MinValue);

            migrationBuilder.AddColumn<string>(
                name: "LifeNumber",
                table: "IncomingCowEvents",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "IncomingCowEvents");

            migrationBuilder.DropColumn(
                name: "LifeNumber",
                table: "IncomingCowEvents");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "IncomingCowEvents",
                newName: "CowId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingCowEvents_CowId",
                table: "IncomingCowEvents",
                column: "CowId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingCowEvents_Cows_CowId",
                table: "IncomingCowEvents",
                column: "CowId",
                principalTable: "Cows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
