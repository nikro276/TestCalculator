using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculator
{
    public abstract class MyOperation
    {
        public abstract int Priority { get; }

        public static Dictionary<char, Type> myOperationTypes;
        public static Dictionary<char, Type> MyOperationTypes
        {
            get
            {
                if (myOperationTypes == null)
                {
                    myOperationTypes = new Dictionary<char, Type>();
                    var myOperationType = typeof(MyOperation);
                    var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(type => myOperationType.IsAssignableFrom(type));
                    object[]? attributes;
                    MyOperationTypeAttribute attribute;
                    foreach (var type in types)
                    {
                        attributes = type.GetCustomAttributes(typeof(MyOperationTypeAttribute), true);
                        if (attributes.Length > 0)
                        {
                            attribute = (MyOperationTypeAttribute)attributes[0];
                            myOperationTypes.Add(attribute.Symbol, type);
                        }
                    }
                }
                return myOperationTypes;
            }
        }

        public static MyOperation GetOperation(char operationChar)
        {
            if (!MyOperationTypes.ContainsKey(operationChar))
                return null;
            var type = MyOperationTypes[operationChar];
            return (MyOperation)Activator.CreateInstance(type);
        }

        public abstract decimal Execute(decimal leftOperand, decimal rightOperand);
    }

    public class MyOperationTypeAttribute : Attribute
    {
        public char Symbol { get; }
        public MyOperationTypeAttribute() { }
        public MyOperationTypeAttribute(char symbol) => Symbol = symbol;
    }

    [MyOperationType('+')]
    public class Addition : MyOperation
    {
        public override int Priority { get; } = 2;

        public override decimal Execute(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand + rightOperand;
        }
    }

    [MyOperationType('-')]
    public class Substraction : MyOperation
    {
        public override int Priority { get; } = 2;

        public override decimal Execute(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand - rightOperand;
        }
    }

    [MyOperationType('*')]
    public class Multiplication : MyOperation
    {
        public override int Priority { get; } = 1;

        public override decimal Execute(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand * rightOperand;
        }
    }

    [MyOperationType('/')]
    public class Division : MyOperation
    {
        public override int Priority { get; } = 1;

        public override decimal Execute(decimal leftOperand, decimal rightOperand)
        {
            return leftOperand / rightOperand;
        }
    }
}