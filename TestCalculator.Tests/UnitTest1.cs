using System;

namespace TestCalculator.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var calculator = new MyCalculator();
            Assert.Equal(6, calculator.Calculate("2+2*2"));
        }

        [Fact]
        public void Test2()
        {
            var calculator = new MyCalculator();
            Assert.Equal(8, calculator.Calculate("(2+2)*2"));
        }

        [Fact]
        public void Test3()
        {
            var calculator = new MyCalculator();
            Assert.Equal(6.44m, calculator.Calculate("(((2.22+1)*(2)))"));
        }

        [Fact]
        public void Test4()
        {
            var calculator = new MyCalculator();
            Assert.Equal(4, calculator.Calculate("2+(+2)"));
        }

        [Fact]
        public void Test5()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("2++2", ref index));
        }

        [Fact]
        public void Test6()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("((2+2*2)", ref index));
        }

        [Fact]
        public void Test7()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("2++2*2", ref index));
        }

        [Fact]
        public void Test8()
        {
            int index = 0;
            Assert.Throws<MyParseExpressionException>(() => MyExpression.Parse("2s2*2", ref index));
        }
    }
}