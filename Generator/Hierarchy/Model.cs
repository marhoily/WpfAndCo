using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public abstract class Model
    {
        public sealed class One : Model
        {
            public object Model { get; }
            public ITransformer Transformer { get; }
            public One(ITransformer transformer, object model)
            {
                Transformer = transformer;
                Model = model;
            }
        }
        public sealed class Many : Model
        {
            public IEnumerable<One> Ones { get; }
            public Many(IEnumerable<One> ones) { Ones = ones; }
        }

        public static Model Choose(OneArgCtor ctor, 
            object model, IEnumerable<RegRoot> registrations)
        {
            var better = model;
            if (ctor.NoArgs) return new One(ctor.Invoke(null), model);
            if (ctor.ArgType.IsInstanceOfType(better))
                return new One(ctor.Invoke(better), model);
            var reg = registrations.Single(r => r.Value == ctor.ArgType);
            var args = reg.Convert(better);
            return new Many(args.Select(m => new One(ctor.Invoke(m), m)));
        }
    }
}