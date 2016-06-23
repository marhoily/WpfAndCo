namespace Generator
{
    public static class StringExtensions
    {
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