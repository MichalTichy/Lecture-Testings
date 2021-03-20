using System;
using TestedApplication.Database;
using TestedApplication.SimpleToTest;
using Xunit;

namespace xUnitTests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(1, int.MaxValue - 1)]
        [InlineData(-1, int.MinValue + 1)]
        public void Add_Produces_Correct_Result(int number1, int number2)
        {
            int expectedResult = number1 + number2;

            var actualResult = Calculator.Calculate(Operations.Add, number1, number2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(1, int.MaxValue)]
        [InlineData(-1, int.MinValue)]
        public void Subtract_Produces_Correct_Result(int number1, int number2)
        {
            int expectedResult = number1 - number2;

            var actualResult = Calculator.Calculate(Operations.Sub, number1, number2);

            Assert.Equal(expectedResult, actualResult);
        }
        
        [Fact]
        public void Add_Throws_OverflowException_For_Big_Numbers()
        {
            Assert.Throws<OverflowException>(() => Calculator.Calculate(Operations.Add, int.MaxValue, 1));
        }

        [Fact]
        public void Subtract_Throws_OverflowException_for_MinValue_Subtraction()
        {
            Assert.Throws<OverflowException>(() => Calculator.Calculate(Operations.Sub, int.MinValue, 1));
        }

        [Fact]
        public void Unknown_Command_Throws_InvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => Calculator.Calculate((Operations)int.MinValue, 1, 1));
        }

    }
}