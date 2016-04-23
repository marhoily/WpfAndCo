namespace Generator
{
    public interface ILocated
    {
        int Level { get; }
        string ProjectDir { get; }
        string DependentUpon { get; }
    }
}