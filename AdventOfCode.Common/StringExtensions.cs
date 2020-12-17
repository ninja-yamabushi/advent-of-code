using System;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class StringExtensions
    {
        public static string[] SplitOn(this string source, string separator)
        {
            return source.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitByLine(this string source)
        {
            return source.SplitOn(Environment.NewLine);
        }
        public static string[] SplitByEmptyLine(this string source)
        {
            return source.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int[] GetArrayOfInt(this string source)
        {
            return source.SplitByLine().Select(s => int.Parse(s)).ToArray();
        }
        public static long[] GetArrayOfLong(this string source)
        {
            return source.SplitByLine().Select(s => long.Parse(s)).ToArray();
        }

        public static bool IsNumeric(this string source)
        {
            return source.Count(c => !char.IsDigit(c)) == 0;
        }

        public static long BinaryToLong(this string source)
        {
            return Convert.ToInt64(source, 2);
        }

        public static string ReplaceAt(this string source, int index, char newChar)
        {
            var chars = source.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
