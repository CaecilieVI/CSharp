using System;
using Xunit;

namespace BDSA2017.Assignment02.Tests
{
    public class RegExprTests
    {
        [Fact]
        public void SplitLine_given_lines_returns_stream_of_words()
        {
            var line1 = "Hej med dig";
            var line2 = "12 34";

            string[] lines = { line1, line2 };

            string[] expected = { "Hej", "med", "dig", "12", "34" };

            var actual = RegExpr.SplitLine(lines);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Resolutions_given_string_of_resolutions_returns_stream_of_resolutions_as_tuples()
        {
            var resolutions = "720x480, 1920x1080, 8192x4320";

            ValueTuple<int, int>[] expected = { (720, 480), (1920, 1080), (8192, 4320) };
            var actual = RegExpr.Resolution(resolutions);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InnerText_given_string_of_html_and_tag_returns_inner_text_of_tags()
        {
            var html = "<div> <p>The phrase <i>regular expressions</i> (and consequently, regexes)</p> </div>";
            var tag = "p";

            string[] expected = { "The phrase regular expressions (and consequently, regexes)" };
            var actual = RegExpr.InnerText(html, tag);

            Assert.Equal(expected, actual);
        }
    }
}
