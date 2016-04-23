using System;

namespace Generator
{
    public static class TypeExtensions
    {
        public static OneArgCtor Ctor(this Type tp)
        {
            return OneArgCtor.From(tp);
        }
    }
}