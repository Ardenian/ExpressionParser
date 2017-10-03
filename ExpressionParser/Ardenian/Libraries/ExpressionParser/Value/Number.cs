using Ardenian.Libraries.ExpressionParser.Data;

namespace Ardenian.Libraries.ExpressionParser.Values
{
    public class Number<T> : Value<T>
    {
        #region implemented abstract members of Value

        private string value;
        private IParserData<T> data;

        public Number(string item, ref IParserData<T> data)
        {
            this.data = data;
            value = item;
        }

        public override T ToValue()
        {
            return data.GetParameterOrParseString(value);
        }

        #endregion
    }
}
