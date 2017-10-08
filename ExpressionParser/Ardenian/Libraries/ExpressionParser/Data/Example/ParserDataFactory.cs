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
                    throw new ParserException($"Type \'{typeof(T)}\' is not supported for parameterless parser construction.");
            }
        }
    }
}
