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
                return new EmptyPuzzle(year, day, problem);
            return Activator.CreateInstance(solverType) as IPuzzle;
        }

        private class EmptyPuzzle : IPuzzle
        {
            private readonly string message;
            public EmptyPuzzle(int year, int day, int problem)
            {
                message = $"The solution for the challenge ({problem}) of day {day} of yeay {year} is not implemented.";
            }
            public string Solve(string inputs)
            {
                return message;
            }
        }
    }
}
