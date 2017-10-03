using System;
using Ardenian.Libraries.ExpressionParser.Data;
using Ardenian.Libraries.ExpressionParser.Values;

namespace Ardenian.Libraries.ExpressionParser
{
    public class Expression<T>
    {
        private Value<T> value;

        public IParserData<T> Data;

        public Expression(Value<T> value, ref IParserData<T> data)
        {
            this.value = value;
            Data = data;
        }

        public T ToValue()
        {
            return value.ToValue();
        }

        public sealed override string ToString()
        {
            return Convert.ToString(ToValue());
        }
    }
}
