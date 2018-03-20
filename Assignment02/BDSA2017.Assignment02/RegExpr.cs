using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BDSA2017.Assignment02
{
    public static class RegExpr
    {
        public static IEnumerable<string> SplitLine(IEnumerable<string> lines)
        {
            var pattern = "[a-zA-Z0-9]+";

            foreach(var line in lines)
            {
                foreach(var word in Regex.Matches(line, pattern))
                {
                    yield return word.ToString();
                }
            }
        }

        public static IEnumerable<(int width, int height)> Resolution(string resolutions)
        {
            var pattern = "(?<width>[0-9]+)x(?<height>[0-9]+)";

            var matches = Regex.Matches(resolutions, pattern);

            foreach(Match match in matches)
            {
                var width = match.Groups["width"].Value;
                var height = match.Groups["height"].Value;

                yield return (int.Parse(width), int.Parse(height));
            }
        }

        public static IEnumerable<string> InnerText(string html, string tag)
        {
            var outerPattern = "<(" + tag + ").*?>(?<text>.+?)</\\1>";
            var innerPattern = "<.*?>";

            var matches = Regex.Matches(html, outerPattern);

            foreach(Match match in matches)
            {
                var matchString = match.Groups["text"].Value;
                var cleanString = Regex.Replace(matchString, innerPattern, "");
                yield return cleanString;
            }
        }
    }
}
