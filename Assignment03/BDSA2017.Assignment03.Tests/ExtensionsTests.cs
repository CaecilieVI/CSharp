using System;
using Xunit;

namespace BDSA2017.Assignment02.Tests
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData("https://github.itu.dk/", true)]
        [InlineData("https://itu.dk", true)]
        [InlineData("http://github.itu.dk/", false)]
        [InlineData("http://itu.dk", false)]
        public void IsSecure_given_Uri_using_https_returns_true(string uri, bool expected)
        {
            var actual = Extensions.IsSecure(new Uri(uri));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Hej med dig", 3)]
        [InlineData("Hvordan går det ?", 3)]
        [InlineData("", 0)]
        [InlineData("H3j m3d dig", 1)]
        public void WordCount_given_string_returns_number_of_words(string wordString, int expected)
        {
            var actual = Extensions.WordCount(wordString);
            Assert.Equal(expected, actual);
        }
    }
}
