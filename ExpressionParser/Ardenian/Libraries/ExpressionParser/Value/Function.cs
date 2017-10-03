using System;
using Ardenian.Libraries.ExpressionParser.Data;

namespace Ardenian.Libraries.ExpressionParser.Values
{
    public class Function<T> : Value<T>
    {
        private string identifier;
        private IParserData<T> data;
        private Value<T>[] values;

        public Function(string identifier, ref IParserData<T> data, Value<T>[] values)
        {
            this.identifier = identifier;
            this.data = data;
            this.values = values;
        }

        #region implemented abstract members of Value

        public override T ToValue()
        {
            return data.Functions[identifier](Array.ConvertAll(values, value => value.ToValue()));
        }

        #endregion


    }
}
