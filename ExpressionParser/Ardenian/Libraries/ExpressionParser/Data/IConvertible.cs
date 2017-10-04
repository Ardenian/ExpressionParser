namespace Ardenian.Libraries.CSharp.ExpressionParser.Data
{
    public interface IConvertible<T>
    {
        T Parse(string s);

        bool TryParse(string s, out T result);
    }
}
