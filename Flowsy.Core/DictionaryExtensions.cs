namespace Flowsy.Core;

public static class DictionaryExtensions
{
    public static IReadOnlyDictionary<TKey, TValue> ExceptBy<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> dictionary,
        IEnumerable<TKey> keys
        ) where TKey : notnull
    {
        var filteredKeys = 
            from k in dictionary.Keys
            where !keys.Contains(k)
            select k;
        
        var newDictionary = new Dictionary<TKey, TValue>();
        foreach (var key in filteredKeys)
            newDictionary[key] = dictionary[key];

        return newDictionary;
    }
}