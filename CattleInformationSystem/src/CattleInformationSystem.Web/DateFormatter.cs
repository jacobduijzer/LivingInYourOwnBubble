namespace CattleInformationSystem.Web;

public static class DateFormatter
{
    public static string Format(DateOnly? dateTime)
    {
        if (dateTime != null)
            return dateTime.Value.ToString("yyyy-MM-dd");

        return string.Empty;
    } 
}