using System;

namespace Generator
{
    internal static class TypeExtensions
    {
        public static OneArgCtor Ctor(this Type tp) => OneArgCtor.From(tp);
    }
}