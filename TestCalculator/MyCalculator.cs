using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestCalculator
{
    public class MyCalculator
    {
        public decimal Calculate(string input)
        {
            input = Regex.Replace(input, @"\s+", "");
            int index = 0;
            var expression = MyExpression.Parse(input, ref index);
            var result = Calculate(expression);
            return result;
        }

        public decimal Calculate(MyExpression expression)
        {
            if (expression.Terms.Count == 1)
                return expression.Terms[0] is MyExpression ? Calculate((MyExpression)expression.Terms[0]) : (decimal)expression.Terms[0];
            var operations = new List<MyOperation>(expression.Operations);
            var terms = new List<object>(expression.Terms);
            var minPriority = operations.Min(x => x.Priority);
            var maxPriority = operations.Max(x => x.Priority);
            var operationsCount = operations.Count;
            MyOperation operation;
            decimal leftOperand, rightOperand, result;
            object leftTerm, rightTerm;
            for (var i = minPriority; i < maxPriority + 1; i++)
            {
                for (var j = 0; j < operationsCount; j++)
                {
                    operation = operations[j];
                    if (operation.Priority == i)
                    {
                        leftTerm = terms[j];
                        rightTerm = terms[j + 1];
                        leftOperand = leftTerm is MyExpression ? Calculate((MyExpression)leftTerm) : (decimal)leftTerm;
                        rightOperand = rightTerm is MyExpression ? Calculate((MyExpression)rightTerm) : (decimal)rightTerm;
                        result = operation.Execute(leftOperand, rightOperand);
                        terms[j] = result;
                        terms.RemoveAt(j + 1);
                        operations.RemoveAt(j);
                        operationsCount--;
                        j--;
                    }
                }
            }
            return (decimal)terms[0];
        }
    }
}