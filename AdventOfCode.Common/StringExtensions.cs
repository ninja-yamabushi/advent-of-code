using System;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class StringExtensions
    {
        public static string[] SplitOn(this string source, string separator)
        {
            return source.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitByLine(this string source)
        {
            return source.SplitOn(Environment.NewLine);
        }
        public static int[] GetArrayOfInt(this string source)
        {
            return source.SplitByLine().Select(s => int.Parse(s)).ToArray();
        }
    }
}
