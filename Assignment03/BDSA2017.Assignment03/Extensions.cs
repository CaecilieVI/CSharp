using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BDSA2017.Assignment02
{
    public static class Extensions
    {
        public static bool IsSecure(this Uri uri)
        {
            if(uri.Scheme == Uri.UriSchemeHttps)
            {
                return true;
            }
            return false;
        }

        public static int WordCount(this string wordString)
        {
            var pattern = @"\b[a-zA-ZæøåÆØÅ]+\b";
            var counter = 0;
            foreach(Match match in Regex.Matches(wordString,pattern))
            {
                counter++;
            }
            return counter;
        }
    }
}
