using System;
using System.Collections.Generic;

namespace Generator
{
    public sealed class RegRoot
    {
        private readonly Func<object, IEnumerable<object>> _convert;
        public Type Key { get; set; }
        public Type Value { get; set; }

        public RegRoot(Type key, Type value, 
            Func<object, IEnumerable<object>> convert)
        {
            _convert = convert;
            Key = key;
            Value = value;
        }

        public IEnumerable<object> Convert(object obj)
        {
            return _convert(obj);
        }
    }
}