using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2015.Shared;
using System.Linq;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle2_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            int total = 0;

            Box[] boxes = Box.Parse(inputs);
            foreach (Box box in boxes)
            {
                int paper = 2 * box.AreaSide1 + 2 * box.AreaSide2 + 2 * box.AreaSide3;
                int extraPaper = box.GetAreaInAscendingOrder()[0];
                total += paper + extraPaper;
            }

            return total.ToString();
        }
    }

    public class Puzzle2_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            int total = 0;

            Box[] boxes = Box.Parse(inputs);
            foreach (Box box in boxes)
            {
                int[] sides = box.GetSidesInAscendingOrder();
                int ribbon = sides[0] + sides[0] + sides[1] + sides[1];
                int bow = box.Volume;
                total += ribbon + bow;
            }

            return total.ToString();
        }
    }
}