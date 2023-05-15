CREATE OR REPLACE FUNCTION calculate_age_in_days(date_of_birth DATE, event_date DATE)
    RETURNS INTEGER AS $$
BEGIN
    RETURN (SELECT DATE_PART('day', event_date::timestamp - date_of_birth::timestamp));
END;
$$ LANGUAGE plpgsql;