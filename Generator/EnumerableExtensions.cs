using System.Collections.Generic;

namespace Generator
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> source, 
            string separator = ", ") => string.Join(separator, source);
    }
}