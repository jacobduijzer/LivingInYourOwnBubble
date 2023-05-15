-- Function for determination of animal category
CREATE OR REPLACE FUNCTION determine_cow_category(gender INTEGER, calved BOOLEAN, age INTEGER, farm_type INTEGER)
    RETURNS TABLE (
        category INTEGER
                  )
AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM "AnimalCategories"
        WHERE "Gender" = gender AND "Calved" = calved AND "FarmType" = farm_type AND "AgeInDays" <= age
    ) THEN
        RETURN QUERY
            SELECT "Category"
            FROM "AnimalCategories"
            WHERE "Gender" = gender AND "Calved" = calved AND "FarmType" = farm_type AND "AgeInDays" <= age
            ORDER BY "AgeInDays" DESC
            LIMIT 1;
    ELSE
        RETURN QUERY
            SELECT 0;
    END IF;
END;
$$ LANGUAGE plpgsql;