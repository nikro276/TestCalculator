using System;

namespace TestCalculator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int index = 0;
            var expression = MyExpression.Parse("2+2*2", ref index);
            Assert.Equal(6, expression.Calculate());
        }

        [Fact]
        public void Test2()
        {
            int index = 0;
            var expression = MyExpression.Parse("(2+2)*2", ref index);
            Assert.Equal(8, expression.Calculate());
        }

        [Fact]
        public void Test3()
        {
            int index = 0;
            var expression = MyExpression.Parse("(((2.22+1)*(2)))", ref index);
            Assert.Equal(6.44m, expression.Calculate());
        }

        [Fact]
        public void Test4()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("((2+2*2)", ref index));
        }

        [Fact]
        public void Test5()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("2++2*2", ref index));
        }
    }
}