using System.IO;

namespace Sample
{
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
}