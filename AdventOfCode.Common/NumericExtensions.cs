﻿using System;

namespace AdventOfCode.Common
{
    public enum BetweenType { Inclusive, Exclusive }

    public static class NumericExtensions
    {
        public static bool Between(this int source, int min, int max, BetweenType type = BetweenType.Inclusive)
        {
            if (type == BetweenType.Exclusive)
                return (source > min && source < max);
            return (source >= min && source <= max);
        }

        public static int AbsoluteValue(this int source)
        {
            return Math.Abs(source);
        }

        public static string ToBinaryString(this long source, int minLenght = 1)
        {
            return Convert.ToString(source, 2).PadLeft(minLenght, '0');
        }
        public static string ToBinaryString(this int source, int minLenght = 1)
        {
            return Convert.ToString(source, 2).PadLeft(minLenght, '0');
        }
    }
}
