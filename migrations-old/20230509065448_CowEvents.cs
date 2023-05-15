using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CowEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CowEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmId = table.Column<int>(type: "integer", nullable: false),
                    CowId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CowEvents_Cows_CowId",
                        column: x => x.CowId,
                        principalTable: "Cows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowEvents_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomingCowEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CowId = table.Column<int>(type: "integer", nullable: false),
                    UBN_1 = table.Column<string>(type: "text", nullable: false),
                    UBN_2 = table.Column<string>(type: "text", nullable: true),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingCowEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomingCowEvents_Cows_CowId",
                        column: x => x.CowId,
                        principalTable: "Cows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CowEvents_CowId",
                table: "CowEvents",
                column: "CowId");

            migrationBuilder.CreateIndex(
                name: "IX_CowEvents_FarmId",
                table: "CowEvents",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingCowEvents_CowId",
                table: "IncomingCowEvents",
                column: "CowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CowEvents");

            migrationBuilder.DropTable(
                name: "IncomingCowEvents");
        }
    }
}
