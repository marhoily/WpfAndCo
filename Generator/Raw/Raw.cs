using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Metadata;

namespace Generator.Raw
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

    public class HierarchyRoot : IEnumerable
    {
        public HierarchyRoot(string dir, string generated)
        {
        }
        public void Add<T>(Node<T> item) { }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public HierarchyRoot With<TK, TV>(Func<TK, IEnumerable<TV>> func)
        {
            return this;
        }

        public void Build()
        {
            
        }
    }

    public class Node<T> : IEnumerable
    {
        public Node(object model)
        {
        }

        public Node()
        {
        }

        public void Add<U>(Node<U> item) { }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }


    public interface ITransformer { }
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
