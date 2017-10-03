using System;
using System.Linq;
using System.Collections.Generic;

using Ardenian.Libraries.ExpressionParser.Data;
using Ardenian.Libraries.ExpressionParser.Values;

namespace Ardenian.Libraries.ExpressionParser
{
    public class ExpParser<T>
    {
        public IParserData<T> Data;

        public ExpParser(ref IParserData<T> data) => Data = data ?? throw new ParserException("ExpParser: Constructor: Data in dataless constructor may not be null, as the parser depends on it.");

        public ExpParser() => Data = ParameterFactory.CreateParameter<T>();

        public Expression<T> ParseToExpression(string expression) => new Expression<T>(ParseExpression(expression.Trim()), ref Data);

        public T ParseToValue(string expression) => ParseToExpression(expression).ToValue();

        private int FindPartnerIndex(string s, int index, char partner)
        {
            var count = 1;
            for (int i = index + 1; i < s.Length; ++i)
            {
                if (s[i] == s[index])
                {
                    ++count;
                }
                else if (s[i] == partner)
                {
                    if (count == 1)
                    {
                        return i;
                    }
                    else
                    {
                        --count;
                    }
                }
            }
            return -1;
        }
        private string[] SplitAtOperator(string identifier, string s)
        {
            var tmp = new List<string>();

            var origin = 0;
            var index = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(')
                {
                    i = FindPartnerIndex(s, i, ')') + 1;
                    if (i >= s.Length)
                    {
                        tmp.Add(s.Substring(origin));
                        return tmp.ToArray();
                    }
                    index = 0;
                }
                if (s[i] == identifier[index])
                {
                    ++index;
                    if (index == identifier.Length)
                    {
                        tmp.Add(s.Substring(origin, i - identifier.Length - origin + 1));
                        origin = i + 1;
                        index = 0;
                    }
                }
                else if (index > 0)
                {
                    index = 0;
                }
            }
            tmp.Add(s.Substring(origin));
            return tmp.ToArray();
        }
        private bool ContainsOperator(string identifier, string s)
        {
            var index = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '(')
                {
                    i = FindPartnerIndex(s, i, ')') + 1;
                    if (i >= s.Length || i < 0)
                    {
                        return false;
                    }
                    index = 0;
                }
                if (s[i] == identifier[index])
                {
                    ++index;
                    if (index == identifier.Length)
                    {
                        return true;
                    }
                }
                else if (index > 0)
                {
                    index = 0;
                }
            }
            return false;
        }
        private string RemoveOuterBrackets(string s)
        {
            while (s[0] == '(' && FindPartnerIndex(s, 0, ')') == s.Length - 1)
            {
                s = s.Substring(1, s.Length - 2);
            }
            return s;
        }
        private bool IsNegativeNumber(string[] array, int index)
        {
            var count = 0;
            for (int i = index - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(array[i]))
                {
                    ++count;
                }
                else { break; }
            }
            return count % 2 != 0;
        }

        private Value<T> ParseExpression(string s)
        {
            s = RemoveOuterBrackets(s);

            foreach (var identifier in Data.Operators.Keys)
            {
                if (ContainsOperator(identifier, s))
                {
                    return ParseOperator(s, identifier);
                }
            }
            foreach (var identifier in Data.Functions.Keys)
            {
                if (s.IndexOf(identifier, StringComparison.Ordinal) == 0)
                {
                    return ParseFunction(s, identifier);
                }
            }
            return new Number<T>(s, ref Data);
        }

        private Value<T> ParseOperator(string s, string identifier)
        {
            var array = SplitAtOperator(identifier, s);
            switch (identifier)
            {
                case "-":
                    List<Value<T>> list = new List<Value<T>>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i > 0 && IsNegativeNumber(array, i) && !string.IsNullOrEmpty(array[i]))
                        {
                            list.Add(new NegativeNumber<T>(ParseExpression(array[i]), ref Data));
                        }
                        else if (!string.IsNullOrEmpty(array[i]))
                        {
                            list.Add(ParseExpression(array[i]));
                        }
                    }
                    return new Operator<T>(identifier, ref Data, list.ToArray());
                default:
                    if (array.Any(item => string.IsNullOrEmpty(item)))
                    {
                        throw new ParserException($"ExpParser: Operator '{identifier}' does not support empty sides. Both sides need to have values, as in <value><operator><value>.");
                    }
                    return new Operator<T>(identifier, ref Data, Array.ConvertAll(array, item => ParseExpression(item)));
            }
        }
        private Value<T> ParseFunction(string s, string identifier)
        {
            return new Function<T>(identifier, ref Data, Array.ConvertAll(RemoveOuterBrackets((s.Remove(s.IndexOf(identifier, StringComparison.Ordinal), identifier.Length))).Split(','), part => ParseExpression(part)));
        }
    }
}