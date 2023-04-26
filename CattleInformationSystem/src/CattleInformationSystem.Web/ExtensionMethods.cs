namespace CattleInformationSystem.Web;

public static class ExtensionMethods
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) =>
        source.Select((item, index) => (item, index));
}