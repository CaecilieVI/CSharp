using System;
using System.IO;
using System.Linq;
using Xunit;

namespace BDSA2017.Assignment03.Tests
{
    public class DelegatesTests
    {
        [Fact]
        public void Print_in_reverse_order()
        {
            var str = "Hej med dig";
            var expected = "gid dem jeH";

            Action<string> action = s =>
            {
                foreach (var v in s.Reverse())
                {
                    Console.Write(v);
                }
            };

            var writer = new StringWriter();
            Console.SetOut(writer);
            action(str);
            Assert.Equal(expected, writer.ToString());
        }

        [Fact]
        public void Given_decimals_return_product()
        {
            var double1 = 4.5;
            var double2 = 3.2;
            var expected = 14.4;

            Func<double, double, double> product = (double a, double b) => a * b;
            Assert.Equal(expected, product(double1, double2));
        }

        [Theory]
        [InlineData(100, "100", true)]
        [InlineData(811, "0811", true)]
        [InlineData(96, "97", false)]
        [InlineData(20, "Twenty", false)]
        public void Given_number_and_string_return_true_if_equal(int number, string str, bool expected)
        {
            Func<int, string, bool> equivalent = (n, s) =>
            {
                if(int.TryParse(str, out var eq))
                {
                    return eq == n;
                }
                return false;
            };

            Assert.Equal(expected, equivalent(number, str));
        }
    }
}
