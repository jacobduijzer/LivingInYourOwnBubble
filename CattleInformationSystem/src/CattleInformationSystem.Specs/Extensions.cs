using TechTalk.SpecFlow;

namespace CattleInformationSystem.Specs;

public static class Extensions
{
    public static Dictionary<string, string> ToDictionary(Table table)
    {
        var dictionary = new Dictionary<string, string>();
        foreach (var row in table.Rows)
        {
            dictionary.Add(row[0], row[1]);
        }
        return dictionary;
    }
    
    public static T GetRandomElement<T>(this IEnumerable<T> list)
    {
        // If there are no elements in the collection, return the default value of T
        if (list.Count() == 0)
            return default(T);
 
        // Guids as well as the hash code for a guid will be unique and thus random        
        int hashCode = Math.Abs(Guid.NewGuid().GetHashCode());
        return list.ElementAt(hashCode % list.Count());
    }
}