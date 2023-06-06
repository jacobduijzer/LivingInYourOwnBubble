CREATE OR REPLACE FUNCTION process_incoming_cow_event()
    RETURNS TRIGGER AS
$$
DECLARE
    from_farm_id               INTEGER;
    from_farm_type             INTEGER;
    to_farm_id                 INTEGER;
    to_farm_type               INTEGER;
    cow_id                     INTEGER;
    date_of_birth              DATE;
    date_first_calved          DATE;
    target_animal_category     INTEGER;
    last_known_animal_category INTEGER;
    calved                     BOOLEAN;
    last_order                 INTEGER;
BEGIN
    -- GET TO FARM DETAILS
    SELECT "Id", "FarmType" INTO from_farm_id, from_farm_type FROM "Farms" WHERE "UBN" = NEW."UBN_1";
    IF from_farm_id IS NULL THEN
        RAISE EXCEPTION 'Farm with UBN % not found', NEW."UBN_1";
    END IF;

    CASE
        -- BIRTH
        WHEN NEW."Reason" = 0 THEN BEGIN
            -- CREATE COW
            INSERT INTO "Cows" ("LifeNumber", "Gender", "DateOfBirth")
            VALUES (NEW."LifeNumber", NEW."Gender", NEW."DateOfBirth")
            RETURNING "Id" INTO cow_id;

            -- LINK COW TO FARM
            INSERT INTO "FarmCows" ("FarmId", "CowId", "StartDate")
            VALUES (from_farm_id, cow_id, NEW."DateOfBirth");

            -- DETERMINE CATEGORY
            SELECT "category"
            INTO target_animal_category
            FROM determine_cow_category(NEW."Gender", false, 0, from_farm_type);
           
            -- CREATE EVENT
            INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
            VALUES (from_farm_id, cow_id, NEW."Reason", target_animal_category, NEW."DateOfBirth", 0);
        END;

        -- DEPARTURE (& ARRIVAL)
        WHEN NEW."Reason" = 2 THEN BEGIN
            -- GET COW DETAILS
            SELECT "Id", "DateOfBirth", "DateFirstCalved"
            INTO cow_id, date_of_birth, date_first_calved
            FROM "Cows"
            WHERE "LifeNumber" = NEW."LifeNumber";
            IF cow_id IS NULL THEN
                RAISE EXCEPTION 'Cow with LifeNumber % not found', NEW."LifeNumber";
            END IF;

            -- GET LAST KNOWN CATEGORY
            SELECT "Category"
            INTO last_known_animal_category
            FROM "CowEvents"
            WHERE "CowId" = cow_id
              AND "FarmId" = from_farm_id
            ORDER BY "Order" DESC
            LIMIT 1;

            -- SET END DATE OF FarmCow
            UPDATE "FarmCows"
            SET "EndDate" = NEW."EventDate"
            WHERE "FarmId" = from_farm_id
              AND "CowId" = cow_id;

            -- GET TO FARM DETAILS
            SELECT "Id", "FarmType"
            INTO to_farm_id, to_farm_type
            FROM "Farms"
            WHERE "UBN" = NEW."UBN_2";
            IF to_farm_id IS NULL THEN
                RAISE EXCEPTION 'Farm with UBN % not found', NEW."UBN_2";
            END IF;

            -- CONNECT COW TO NEW FARM
            INSERT INTO "FarmCows" ("FarmId", "CowId", "StartDate")
            VALUES (to_farm_id, cow_id, NEW."EventDate");

            -- DETERMINE TARGET CATEGORY
            calved := date_first_calved IS NOT NULL;
            SELECT "category"
            INTO target_animal_category
            FROM determine_cow_category(NEW."Gender", calved,
                                        calculate_age_in_days(date_of_birth, NEW."EventDate"::DATE), to_farm_type);

            -- LAST ORDER
            SELECT "Order"
            INTO last_order
            FROM "CowEvents"
            WHERE "FarmId" = from_farm_id
              AND "CowId" = cow_id
              AND "EventDate" = NEW."EventDate"::DATE
            ORDER BY "Order" DESC
            LIMIT 1;
            IF last_order IS NULL THEN
               last_order := 0; 
            ELSE 
                last_order = last_order + 1;
            END IF;

            -- CREATE DEPARTURE 
            INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
            VALUES (from_farm_id, cow_id, NEW."Reason", last_known_animal_category, NEW."EventDate", last_order);

            -- CREATE ARRIVAL
            INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
            VALUES (to_farm_id, cow_id, 1, target_animal_category, NEW."EventDate", 0);
        END;

        -- CALVED
        WHEN NEW."Reason" = 3 THEN BEGIN
            -- GET COW DETAILS
            SELECT "Id", "DateOfBirth" INTO cow_id, date_of_birth FROM "Cows" WHERE "LifeNumber" = NEW."LifeNumber";
            IF cow_id IS NULL THEN
                RAISE EXCEPTION 'Cow with LifeNumber % not found', NEW."LifeNumber";
            END IF;

            -- SET DateFirstCalved OF Cow
            UPDATE "Cows"
            SET "DateFirstCalved" = NEW."EventDate"
            WHERE "Id" = cow_id;

            -- DETERMINE CATEGORY
            SELECT "category"
            INTO target_animal_category
            FROM determine_cow_category(NEW."Gender", true, calculate_age_in_days(date_of_birth, NEW."EventDate"::DATE),
                                        from_farm_type);

            -- LAST ORDER
            SELECT "Order"
            INTO last_order
            FROM "CowEvents"
            WHERE "FarmId" = from_farm_id
              AND "CowId" = cow_id
              AND "EventDate" = NEW."EventDate"::DATE
            ORDER BY "Order" DESC
            LIMIT 1;
            IF last_order IS NULL THEN
                last_order := 0;
            ELSE
                last_order = last_order + 1;
            END IF;

            -- CREATE BIRTH
            INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
            VALUES (from_farm_id, cow_id, NEW."Reason", target_animal_category, NEW."EventDate", last_order);
        END;

        -- DEATH EVENT
        WHEN NEW."Reason" = 4 THEN BEGIN
            -- GET COW DETAILS
            SELECT "Id" INTO cow_id FROM "Cows" WHERE "LifeNumber" = NEW."LifeNumber";
            IF cow_id IS NULL THEN
                RAISE EXCEPTION 'Cow with LifeNumber % not found', NEW."LifeNumber";
            END IF;

            -- SET END DATE OF FarmCow
            UPDATE "FarmCows"
            SET "EndDate" = NEW."EventDate"
            WHERE "FarmId" = from_farm_id
              AND "CowId" = cow_id;

            -- SET DateOfDeath OF Cow
            UPDATE "Cows"
            SET "DateOfDeath" = NEW."EventDate"
            WHERE "Id" = cow_id;

            -- GET LAST KNOWN CATEGORY
            SELECT "Category"
            INTO last_known_animal_category
            FROM "CowEvents"
            WHERE "CowId" = cow_id
              AND "FarmId" = from_farm_id
            ORDER BY "Order" DESC
            LIMIT 1;

            -- LAST ORDER
            SELECT "Order"
            INTO last_order
            FROM "CowEvents"
            WHERE "FarmId" = from_farm_id
              AND "CowId" = cow_id
              AND "EventDate" = NEW."EventDate"::DATE
            ORDER BY "Order" DESC
            LIMIT 1;
            IF last_order IS NULL THEN
                last_order := 0;
            ELSE
                last_order = last_order + 1;
            END IF;

            -- CREATE DEATH EVENT
            INSERT INTO "CowEvents" ("FarmId", "CowId", "Reason", "Category", "EventDate", "Order")
            VALUES (from_farm_id, cow_id, NEW."Reason", last_known_animal_category, NEW."EventDate", last_order);
        END;
        ELSE RAISE EXCEPTION 'This reason is not implemented: %', NEW."Reason";
    END CASE;

    DELETE FROM "IncomingCowEvents" WHERE "Id" = NEW."Id";

    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

-- TRIGGER
DROP TRIGGER IF EXISTS "incoming_cow_event_trigger" ON "IncomingCowEvents";

CREATE TRIGGER incoming_cow_event_trigger
    AFTER INSERT
    ON "IncomingCowEvents"
    FOR EACH ROW
EXECUTE FUNCTION process_incoming_cow_event();