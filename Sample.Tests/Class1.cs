using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using FluentAssertions;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Conventions.Internal;
using Xunit;

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

    public class BinaryKeyTests
    {
        [Fact]
        public void WrongSize()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new BinaryKey(new byte[] {1, 2, 3}));
        }
        [Fact]
        public void CompareEqual()
        {
            var k1 = new BinaryKey(new byte[] { 1, 2, 3, 4 });
            var k2 = new BinaryKey(new byte[] { 1, 2, 3, 4 });
            k1.Should().Be(k2);
        }
        [Fact]
        public void CompareDiffSize()
        {
            var k1 = new BinaryKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var k2 = new BinaryKey(new byte[] { 1, 2, 3, 4 });
            k1.Should().NotBe(k2);
        }
        [Fact]
        public void CompareDiffElement()
        {
            var k1 = new BinaryKey(new byte[] { 1, 3, 2, 4 });
            var k2 = new BinaryKey(new byte[] { 1, 2, 3, 4 });
            k1.Should().NotBe(k2);
        }
        [Fact]
        public void CompareZeroSize()
        {
            var k1 = new BinaryKey(new byte[0]);
            var k2 = new BinaryKey(new byte[0]);
            k1.Should().Be(k2);
        }
        [Fact]
        public void HasnDependsOnAllBytes()
        {
            var k1 = new BinaryKey(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var k2 = new BinaryKey(new byte[] { 1, 2, 3, 4 });
            k1.GetHashCode().Should().Be(1034819727);
            k2.GetHashCode().Should().Be(67305985);
        }
        [Fact]
        public void HashOverflow()
        {
            new BinaryKey(new byte[] { 255, 2, 3, 4, 255, 6, 7, 8 })
                .GetHashCode().Should().Be(-944789903);
        }
    }

    public class Class1
    {
        [Fact]
        public void FactMethodName()
        {
            var builder = new ModelBuilder(
                new CoreConventionSetBuilder().CreateConventionSet());
            builder.Entity<Person>();
            builder.Entity<City>();
            IModel ro = builder.Model;
            var entityType = ro.GetEntityTypes().First();
            //entityType.GetProperties()
        }
    }
}
