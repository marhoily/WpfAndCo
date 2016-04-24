using Generaid;
using Microsoft.Data.Entity.Metadata;

namespace Generator
{
    public sealed class Raw : ITransformer
    {
        public string Name => "Raw.cs";
        public string TransformText() => "";
    }

    partial class Change : ITransformer
    {
        private readonly IEntityType _type;
        public Change(IEntityType type) { _type = type; }
        public string Name => $"{_type.Name}.change.cs";
    }
    partial class ChangeSet : ITransformer
    {
        private readonly IModel _model;
        public ChangeSet(IModel model) { _model = model; }
        public string Name => "changeSet.cs";
    }
    partial class Columns : ITransformer
    {
        private readonly IEntityType _type;
        public Columns(IEntityType type) { _type = type; }
        public string Name => $"{_type.Name}.columns.cs";
    }
    partial class PrimaryKey : ITransformer
    {
        private readonly IEntityType _type;
        public PrimaryKey(IEntityType type) { _type = type; }
        public string Name => $"{_type.Name}.primaryKey.cs";
    }
    partial class Table : ITransformer
    {
        private readonly IEntityType _type;
        public Table(IEntityType type) { _type = type; }
        public string Name => $"{_type.Name}.table.cs";
    }
    partial class TableSet : ITransformer
    {
        private readonly IModel _model;
        public TableSet(IModel model) { _model = model; }
        public string Name => "tableSet.cs";
    }
}
