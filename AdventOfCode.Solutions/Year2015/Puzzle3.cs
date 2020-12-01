using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2015.Shared;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle3_1 : IPuzzle
    {
        private readonly List<string> visited = new List<string>();

        public string Solve(string inputs)
        {
            int x = 0; int y = 0;

            UpdateVisitedPosition(x, y);
            for (int index = 0; index < inputs.Length; index++)
            {
                var current = inputs[index];
                if (current == '<') x--;
                if (current == '>') x++;
                if (current == '^') y++;
                if (current == 'v') y--;

                UpdateVisitedPosition(x, y);
            }

            return visited.Count.ToString();
        }

        private void UpdateVisitedPosition(int x, int y)
        {
            var position = $"{x},{y}";

            if (!visited.Contains(position))
                visited.Add(position);
        }
    }

    public class Puzzle3_2 : IPuzzle
    {
        private readonly List<string> visited = new List<string>();

        public string Solve(string inputs)
        {
            var santa = new SantaPosition(0, 0);
            var robot = new SantaPosition(0, 0);

            UpdateVisitedPositions(santa.Position);
            for (int index = 0; index < inputs.Length; index = index + 2)
            {
                var directionSanta = inputs[index];
                UpdateVisitedPositions(santa.Move(directionSanta));

                var directionRobot = inputs[index + 1];
                UpdateVisitedPositions(robot.Move(directionRobot));
            }

            return visited.Count.ToString();
        }

        private void UpdateVisitedPositions(string position)
        {
            if (!visited.Contains(position))
                visited.Add(position);
        }
    }
}