using System;
using System.Text;

namespace BigramParser.Core.Extensions
{
    public static class StringExtensions
    {
        private const string PunctuationMarks = ".,?!;:";

        public static string ReplaceNewLines(this string str, string replacement)
        {
            str = str.Replace("\r\n", replacement);
            str = str.Replace("\n", replacement);
            str = str.Replace("\r", replacement);
            return str;
        }

        public static string ReplacePunctuationMarks(this string str, string replacement)
        {
            foreach(var p in PunctuationMarks)
            {
                str = str.Replace(p.ToString(), replacement);
            }

            return str;
        }

        public static string ReplaceSymbols(this string str, string replacement)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < str.Length; ++i)
            {
                var c = str[i];
                                
                // Handle apostrophes as a special case
                if(c == '\'')
                {
                    if(i > 0 && i < str.Length - 1)
                    {
                        if(char.IsLetter(str[i - 1]) && char.IsLetter(str[i + 1]))
                        {
                            sb.Append(c);
                        }
                    }
                }
                else if(char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(replacement);
                }
            }

            return sb.ToString();
        }

        public static string CleanupExtraSpaces(this string str)
        {
            str = str.Replace('\t', ' ');
            str = RemoveExtraSpaces(str);
            return str;
        }

        // This looks ugly, but it runs incredibly fast compared to even a compiled regex
        private static string RemoveExtraSpaces(string input)
        {
            int len = input.Length,
                index = 0,
                i = 0;
            var src = input.ToCharArray();
            bool skip = false;
            char ch;
            for (; i < len; i++)
            {
                ch = src[i];
                switch (ch)
                {
                    case '\u0020':
                    case '\u00A0':
                    case '\u1680':
                    case '\u2000':
                    case '\u2001':
                    case '\u2002':
                    case '\u2003':
                    case '\u2004':
                    case '\u2005':
                    case '\u2006':
                    case '\u2007':
                    case '\u2008':
                    case '\u2009':
                    case '\u200A':
                    case '\u202F':
                    case '\u205F':
                    case '\u3000':
                    case '\u2028':
                    case '\u2029':
                    case '\u0009':
                    case '\u000A':
                    case '\u000B':
                    case '\u000C':
                    case '\u000D':
                    case '\u0085':
                        if (skip) continue;
                        src[index++] = ch;
                        skip = true;
                        continue;
                    default:
                        skip = false;
                        src[index++] = ch;
                        continue;
                }
            }

            return new string(src, 0, index);
        }
    }
}
