using System;
using System.Collections.Generic;
using System.Linq;

namespace Ardenian.Libraries.CSharp.ExpressionParser.Data
{
    public class DefaultParserData_Double : ParserData<double>
    {
        public override IDictionary<string, double> Variables => variables;
        private Dictionary<string, double> variables;

        public override IDictionary<string, Func<double, double, double>> Operators => operators;
        private Dictionary<string, Func<double, double, double>> operators;

        public override IDictionary<string, Func<double[], double>> Functions => functions;
        private Dictionary<string, Func<double[], double>> functions;

        public override double Parse(string s)
        {
            return double.Parse(s);
        }
        public override bool TryParse(string s, out double result)
        {
            return double.TryParse(s, out result);
        }

        public DefaultParserData_Double()
        {
            variables = new Dictionary<string, double>() {
                { "PI", Math.PI },
                { "E", Math.E }
            };

            operators = new Dictionary<string, Func<double, double, double>>()
            {
                {"+", delegate(double a, double b){ return a + b; } },
                {"-", delegate(double a, double b){ return a - b; } },
                {"*", delegate(double a, double b){ return a * b; } },
                {"/", delegate(double a, double b){ return a / b; } },
                {"^", delegate(double a, double b){ return Math.Pow(a,b); } },
                {"%", delegate(double a, double b){ return a % b; } },
            };
            functions = new Dictionary<string, Func<double[], double>>()
            {
                {"Abs", delegate(double[] arguments){return Math.Abs(arguments[0]); } },
                {"Cos", delegate(double[] arguments){return Math.Cos(arguments[0]); } },
                {"Max", delegate(double[] arguments){return arguments.Max(); } },
                {"Min", delegate(double[] arguments){return arguments.Min(); } },
                {"Sign", delegate(double[] arguments){return Math.Sign(arguments[0]); } },
                {"Sin", delegate(double[] arguments){return Math.Sin(arguments[0]); } },
                {"Sum", delegate(double[] arguments){return arguments.Sum(); } },
            };

        }
    }

}
