using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculator
{
    public class MyOperation
    {
        public MyOperationType OperationType { get; set; }
        public int Priority { get; set; }

        public decimal Execute(decimal leftOperand, decimal rightOperand)
        {
            switch (OperationType)
            {
                case MyOperationType.Addition:
                    return leftOperand + rightOperand;
                case MyOperationType.Substraction:
                    return leftOperand - rightOperand;
                case MyOperationType.Multiplication:
                    return leftOperand * rightOperand;
                case MyOperationType.Division:
                    return leftOperand / rightOperand;
                default:
                    throw new MyParseExpressionException("Incorrect operation type");
            }
        }

        public static MyOperation GetOperation(char operationChar)
        {
            switch (operationChar)
            {
                case '+':
                    return new MyOperation()
                    {
                        OperationType = MyOperationType.Addition,
                        Priority = 2
                    };
                case '-':
                    return new MyOperation()
                    {
                        OperationType = MyOperationType.Substraction,
                        Priority = 2
                    };
                case '*':
                    return new MyOperation()
                    {
                        OperationType = MyOperationType.Multiplication,
                        Priority = 1
                    };
                case '/':
                    return new MyOperation()
                    {
                        OperationType = MyOperationType.Division,
                        Priority = 1
                    };
                default:
                    return null;
            }
        }
    }

    public enum MyOperationType
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }
}