using System;
using System.Collections.Generic;
using System.Linq;

static class DictionaryExtensions
{
    public static void Add<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
        dictionary.AddOrGet(key).Add(value);
    }

    public static TValue AddOrGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        where TValue : new()
    {
        return dictionary.AddOrGet(key, () => new TValue());
    }

    public static TValue AddOrGet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> newValue)
    {
        TValue result;

        if (!dictionary.TryGetValue(key, out result))
        {
            result = newValue();
            dictionary[key] = result;
        }

        return result;
    }

#if NET35
    public static bool Contains<TKey, TValue>(this IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value, IEqualityComparer<TValue> valueComparer)
#else
    public static bool Contains<TKey, TValue>(this IReadOnlyDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value, IEqualityComparer<TValue> valueComparer)
#endif
    {
        List<TValue> values;

        if (!dictionary.TryGetValue(key, out values))
            return false;

        return values.Contains(value, valueComparer);
    }

    public static Dictionary<TKey, TValue> ToDictionaryIgnoringDuplicateKeys<TKey, TValue>(this IEnumerable<TValue> values,
                                                                                           Func<TValue, TKey> keySelector,
                                                                                           IEqualityComparer<TKey> comparer = null)
        => ToDictionaryIgnoringDuplicateKeys(values, keySelector, x => x, comparer);

    public static Dictionary<TKey, TValue> ToDictionaryIgnoringDuplicateKeys<TInput, TKey, TValue>(this IEnumerable<TInput> inputValues,
                                                                                                   Func<TInput, TKey> keySelector,
                                                                                                   Func<TInput, TValue> valueSelector,
                                                                                                   IEqualityComparer<TKey> comparer = null)
    {
        var result = new Dictionary<TKey, TValue>(comparer);

        foreach (var inputValue in inputValues)
        {
            var key = keySelector(inputValue);
            if (!result.ContainsKey(key))
                result.Add(key, valueSelector(inputValue));
        }

        return result;
    }
}
