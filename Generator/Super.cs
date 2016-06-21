using Generaid;

namespace Generator
{
    public sealed class Super : ITransformer
    {
        public string Name => "Super.cs";
        public string TransformText() => "";
    }

    partial class DataContext : ITransformer
    {
        private readonly MetaModel _model;
        public DataContext(MetaModel model) { _model = model; }
        public string Name => "DataContext.cs";
    }
}
