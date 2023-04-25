namespace CattleInformationSystem.Application;

public static class LinqExtensions
{
    public static TItem RandomElement<TItem>(this IEnumerable<TItem> elements)
    {
        var random = new Random();
        var toSkip = random.Next(0, elements.Count());
        return elements.Skip(toSkip).Take(1).First();
    }
}