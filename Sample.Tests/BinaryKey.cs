using System;

namespace Sample
{
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