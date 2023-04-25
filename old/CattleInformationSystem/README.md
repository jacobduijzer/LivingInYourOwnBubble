# Cattle Information System

## Animal Passport

Animal
LifeNumber => 'NL12345', unique
Gender => Male / Female
Calved => bool

Events
Where (unique business number)
What (born, bought, sold, died, calved, transitioned)
EventDate

``` json
{
    "LifeNumber": "NL12345",
    "Gender": "Female",
    "DateCalved": "",
    "DateOfBirth": "2021-12-01T00:00:00",
    "DateOfDeath": "",
    "LifeNumberOfMother": "NL12344",
    "Events": [{
            "LocationNumber": "12345678",
            "StartDate": "2021-12-01T00:00:00",
            "EndDate": "",
            "Reason": "Born"
    }]
 }
```

```
var sp = @"create or replace procedure ProcessRawCowData()
as $$
declare raw_cow_record record;
begin
	for raw_cow_record in select ""LifeNumber"", ""Gender"" from ""RawCowData""		
            loop
                raise notice '% - %', raw_cow_record.""LifeNumber"", raw_cow_record.""Gender"";		

                INSERT INTO ""Cows"" (""LifeNumber"", ""Gender"") VALUES (raw_cow_record.""LifeNumber"", raw_cow_record.""Gender"") 
                ON CONFLICT (""LifeNumber"") DO 
                    UPDATE SET ""Gender"" = raw_cow_record.""Gender"";

                DELETE FROM ""RawCowData"" WHERE ""LifeNumber""=raw_cow_record.""LifeNumber"";
            end loop;

            end;$$
            language plpgsql";

            migrationBuilder.Sql(sp);
```

Bepaal geboorte
Bepaal aan- en afvoer
Bepaal eerste afkalving
Bepaal leeftijdsovergangen
Bepaal Sterfte

-- 	-- BORN
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" = 0

-- Calved
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" = 4

	-- Arrived & Sold
	-- Ordering: LocationNumber, then Arrived, Sold	
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" IN (1, 2)
-- 	ORDER BY "OccuredAt", "LocationNumber", "Reason"

-- Died
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" = 3


-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	ORDER BY "OccuredAt"


-- 	-- BORN
-- 

-- DEFAULT ProductionTarget = 1 (Milk)
-- Does location exist? otherwise Milk
DO
$$
DECLARE cow_record record;
DECLARE location_record record;
DECLARE category_record record;
BEGIN
SELECT *
INTO cow_record
FROM "RawCowData", "RawCowEventData"
WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
AND "Reason" = 0
AND "LifeNumber" = 'NL1234';
RAISE NOTICE 'Gender: %, LocationNumber: %, Reason: %, OccuredAt: %', cow_record."Gender", cow_record."LocationNumber", cow_record."Reason", cow_record."OccuredAt";

	-- PRODUCTION TARGET
	-- TODO: Default when not found
	SELECT * 
	INTO location_record
	FROM "FarmLocations"
	WHERE "LocationNumber" = cow_record."LocationNumber";
	RAISE NOTICE 'ProductionTarget: %', location_record."ProductionTarget";
	
	-- ANIMAL CATEGORY
	SELECT *
	FROM "AnimalCategories"
	INTO category_record
	WHERE "Gender" = cow_record."Gender"
	AND "ProductionTarget" = location_record."ProductionTarget"
	AND "AgeInDays" = 0
	AND "AgeInMonths" = 0
	AND "AgeInYears" = 0
	LIMIT 1;
	RAISE NOTICE 'Animal Category: % (Id: %)', category_record."Category", category_record."Id";

END $$;

-- Calved
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" = 4

	-- Arrived & Sold
	-- Ordering: LocationNumber, then Arrived, Sold	
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" IN (1, 2)
-- 	ORDER BY "OccuredAt", "LocationNumber", "Reason"

-- Died
-- 	SELECT *
-- 	FROM "RawCowData", "RawCowEventData"
-- 	WHERE "RawCowData"."Id" = "RawCowEventData"."RawCowDataId"
-- 	AND "Reason" = 3


	