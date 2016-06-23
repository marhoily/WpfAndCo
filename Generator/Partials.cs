﻿using Generaid;

namespace Generator
{
    partial class Aggregate : ITransformer
    {
        public Aggregate(MetaType type) { _type = type; }
        public string Name => $"{_type.Name}.cs";
    }
    partial class CreateCommit : ITransformer
    {
        public CreateCommit(MetaType type) { _type = type; }
        public string Name => $"Create{_type.Name}.cs";
    }
    partial class CreateHandler : ITransformer
    {
        public CreateHandler(MetaType type) { _type = type; }
        public string Name => $"Create{_type.Name}Handler.cs";
    }
    partial class CreateValidator : ITransformer
    {
        public CreateValidator(MetaType type) { _type = type; }
        public string Name => $"Create{_type.Name}Validator.cs";
    }
    partial class DeleteCommit : ITransformer
    {
        public DeleteCommit(MetaType type) { _type = type; }
        public string Name => $"Delete{_type.Name}.cs";
    }
    partial class DeleteHandler : ITransformer
    {
        public DeleteHandler(MetaType type) { _type = type; }
        public string Name => $"Delete{_type.Name}Handler.cs";
    }
    partial class DeleteValidator : ITransformer
    {
        public DeleteValidator(MetaType type) { _type = type; }
        public string Name => $"Delete{_type.Name}Validator.cs";
    }
    partial class UpdateCommit : ITransformer
    {
        public UpdateCommit(MetaType type) { _type = type; }
        public string Name => $"Update{_type.Name}.cs";
    }
    partial class UpdateHandler : ITransformer
    {
        public UpdateHandler(MetaType type) { _type = type; }
        public string Name => $"Update{_type.Name}Handler.cs";
    }
    partial class UpdateValidator : ITransformer
    {
        public UpdateValidator(MetaType type) { _type = type; }
        public string Name => $"Update{_type.Name}Validator.cs";
    }
}