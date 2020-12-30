using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public enum SortOrder { Ascending, Descending }

    public static class ListExtensions
    {
        public static List<T> Subset<T>(this List<T> source, int startIndex, int lenght)
        {
            return source.Skip(startIndex).Take(lenght).ToList();
        }
    }

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

        public static IEnumerable<ArrayIndices> AllIndices(this Array array, Func<ArrayIndices, bool> condition = null)
        {
            int numberOfDimensions = array.Rank;
            if (numberOfDimensions == 1)
            {
                for (int x = array.GetLowerBound(0); x <= array.GetUpperBound(0); x++)
                {
                    var result = new ArrayIndices(x);
                    if (condition == null || condition(result))
                        yield return result;
                }
            }
            else
            {
                int[] coordinate = new int[numberOfDimensions];
                int[] upperBounds = new int[numberOfDimensions];

                for (int dimensionIndex = 0; dimensionIndex < numberOfDimensions; dimensionIndex++)
                {
                    coordinate[dimensionIndex] = array.GetLowerBound(dimensionIndex);
                    upperBounds[dimensionIndex] = array.GetUpperBound(dimensionIndex);
                }

                int[] lowerBounds = (int[])coordinate.Clone();

            Repeater:
                {
                    var result = new ArrayIndices(coordinate.ToArray());
                    if (condition == null || condition(result))
                        yield return result;

                    for (int dimensionIndex = numberOfDimensions - 1; dimensionIndex >= 0; dimensionIndex--)
                    {
                        coordinate[dimensionIndex]++;
                        if (coordinate[dimensionIndex] <= upperBounds[dimensionIndex])
                            break;
                        coordinate[dimensionIndex] = lowerBounds[dimensionIndex];
                        if (dimensionIndex == 0)
                            yield break;
                    }
                    goto Repeater;
                }
            }
            yield break;
        }
    }

    public class ArrayIndices
    {
        public static IEqualityComparer<ArrayIndices> GetEqualityComparer() => new ArrayIndicesEqualityComparer();

        private int[] indices;

        public ArrayIndices(params int[] points)
        {
            indices = points;
        }

        public int this[int index]
        {
            get { return indices[index]; }
        }

        public int Count()
        {
            return indices.Length;
        }

        public bool Is(ArrayIndices value)
        {
            if (value.indices.Length != indices.Length)
                return false;

            for (int index = 0; index < indices.Length; index++)
                if (value[index] != indices[index])
                    return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            var value = (ArrayIndices)obj;
            if (obj == null) return false;
            return Is(value);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Join<int>(",", indices);
        }


        private class ArrayIndicesEqualityComparer : IEqualityComparer<ArrayIndices>
        {
            public bool Equals(ArrayIndices x, ArrayIndices y)
            {
                if (y == null && x == null)
                    return true;
                else if (x == null || y == null)
                    return false;
                else if (x.Is(y))
                    return true;
                return false;
            }

            public int GetHashCode(ArrayIndices obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}