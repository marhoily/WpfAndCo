using System;

namespace Generator
{
    public static class StringExtensions
    {
        public static string Before(this string s, string delimiter)
        {
            var idx = s.IndexOf(delimiter, StringComparison.Ordinal);
            if (idx == -1) return s;
            return s.Substring(0, idx);
        }
        public static string AsField(this string name)
        {
            return "_" + char.ToLower(name[0]) + name.Substring(1);
        }
        public static string AsLocal(this string name)
        {
            return char.ToLower(name[0]) + name.Substring(1);
        }
    }
}