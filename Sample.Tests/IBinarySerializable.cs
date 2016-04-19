using System.IO;

namespace Sample
{
    public interface IBinarySerializable
    {
        void WriteAllProperties(BinaryWriter writer);
        void WriteChangedProperties(BinaryWriter writer);
        void WriteKey(BinaryWriter writer);
    }
}