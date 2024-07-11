using System.Text.RegularExpressions;

namespace TestCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var input = Console.ReadLine();
                Regex.Replace(input, @"\s+", "");
                int index = 0;
                var expression = MyExpression.Parse(input, ref index);
                var result = expression.Calculate();
                Console.WriteLine(result);
            }
            catch (MyParseExpressionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}