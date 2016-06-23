using System;

namespace Generator
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OnDeleteAttribute : Attribute
    {
        public DeleteReaction Reaction { get; }

        public OnDeleteAttribute(DeleteReaction reaction)
        {
            Reaction = reaction;
        }
    }
}