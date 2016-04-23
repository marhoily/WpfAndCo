namespace Generator
{
    public interface ITransformer
    {
        string Name { get; }
        string TransformText();
    }
}