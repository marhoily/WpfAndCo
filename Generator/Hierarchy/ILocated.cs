namespace Generator
{
    public interface ILocated
    {
        string ProjectDir { get; }
        int Level { get; }
    }
}