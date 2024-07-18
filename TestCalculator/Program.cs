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
                var calculator = new MyCalculator();
                var result = calculator.Calculate(input);
                Console.WriteLine(result);
            }
            catch (MyParseExpressionException e)
            {
                Console.WriteLine("MyParseExpressionException: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}