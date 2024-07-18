using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCalculator
{
    public class MyExpression
    {
        public List<object> Terms { get; set; } = new List<object>();
        public List<MyOperation> Operations { get; set; } = new List<MyOperation>();

        public static MyExpression Parse(string input, ref int index, bool isBracketExpression = false)
        {
            var expression = new MyExpression();
            var length = input.Length;
            char symbol;
            while (index < length)
            {
                symbol = input[index];
                var lastObjectWasOperation = expression.Terms.Count == expression.Operations.Count;
                if (symbol == '(')
                {
                    if (lastObjectWasOperation)
                    {
                        index++;
                        var newExpression = Parse(input, ref index, true);
                        expression.Terms.Add(newExpression);
                    }
                    else
                        throw new MyParseExpressionException("Incorrect expression at " + index);
                }
                else if (symbol == ')')
                {
                    index++;
                    if (isBracketExpression == false)
                        throw new MyParseExpressionException("Expression has no matching open bracket at " + index);
                    if (expression.Terms.Count == 0)
                        throw new MyParseExpressionException("Expression is empty at " + index);
                    return expression;
                }
                else if (lastObjectWasOperation)
                {
                    var isStart = expression.Terms.Count == 0;
                    var decimalLength = expression.GetDecimalValue(input, index, out decimal decimalValue, isStart);
                    if (decimalLength > 0)
                    {
                        expression.Terms.Add(decimalValue);
                        index += decimalLength;
                    }
                    else
                        throw new MyParseExpressionException("Incorrect expression at " + index);
                }
                else
                {
                    var operation = MyOperation.GetOperation(symbol);
                    if (operation != null)
                    {
                        expression.Operations.Add(operation);
                        index++;
                    }
                    else
                        throw new MyParseExpressionException("Cannot parse operation at " + index);
                }
            }
            if (index == length && isBracketExpression)
                throw new MyParseExpressionException("Expression has no matching closed bracket");
            return expression;
        }

        private int GetDecimalValue(string input, int index, out decimal decimalValue, bool isStart)
        {
            string pattern;
            if (isStart)
                pattern = @"^(\+|-)?[0-9\.]+";
            else
                pattern = @"^[0-9\.]+";
            var match = Regex.Match(input.Substring(index), pattern);
            if (match.Success && decimal.TryParse(match.Value, NumberStyles.Number , CultureInfo.GetCultureInfo("en-US"), out decimalValue))
            {
                return match.Value.Length;
            }
            else
            {
                decimalValue = 0;
                return 0;
            }
        }
    }

    public class MyParseExpressionException : Exception
    {
        public MyParseExpressionException(string message) : base(message) { }
    }
}