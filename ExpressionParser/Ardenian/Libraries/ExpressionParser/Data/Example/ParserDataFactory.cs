using System;
using System.Collections.Generic;
using System.Linq;

namespace Ardenian.Libraries.CSharp.ExpressionParser.Data
{
    public static class ParserDataFactory
    {
        public static IParserData<T> CreateData<T>()
        {
            return Create<T>();
        }
        private static IParserData<T> Create<T>()
        {
            switch (typeof(T).ToString())
            {
                case "System.Int32":
                    return new DefaultParserData_Int32() as IParserData<T>;
                default:
                    throw new NotSupportedException($"ExpParser: Constructor: Type \'{typeof(T)}\' is not supported for dataless parser construction.");
            }
        }
    }

    public class DefaultParserData_Int32 : ParserData<int>
    {
        public override IDictionary<string, int> Variables => variables;
        private Dictionary<string, int> variables;

        public override IDictionary<string, Func<int, int, int>> Operators => operators;
        private Dictionary<string, Func<int, int, int>> operators;

        public override IDictionary<string, Func<int[], int>> Functions => functions;
        private Dictionary<string, Func<int[], int>> functions;

        public override int Parse(string s)
        {
            return int.Parse(s);
        }
        public override bool TryParse(string s, out int result)
        {
            return int.TryParse(s, out result);
        }

        public DefaultParserData_Int32()
        {
            variables = new Dictionary<string, int>();

            operators = new Dictionary<string, Func<int, int, int>>()
            {
                {"+", delegate(int a, int b){ return a + b; } },
                {"-", delegate(int a, int b){ return a - b; } },
                {"*", delegate(int a, int b){ return a * b; } },
                {"/", delegate(int a, int b){ return a / b; } },
                {"^", delegate(int a, int b){ return a ^ b; } },
                {"%", delegate(int a, int b){ return a % b; } },
            };
            functions = new Dictionary<string, Func<int[], int>>()
            {
                {"Abs", delegate(int[] arguments){return Math.Abs(arguments[0]); } },
                {"Max", delegate(int[] arguments){return arguments.Max(); } },
                {"Min", delegate(int[] arguments){return arguments.Min(); } },
                {"Sign", delegate(int[] arguments){return Math.Sign(arguments[0]); } },
                {"Sum", delegate(int[] arguments){return arguments.Sum(); } },
            };

        }
    }
}
