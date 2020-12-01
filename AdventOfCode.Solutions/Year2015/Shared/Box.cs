using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2015.Shared
{
    internal class Box
    {
        public readonly int length;
        public readonly int width;
        public readonly int height;
        private readonly int[] _sortedAreas;
        private readonly int[] _sortedSides;

        public Box(int l, int w, int h)
        {
            length = l;
            width = w;
            height = h;

            _sortedAreas = new int[] { AreaSide1, AreaSide2, AreaSide3 };
            Array.Sort(_sortedAreas);

            _sortedSides = new int[] { length, width, height };
            Array.Sort(_sortedSides);
        }

        public int AreaSide1 => length * width;
        public int AreaSide2 => width * height;
        public int AreaSide3 => height * length;
        public int Volume => length * width * height;

        public int[] GetAreaInAscendingOrder()
        {
            return _sortedAreas;
        }

        public int[] GetSidesInAscendingOrder()
        {
            return _sortedSides;
        }

        public override string ToString()
        {
            return $"{length}x{width}x{height}";
        }

        public static Box ParseSingle(string input)
        {
            string[] values = input.Split('x');
            if (values.Length != 3)
                throw new ArgumentException();
            return new Box(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }

        public static Box[] Parse(string inputs)
        {
            string[] values = inputs.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return values.Select(s => ParseSingle(s)).ToArray();
        }
    }
}
