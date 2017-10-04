using System;
using System.Collections.Generic;

namespace Ardenian.Libraries.ExpressionParser.Data
{
    public interface IParserData<T>
    {
        IDictionary<string, T> Variables { get; }
        IDictionary<string, Func<T, T, T>> Operators { get; }
        IDictionary<string, Func<T[], T>> Functions { get; }

        T GetParameterOrParseString(string s);
    }
}
