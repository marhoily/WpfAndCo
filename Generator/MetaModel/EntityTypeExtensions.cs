using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    public static class CodePrettyPrintExtensions
    {
        public static string NiceName(this Type type)
        {
            return type.NiceName((t, s) => s);
        }
        public static string NiceName(this Type type, Func<Type, string, string> selectName)
        {
            while (true)
            {
                string niceName;
                if (NiceNames.TryGetValue(type, out niceName)) return niceName;
                if (type.IsArray) return NiceName(type.GetElementType(), selectName) + "[]";
                if (!type.IsGenericType)
                {
                    if (!type.IsByRef) return selectName(type, type.Name);
                    type = type.GetElementType();
                    continue;
                }
                var args = type.GetGenericArguments();
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    return NiceName(args[0], selectName) + "?";
                var genericArgs = string.Join(", ",
                    args.Select(a => a.NiceName(selectName)));
                return selectName(type, type.Name.Before("`")) + "<" + genericArgs + ">";
            }
        }

        public static bool DeepAny(this Type type, Func<Type, bool> select)
        {
            while (true)
            {
                if (type.IsArray)
                {
                    type = type.GetElementType();
                    continue;
                }
                if (type.IsGenericType)
                    return select(type)
                        || type.GetGenericArguments().Any(a => a.DeepAny(@select));
                if (!type.IsByRef) return select(type);
                type = type.GetElementType();
            }
        }


        private static readonly Dictionary<Type, string> NiceNames = new Dictionary<Type, string>
        {
            {typeof (byte), "byte"},
            {typeof (sbyte), "sbyte"},
            {typeof (short), "short"},
            {typeof (ushort), "ushort"},
            {typeof (int), "int"},
            {typeof (uint), "uint"},
            {typeof (long), "long"},
            {typeof (ulong), "ulong"},
            {typeof (void), "void"},
            {typeof (string), "string"},
            {typeof (bool), "bool"},
            {typeof (object), "object"},
        };
    }

    public static class EntityTypeExtensions
    {
        public static string GetPropertiesArgumentsList(this MetaType meta)
            => meta.GetProperties()
                .Select(p => $"{p.Type} {p.Name}")
                .Join();

        public static string GetPropertiesParametersList(this MetaType meta)
            => meta.GetProperties()
                .Select(p => p.Name)
                .Join();
    }
}