namespace Generator
{
    public interface ILocated
    {
        int Level { get; }
        ITransformer Transformer { get; }
        string ProjectDir { get;}
    }
}