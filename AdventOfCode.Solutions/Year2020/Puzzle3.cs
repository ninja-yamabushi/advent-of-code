using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle3_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var map = new Map(inputs.SplitByLine());
            return map.CountForSlope(3, 1).ToString();
        }
    }

    public class Puzzle3_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var map = new Map(inputs.SplitByLine());

            long result = 1;
            result = result * map.CountForSlope(1, 1);
            result = result * map.CountForSlope(3, 1);
            result = result * map.CountForSlope(5, 1);
            result = result * map.CountForSlope(7, 1);
            result = result * map.CountForSlope(1, 2);

            return result.ToString();
        }
    }

    internal class Map
    {
        private const char TREE = '#';
        private readonly string[] template;
        private readonly int maxX;
        private readonly int maxY;
        private int x = 0;
        private int y = 0;

        public Map(string[] template)
        {
            this.template = template;
            maxX = template[0].Length;
            maxY = template.Length;
        }

        public void Move(int right, int down)
        {
            x += right;
            if (x >= maxX) x -= maxX;
            y += down;
        }

        public bool Arrived
        {
            get { return y >= maxY; }
        }
        
        public bool IsTree
        { 
            get { return template[y][x] == TREE; }
        }

        public int CountForSlope(int right, int down)
        {
            var count = 0;

            while (!Arrived)
            {
                Move(right, down);
                if (Arrived) break;
                if (IsTree) count++;
            }

            Reset();
            return count;
        }

        public void Reset()
        {
            x = 0; y = 0;
        }
    }

}
