namespace Generator
{
    public interface INodeOwner
    {
        int Level { get; }
        string ProjectDir { get; }
    }
}