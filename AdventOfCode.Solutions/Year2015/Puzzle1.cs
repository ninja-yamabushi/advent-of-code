using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle1_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            int opening = inputs.Count(c => c == '(');
            int closing = inputs.Count(c => c == ')');
            return (opening - closing).ToString();
        }
    }

    public class Puzzle1_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            int level = 0;
            for (int index = 0; index < inputs.Length; index++)
            {
                var current = inputs[index];
                if (current == '(') level++;
                if (current == ')') level--;

                if (level == -1) return (index + 1).ToString();
            }

            return "";
        }
    }
}