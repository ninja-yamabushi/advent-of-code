using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2015.Shared;
using System;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle6_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var grid = new LightGrid();

            var commands = inputs.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string command in commands)
            {
                var cmd = LightCommand.Parse(command);
                cmd.Execute(grid);
            }

            return grid.OnCount().ToString();
        }
    }

    public class Puzzle6_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var grid = new LightGrid();

            var commands = inputs.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string command in commands)
            {
                var cmd = LightCommand.Parse2(command);
                cmd.Execute(grid);
            }

            return grid.OnCount().ToString();
        }
    }
}