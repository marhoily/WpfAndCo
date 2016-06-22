using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> source, 
            string separator = ", ") => string.Join(separator, source);
        public static string Join<T>(this IEnumerable<T> source,
             Func<T, object> selector, string separator = ", ") => string.Join(separator, source.Select(selector));
    }
}