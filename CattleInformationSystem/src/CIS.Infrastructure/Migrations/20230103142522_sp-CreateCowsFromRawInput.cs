using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class spCreateCowsFromRawInput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"create or replace procedure ProcessRawCowData()
as $$
declare raw_cow_record record;
begin
	for raw_cow_record in select ""LifeNumber"", ""Gender"" from ""RawCowData""		
            loop
                raise notice '% - %', raw_cow_record.""LifeNumber"", raw_cow_record.""Gender"";		
            /* 
            TODO: update when exists
            https://www.postgresql.org/docs/current/sql-insert.html#SQL-ON-CONFLICT 
            */
            INSERT INTO ""Cows"" (""LifeNumber"") VALUES (raw_cow_record.""LifeNumber"") ON CONFLICT DO NOTHING;
        DELETE FROM ""RawCowData"" WHERE ""LifeNumber""=raw_cow_record.""LifeNumber"";
            end loop;

            end;$$
            language plpgsql";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
