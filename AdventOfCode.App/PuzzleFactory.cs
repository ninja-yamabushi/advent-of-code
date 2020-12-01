using AdventOfCode.Common;
using AdventOfCode.Solutions;
using System.Linq;

namespace AdventOfCode.App
{
    public class PuzzleFactory
    {
        public IPuzzle Create(string year, string day, string problem)
        {
            int y = int.Parse(year);
            int d = int.Parse(day);
            int p = int.Parse(problem);

            return SolutionProvider.GetPuzzle(y, d, p);
        }

        public static int[] GetYears()
        {
            return Enumerable.Range(2015, 6).ToArray();
        }

        public static int[] GetDays()
        {
            return Enumerable.Range(1, 25).ToArray();
        }

        public static int[] GetProblems()
        {
            return Enumerable.Range(1, 2).ToArray();
        }
    }
}
