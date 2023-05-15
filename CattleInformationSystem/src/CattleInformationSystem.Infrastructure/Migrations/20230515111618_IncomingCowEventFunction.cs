using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CattleInformationSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncomingCowEventFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var incomingCowEventSql = File.ReadAllText("incoming_cow_event.sql");
            migrationBuilder.Sql(incomingCowEventSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
