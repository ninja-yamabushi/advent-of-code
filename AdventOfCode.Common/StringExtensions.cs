using System;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class StringExtensions
    {
        public static int[] GetArrayOfInt(this string source)
        {
            string[] values = source.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return values.Select(s => int.Parse(s)).ToArray();
        }
    }
}
