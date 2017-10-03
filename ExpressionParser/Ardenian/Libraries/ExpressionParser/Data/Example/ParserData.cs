using System;
using System.Collections.Generic;

namespace Ardenian.Libraries.ExpressionParser.Data
{
    public abstract class Parameter<T> : IParserData<T>, IConvertible<T>
    {
        public abstract IDictionary<string, T> Parameters { get; }
        public abstract IDictionary<string, Func<T, T, T>> Operators { get; }
        public abstract IDictionary<string, Func<T[], T>> Functions { get; }

        public T GetParameterOrParseString(string s)
        {
            if (Parameters != null && Parameters.Count > 0 && Parameters.ContainsKey(s))
            {
                return Parameters[s];
            }
            return Parse(s);
        }

        public abstract T Parse(string s);
        public abstract bool TryParse(string s, out T result);
    }
}