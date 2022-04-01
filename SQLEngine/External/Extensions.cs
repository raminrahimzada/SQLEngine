using System.Collections.Generic;

namespace SQLEngine;

public static class Extensions
{
    public static string JoinWith<T>(this IEnumerable<T> source, string separator = ",")
    {
        return string.Join(separator, source);
    }

    public static string RemoveString(this string original, params string[] stringArrayToRemove)
    {
        foreach (var toRemove in stringArrayToRemove)
        {
            if (string.IsNullOrWhiteSpace(original)) original = string.Empty;

            if (!string.IsNullOrWhiteSpace(toRemove)) original = original.Replace(toRemove, string.Empty);
        }

        return original;
    }
}