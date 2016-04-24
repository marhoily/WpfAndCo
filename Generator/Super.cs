using Generaid;
using Microsoft.Data.Entity.Metadata;

namespace Generator
{
    public sealed class Super : ITransformer
    {
        public string Name => "Super.cs";
        public string TransformText() => "";
    }

    partial class DataContext : ITransformer
    {
        private readonly IModel _model;
        public DataContext(IModel model) { _model = model; }
        public string Name => "DataContext.cs";
    }
}
