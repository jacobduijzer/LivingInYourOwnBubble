using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertCowFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR REPLACE PROCEDURE insert_cow(in lifeNumber text,
									 in gender int,
									 in dateOfBirth timestamp with time zone,
									 inout new_cow_id int)
LANGUAGE 'plpgsql'
AS $$
BEGIN
	INSERT INTO ""Cows"" (""Id"", ""LifeNumber"", ""Gender"", ""DateOfBirth"") 
            VALUES (NEXTVAL('""public"".""Cows_Id_seq""'), lifeNumber, gender, dateOfBirth)	
            ON CONFLICT (""LifeNumber"") DO 
                UPDATE SET ""Gender"" = gender, ""DateOfBirth"" = dateOfBirth WHERE ""Cows"".""LifeNumber"" = lifeNumber	
            RETURNING ""Id"" INTO new_cow_id;
            END;
                $$;";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
