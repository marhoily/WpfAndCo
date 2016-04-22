using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApprovalTests;
using Generator;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace Sample
{
    public class HierarchyFacts
    {
        public class X
        {
            public IEnumerable<Y> Ys => new[] {new Y("1"), new Y("2")};
            public override string ToString() => $"X: [{Ys.Join()}]";
        }
        public class Y {
            public string Name { get; }
            public Y(string name){Name = name;}
            public override string ToString() => $"Y{Name}";
        }
        public class A : ITransformer { }
        public class B : ITransformer { public B(X x) { } }
        public class C : ITransformer { public C(X x) { } }
        public class D : ITransformer { public D(Y y) { } }
        
        [Fact]
        public void Apply()
        {
            var hierarchy = new HierarchyRoot("c:/dir/my.csproj", "Generated") {
                new Node<A>(new X()) {
                    new Node<B> {new Node<D>()},
                    new Node<C>()
                } };

            Approvals.Verify(JsonConvert.SerializeObject(
                hierarchy.With((X m) => m.Ys).Build(),
                new JsonSerializerSettings()
                {
                    Converters =
                    {
                        new TypeConverter(),
                        new NestedConverter(),
                    },
                    Formatting = Formatting.Indented
                }));
        }
    }

    public class NestedConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsNested && objectType.Assembly == Assembly.GetExecutingAssembly();
        }
    }

    public class TypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Type)value).Name);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Type).IsAssignableFrom(objectType);
        }
    }
}