using System;
using System.IO;

namespace Generator
{
    public sealed class TempFolder : IDisposable
    {
        private readonly string _fullName;

        public TempFolder()
        {
            _fullName = Path.GetTempFileName();
            File.Delete(_fullName);
            Directory.CreateDirectory(_fullName);
        }

        public void Dispose()
        {
            if (Directory.Exists(_fullName))
                Directory.Delete(_fullName, true);
        }

        public static implicit operator string(TempFolder folder) 
            => folder._fullName;

        public string CopyFileFrom(string src)
        {
            var dest = Path.Combine(_fullName, Path.GetFileName(src));
            File.Copy(src, dest);
            return dest;
        }
    }
}