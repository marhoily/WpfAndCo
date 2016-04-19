using System;
using System.IO;

namespace Sample
{
    public static class BinaryReaderExtensions
    {
        public static TEnum ReadEnum<TEnum>(this BinaryReader reader)
            where TEnum : struct
        {
            return (TEnum)Convert.ChangeType(reader.ReadInt32(), typeof(TEnum));
        }
        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return DateTime.FromBinary(reader.ReadInt64());
        }
    }
}