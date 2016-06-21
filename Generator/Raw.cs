using Generaid;

namespace Generator
{
    public sealed class Raw : ITransformer
    {
        public string Name => "Raw.cs";
        public string TransformText() => "";
    }

    partial class Change : ITransformer
    {
        public Change(EntityType type) { _type = type; }
        public string Name => $"{_type.Name}.change.cs";
    }
    partial class ChangeSet : ITransformer
    {
        public ChangeSet(MetaModel model) { _model = model; }
        public string Name => "changeSet.cs";
    }
    partial class Columns : ITransformer
    {
        public Columns(EntityType type) { _type = type; }
        public string Name => $"{_type.Name}.columns.cs";
    }
    partial class Table : ITransformer
    {
        public Table(EntityType type) { _type = type; }
        public string Name => $"{_type.Name}.table.cs";
    }
    partial class TableSet : ITransformer
    {
        public TableSet(MetaModel model) { _model = model; }
        public string Name => "tableSet.cs";
    }
}
