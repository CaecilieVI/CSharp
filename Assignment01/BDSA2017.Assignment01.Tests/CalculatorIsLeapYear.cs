using Xunit;
using System;

namespace BDSA2017.Exercise01.Tests
{
    public partial class CalculatorTests
    {

        [Fact]
        public void IsLeapYear_given_1581_throws_argument_exception()
        {
            Assert.Throws<ArgumentException>(
                () => Calculator.IsLeapYear(1581)
            );
        }

        [Fact]
        public void IsLeapYear_given_10000_throws_argument_exception()
        {
            Assert.Throws<ArgumentException>(
                () => Calculator.IsLeapYear(10000)
            );
        }

        [Theory]
        [InlineData(1990)]
        [InlineData(1991)]
        [InlineData(1993)]
        public void IsLeapYear_given_year_not_divisible_by_4_returns_false(int year_not_divisible_by_4)
        {
            bool is_leap_year = Calculator.IsLeapYear(year_not_divisible_by_4);
            Assert.False(is_leap_year);
        }

        [Fact]
        public void IsLeapYear_given_year_divisible_by_4_100_and_400_and_returns_true()
        {
            bool is_leap_year = Calculator.IsLeapYear(2000);
            Assert.True(is_leap_year);
        }

        [Fact]
        public void IsLeapYear_given_year_divisible_by_4_but_not_100_and_returns_true()
        {
            bool is_leap_year = Calculator.IsLeapYear(2004);
            Assert.True(is_leap_year);
        }

        [Fact]
        public void IsLeapYear_given_year_divisible_by_4_and_100_but_not_400_and_returns_false()
        {
            bool is_leap_year = Calculator.IsLeapYear(2100);
            Assert.False(is_leap_year);
        }
    }
}