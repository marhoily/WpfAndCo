using Microsoft.Data.Entity.Metadata;

namespace Generator
{
    public sealed class Raw
    {
        public void Generate(IModel model, string dir)
        {
            var hierarchy = new HierarchyRoot(dir, "Generated") {
                new Node<Raw>(model) {
                    new Node<ChangeSet> {new Node<Change>()},
                    new Node<TableSet> {new Node<Table>()},
                    new Node<Columns>(),
                    new Node<PrimaryKey>()
                } };

            hierarchy
                .With((IModel m) => m.GetEntityTypes())
                .Build();
        }
    }

    partial class Change : ITransformer
    {
        private readonly IEntityType _type;
        public Change(IEntityType type) { _type = type; }
    }
    partial class ChangeSet : ITransformer
    {
        private readonly IModel _model;
        public ChangeSet(IModel model) { _model = model; }
    }
    partial class Columns : ITransformer
    {
        private readonly IEntityType _type;
        public Columns(IEntityType type) { _type = type; }
    }
    partial class PrimaryKey : ITransformer
    {
        private readonly IEntityType _type;
        public PrimaryKey(IEntityType type) { _type = type; }
    }
    partial class Table : ITransformer
    {
        private readonly IEntityType _type;
        public Table(IEntityType type) { _type = type; }
    }
    partial class TableSet : ITransformer
    {
        private readonly IModel _model;
        public TableSet(IModel model) { _model = model; }
    }
}
