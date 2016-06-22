using Generaid;

namespace Generator
{
    public sealed class Crud : ITransformer
    {
        public string Name => "Crud.cs";
        public string TransformText() => "";
    }

    public sealed class Entity : ITransformer
    {
        private readonly EntityType _type;
        public string TransformText() => "";
        public Entity(EntityType type) { _type = type; }
        public string Name => $"{_type.Name}.cs";
    }

    partial class CreateAggregate : ITransformer
    {
        public CreateAggregate(EntityType type) { _type = type; }
        public string Name => $"Create{_type.Name}Aggregate.cs";
    }
    partial class CreateCommit : ITransformer
    {
        public CreateCommit(EntityType type) { _type = type; }
        public string Name => $"Create{_type.Name}.cs";
    }
    partial class CreateHandler : ITransformer
    {
        public CreateHandler(EntityType type) { _type = type; }
        public string Name => $"Create{_type.Name}Handler.cs";
    }
    partial class CreateValidator : ITransformer
    {
        public CreateValidator(EntityType type) { _type = type; }
        public string Name => $"Create{_type.Name}Validator.cs";
    }

}
