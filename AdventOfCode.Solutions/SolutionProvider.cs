using AdventOfCode.Common;
using System;

namespace AdventOfCode.Solutions
{
    public class SolutionProvider
    {
        public static IPuzzle GetPuzzle(int year, int day, int problem)
        {
            var solver = $"AdventOfCode.Solutions.Year{year}.Puzzle{day}_{problem}";
            var solverType = Type.GetType(solver);
            if (solverType == null)
                return new EmptyPuzzle();
            return Activator.CreateInstance(solverType) as IPuzzle;
        }

        private class EmptyPuzzle : IPuzzle
        {
            public string Solve(string inputs)
            {
                return "Not implemented Puzzle.";
            }
        }
    }
}
