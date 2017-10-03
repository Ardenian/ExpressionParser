using Ardenian.Libraries.ExpressionParser.Data;

namespace Ardenian.Libraries.ExpressionParser.Values
{
    public class NegativeNumber<T> : Value<T>
    {
        #region implemented abstract members of Value

        private Value<T> value;
        private IParserData<T> data;

        public NegativeNumber(Value<T> item, ref IParserData<T> data)
        {
            this.data = data;
            value = item;
        }

        public override T ToValue()
        {
            return data.GetParameterOrParseString("-" + value.ToValue());
        }

        #endregion
    }
}
