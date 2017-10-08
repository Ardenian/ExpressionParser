using System;
using System.Collections.Generic;
using System.Linq;

namespace Ardenian.Libraries.CSharp.ExpressionParser.Data
{
    public class DefaultParserData_Float : ParserData<float>
    {
        public override IDictionary<string, float> Variables => variables;
        private Dictionary<string, float> variables;

        public override IDictionary<string, Func<float, float, float>> Operators => operators;
        private Dictionary<string, Func<float, float, float>> operators;

        public override IDictionary<string, Func<float[], float>> Functions => functions;
        private Dictionary<string, Func<float[], float>> functions;

        public override float Parse(string s)
        {
            return float.Parse(s);
        }
        public override bool TryParse(string s, out float result)
        {
            return float.TryParse(s, out result);
        }

        public DefaultParserData_Float()
        {
            variables = new Dictionary<string, float>() {
                { "PI", (float)Math.PI },
                { "E", (float)Math.E }
            };
            operators = new Dictionary<string, Func<float, float, float>>()
            {
                {"+", delegate(float a, float b){ return a + b; } },
                {"-", delegate(float a, float b){ return a - b; } },
                {"*", delegate(float a, float b){ return a * b; } },
                {"/", delegate(float a, float b){ return a / b; } },
                {"^", delegate(float a, float b){ return (float)Math.Pow(a,b); } },
                {"%", delegate(float a, float b){ return a % b; } },
            };
            functions = new Dictionary<string, Func<float[], float>>()
            {
                {"Abs", delegate(float[] arguments){return Math.Abs(arguments[0]); } },
                {"Max", delegate(float[] arguments){return arguments.Max(); } },
                {"Min", delegate(float[] arguments){return arguments.Min(); } },
                {"Sign", delegate(float[] arguments){return Math.Sign(arguments[0]); } },
                {"Sum", delegate(float[] arguments){return arguments.Sum(); } },
            };

        }
    }

}
