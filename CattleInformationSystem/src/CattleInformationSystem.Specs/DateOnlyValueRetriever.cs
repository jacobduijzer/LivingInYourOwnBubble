using System.Globalization;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace CattleInformationSystem.Specs;

public class DateOnlyValueRetriever : StructRetriever<DateOnly>
{
    /// <summary>
    /// Gets or sets the DateTimeStyles to use when parsing the string value.
    /// </summary>
    /// <remarks>Defaults to DateTimeStyles.None.</remarks>
    public static DateTimeStyles DateTimeStyles { get; set; } = DateTimeStyles.None;

    protected override DateOnly GetNonEmptyValue(string value)
    {
        DateOnly.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles, out DateOnly returnValue);
        return returnValue;
    }
}