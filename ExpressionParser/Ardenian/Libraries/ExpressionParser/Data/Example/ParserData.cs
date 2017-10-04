using System;
using System.Collections.Generic;

namespace Ardenian.Libraries.ExpressionParser.Data
{
    public abstract class ParserData<T> : IParserData<T>, IConvertible<T>
    {
        public abstract IDictionary<string, T> Variables { get; }
        public abstract IDictionary<string, Func<T, T, T>> Operators { get; }
        public abstract IDictionary<string, Func<T[], T>> Functions { get; }

        public T GetParameterOrParseString(string s)
        {
            if (Variables != null && Variables.Count > 0 && Variables.ContainsKey(s))
            {
                return Variables[s];
            }
            return Parse(s);
        }

        public abstract T Parse(string s);
        public abstract bool TryParse(string s, out T result);
    }
}