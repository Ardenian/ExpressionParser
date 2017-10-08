using System;
using System.Collections.Generic;

namespace Ardenian.Libraries.CSharp.ExpressionParser.Data
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
            if (!TryParse(s, out T result))
            {
                throw new ParserException($"Couldn't parse \"{s}\". Did you forget a bracket or are you missing a ParserData entry, like variables or functions?");
            }
            return result;
        }

        public abstract T Parse(string s);
        public abstract bool TryParse(string s, out T result);
    }
}