using System;
using System.Linq;

namespace AdventOfCode.Common
{
    public enum SortOrder { Ascending, Descending }

    public static class ArrayExtensions
    {
        
        public static T[] Subset<T>(this T[] source, int startIndex, int lenght)
        {
            return source.Skip(startIndex).Take(lenght).ToArray();
        }
        public static T[] Subset<T>(this T[] source, int startIndex)
        {
            return source.Skip(startIndex).ToArray();
        }

        public static T[] Sort<T>(this T[] source, SortOrder order = SortOrder.Ascending)
        {
            if (order == SortOrder.Ascending) Array.Sort(source);
            if (order == SortOrder.Descending) Array.Reverse(source);

            return source;
        }
    }
}

