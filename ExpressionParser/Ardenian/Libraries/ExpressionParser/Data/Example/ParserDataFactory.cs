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
                case "System.Single":
                    return new DefaultParserData_Float() as IParserData<T>;
                case "System.Double":
                    return new DefaultParserData_Double() as IParserData<T>;

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
                {"Max", delegate(double[] arguments){return arguments.Max(); } },
                {"Min", delegate(double[] arguments){return arguments.Min(); } },
                {"Sign", delegate(double[] arguments){return Math.Sign(arguments[0]); } },
                {"Sum", delegate(double[] arguments){return arguments.Sum(); } },
            };

        }
    }

}
