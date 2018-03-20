using Xunit;

namespace BDSA2017.Exercise01.Tests
{
    public partial class CalculatorTests
    {
        [Fact]
        public void IsPowerOf_given_1764_and_42_returns_true()
        {
            var result = Calculator.IsPowerOf(1764, 42);

            Assert.True(result);
        }

        [Fact]
        public void IsPowerOf_given_42_and_2_returns_false()
        {
            var result = Calculator.IsPowerOf(42, 2);

            Assert.False(result);
        }


        [Theory]
        [InlineData(16, 4)] // 4^2
        [InlineData(8, 2)] // 2^3
        [InlineData(42, 42)] // 42^1
        [InlineData(256, 4)] // 4^4
        [InlineData(125, 5)] // 5^3
        [InlineData(10000, 10)] // 10^4
        [InlineData(9765625, 5)] // 5^10
        public void IsPowerOf_True(int number, int power)
        {
            var result = Calculator.IsPowerOf(number, power);

            Assert.True(result);
        }

        [Theory]
        [InlineData(2, 12)]
        [InlineData(16, 3)]
        [InlineData(10, 2)]
        [InlineData(150, 5)]
        [InlineData(100, 8)]
        public void IsPowerOfTest_IsFalse(int number, int power)
        {
            var result = Calculator.IsPowerOf(number, power);

            Assert.False(result);
        }

        /// <summary>
        /// For negative numbers, depending on the power raised to is odd or even, the sign of the output changes.
        /// </summary>
        [Theory]
        [InlineData(16, -4)] // -4^2
        [InlineData(-8, -2)] // -2^3
        [InlineData(10000, -10)] // -10^4
        public void IsPowerOf_NegativeNumbers(int number, int power)
        {
            var result = Calculator.IsPowerOf(number, power);

            Assert.True(result);
        }

        /// <summary>
        /// 0 to the power of 0 is 1. 0 to any power > 0 is 0.
        /// </summary>
        [Theory]
        [InlineData(1, 0)] // 0^0
        [InlineData(0, 0)] // 0^any other number
        public void IsPowerOf_Zero(int number, int power)
        {
            var result = Calculator.IsPowerOf(number, power);

            Assert.True(result);
        }

        /// <summary>
        /// 1 to any power is always 1.
        /// </summary>
        [Fact]
        public void IsPowerOfTest_1_is_a_power_of_1()
        {
            var result = Calculator.IsPowerOf(1, 1);

            Assert.True(result);
        }

        /// <summary>
        /// Only 1 is a power of 1.
        /// </summary>
        [Fact]
        public void IsPowerOf_10_is_not_a_power_of_1()
        {
            var result = Calculator.IsPowerOf(10, 1);

            Assert.False(result);
        }
    }
}
