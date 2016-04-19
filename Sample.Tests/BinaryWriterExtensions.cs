using System;
using System.IO;

namespace Sample
{
    public static class BinaryWriterExtensions
    {
        public static void WriteEnum<TEnum>(this BinaryWriter writer, TEnum value)
        {
            writer.Write(Convert.ToInt32(value));
        }
        public static void Write(this BinaryWriter writer, DateTime value)
        {
            writer.Write(value.Ticks);
        }
    }
}