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
    }
}
