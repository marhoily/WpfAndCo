using System;
using FluentAssertions;
using Xunit;

namespace Sample
{
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
}