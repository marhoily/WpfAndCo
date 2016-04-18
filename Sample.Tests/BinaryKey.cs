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

    public class BinaryKeyBuilder
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _writer;

        public BinaryKeyBuilder() {
            _memoryStream = new MemoryStream();
            _writer = new BinaryWriter(_memoryStream);
        }

        public void Add(int value)  { _writer.Write(value); }
        public void Add(long value)  { _writer.Write(value); }

        public BinaryKey Build()
        {
            _writer.Flush();
            return new BinaryKey(_memoryStream.ToArray());
        }
    }
    public sealed class BinaryKey
    {
        private readonly byte[] _bytes;
        private readonly int _hashCode;

        public BinaryKey(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentOutOfRangeException("bytes");

            var end = bytes.Length / 4;
            for (var i = 0; i < end; i++)
                _hashCode = (_hashCode * 397) ^
                            BitConverter.ToInt32(bytes, i);
            _bytes = bytes;

        }

        private bool Equals(BinaryKey other)
        {
            if (_bytes.Length != other._bytes.Length) return false;
            // ReSharper disable once LoopCanBeConvertedToQuery for performance
            for (var i = 0; i < _bytes.Length; i++)
                if (_bytes[i] != other._bytes[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is BinaryKey && Equals((BinaryKey)obj);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public static bool operator ==(BinaryKey left, BinaryKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BinaryKey left, BinaryKey right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Convert.ToBase64String(_bytes);
        }
    }
}