using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public sealed class RegModel
    {
        public object Model1 { get; }
        public ITransformer Transformer { get; }

        private RegModel(ITransformer transformer, object model)
        {
            Transformer = transformer;
            Model1 = model;
        }

        public static IEnumerable<RegModel> Choose(OneArgCtor ctor,
            object model, IEnumerable<RegRoot> registrations)
        {
            var better = model;
            if (ctor.NoArgs) {
                yield return new RegModel(ctor.Invoke(null), model);
            }
            else if (ctor.ArgType.IsInstanceOfType(better)) {
                yield return new RegModel(ctor.Invoke(better), model);
            }
            else
            {
                var reg = registrations.Single(r => r.Value == ctor.ArgType);
                var args = reg.Convert(better);
                var many = args.Select(m => new RegModel(ctor.Invoke(m), m));
                foreach (var o in many) yield return o;
            }
        }
    }
}
