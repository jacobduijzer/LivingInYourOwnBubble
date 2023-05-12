using Microsoft.EntityFrameworkCore.Migrations;

namespace CattleInformationSystem.Infrastructure;

public partial class AddStoredProcedure : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
//         var incoming = @$"
// CREATE OR REPLACE FUNCTION process_incoming_cow_event()
// RETURNS TRIGGER AS $$
// DECLARE
//     farm_id INTEGER;
//     farm_type INTEGER;
//     cow_id INTEGER;
//     animal_category INTEGER;
//     calved BOOLEAN;
// BEGIN
//     SELECT "Id", "FarmType" INTO farm_id, farm_type FROM "Farms" WHERE "UBN" = NEW."UBN_1";
//
//         IF farm_id IS NULL THEN
//             RAISE EXCEPTION 'Farm with UBN % not found', NEW."UBN_1";
//         END IF;
//
//         -- Create Cow
//             INSERT INTO "Cows" ("LifeNumber", "Gender", "DateOfBirth")
//         VALUES (NEW."LifeNumber", NEW."Gender", NEW."DateOfBirth")
//         RETURNING "Id" INTO cow_id;
//
//         --     -- Create Cow Event
//         --     -- Get Animal Category
//         --     -- calved := mytable.mydate IS NOT NULL;
//         --     -- TODO when more scenario's are added
//             calved = false;
//         SELECT "category" INTO animal_category FROM determine_cow_category(0, calved, 0, 0);
//         INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
//         VALUES (farm_id, cow_id, NEW."Reason", animal_category, NEW."EventDate", 0);
//
//         -- Link Cow to Farm
//             INSERT INTO "FarmCows" ("FarmId", "CowId", "StartDate")
//         VALUES (farm_id, cow_id, NEW."DateOfBirth");
//
//         DELETE FROM "IncomingCowEvents" WHERE "Id" = NEW."Id";
//
//         RETURN NULL;
//         END;
//             $$ LANGUAGE plpgsql
//         "
//         migrationBuilder.Sql()
    }
}