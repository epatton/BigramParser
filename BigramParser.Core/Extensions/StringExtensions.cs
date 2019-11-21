using System.Text;
using System.Linq;

namespace BigramParser.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ScrubNonLettersAndDigits(this string str)
        {
            var sb = new StringBuilder();

            string replacement;
            char lastCharAdded = '0';

            for (int i = 0; i < str.Length; ++i)
            {
                var c = str[i];

                replacement = (lastCharAdded == ' ') ? string.Empty : " ";

                // Handle apostrophes as a special case
                if (c == '\'')
                {
                    if (i > 0 && i < str.Length - 1)
                    {
                        if (char.IsLetter(str[i - 1]) && char.IsLetter(str[i + 1]))
                        {
                            sb.Append(c);
                            lastCharAdded = c;
                        }
                    }
                }
                else if (c == '\n' || c == '\r' || c == '\t')
                {
                    sb.Append(replacement);
                    if (replacement != string.Empty)
                        lastCharAdded = replacement.FirstOrDefault();
                }
                else if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                    lastCharAdded = c;
                }
                else
                {
                    sb.Append(replacement);
                    if (replacement != string.Empty)
                        lastCharAdded = replacement.FirstOrDefault();
                }
            }

            return sb.ToString();
        }
    }
}
