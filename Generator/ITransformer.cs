namespace Generaid
{
    public interface ITransformer
    {
        string Name { get; }
        string TransformText();
    }
}