using System.IO;

namespace Generator
{
    public static class PathExtensions
    {
        public static void EnsureDirectoyExists(this string path, string dir)
        {
            Path.Combine(path, dir).EnsureDirectoyExists();
        }
        public static void EnsureDirectoyExists(this string path)
        {
            if (Directory.Exists(path)) return;
            Path.GetDirectoryName(path).EnsureDirectoyExists();
            Directory.CreateDirectory(path);
        }
    }
}