using Ardenian.Libraries.ExpressionParser.Data;

namespace Ardenian.Libraries.ExpressionParser.Values
{
    public class Operator<T> : Value<T>
    {
        private string identifier;
        private IParserData<T> data;
        private Value<T>[] values;

        public Operator(string identifier, ref IParserData<T> data, Value<T>[] values)
        {
            this.identifier = identifier;
            this.data = data;
            this.values = values;
        }

        #region implemented abstract members of Value

        public override T ToValue()
        {
            var result = values[0].ToValue();
            for (int i = 1; i < values.Length; ++i)
            {
                result = data.Operators[identifier](result, values[i].ToValue());
            }
            return result;
        }
        #endregion
    }
}
