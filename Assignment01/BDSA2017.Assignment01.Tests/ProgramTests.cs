using Xunit;
using System;
using System.IO;

namespace BDSA2017.Exercise01.Tests
{
    public class ProgramTests
    {

        private String CaptureConsoleOutput(Action action)
        {
            // Replace out with our mock
            var original_out = Console.Out;
            var captured_out = new StringWriter();
            Console.SetOut(captured_out);

            action.Invoke();

            // Restore out
            Console.SetOut(original_out);

            return captured_out.ToString();
        }

        [Fact]
        public void Main_given_leap_and_year_returns_true()
        {
            var output = CaptureConsoleOutput(() => Program.Main(new[] { "leap", "2000" }));
            Assert.Equal("True" + Environment.NewLine, output);
        }

        [Fact]
        public void Main_given_power_and_two_numbers_returns()
        {
            var output = CaptureConsoleOutput(() => Program.Main(new[] { "6", "powerof", "2" }));
            Assert.Equal("False" + Environment.NewLine, output);
        }

        [Fact]
        public void Main_given_no_arguments_prints_error()
        {
            var output = CaptureConsoleOutput(() => Program.Main(new string[] { }));
            Assert.Equal("Invalid command" + Environment.NewLine, output);
        }

        [Fact]
        public void Main_given_bad_arguments_prints_error()
        {
            var output = CaptureConsoleOutput(() => Program.Main(new[] { "akdjadjslkad" }));
            Assert.Equal("Invalid command" + Environment.NewLine, output);
        }
    }
}
