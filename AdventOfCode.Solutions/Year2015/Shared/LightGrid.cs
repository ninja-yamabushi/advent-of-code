using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2015.Shared
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;
    }

    public class LightGrid
    {
        private int[][] grid;
        public LightGrid()
        {
            grid = new int[1000][];
            for (int index = 0; index < 1000; index++)
                grid[index] = new int[1000];
        }

        public int OnCount()
        {
            int count = 0;
            for (int index = 0; index < 1000; index++)
            {
                count += grid[index].Sum();
            }
            return count;
        }

        public void TurnOn(Position start, Position end)
        {
            for (int x = start.X; x <= end.X; x++)
                for (int y = start.Y; y <= end.Y; y++)
                    grid[x][y] = 1;
        }
        public void TurnOff(Position start, Position end)
        {
            for (int x = start.X; x <= end.X; x++)
                for (int y = start.Y; y <= end.Y; y++)
                    grid[x][y] = 0;
        }
        public void Toggle(Position start, Position end)
        {
            for (int x = start.X; x <= end.X; x++)
                for (int y = start.Y; y <= end.Y; y++)
                {
                    if (grid[x][y] == 0)
                        grid[x][y] = 1;
                    else
                        grid[x][y] = 0;
                }
        }
        public void Increase(Position start, Position end, int factor)
        {
            for (int x = start.X; x <= end.X; x++)
                for (int y = start.Y; y <= end.Y; y++)
                    grid[x][y] += factor;
        }
        public void Decrease(Position start, Position end)
        {
            for (int x = start.X; x <= end.X; x++)
                for (int y = start.Y; y <= end.Y; y++)
                    if (grid[x][y] > 0)
                        grid[x][y] -= 1;

        }
    }

    public class LightCommand
    {
        public Position Start;
        public Position End;
        private Action<LightGrid, Position, Position> action;
        public void Execute(LightGrid grid)
        {
            action.Invoke(grid, Start, End);
        }
        public static LightCommand Parse(string input)
        {
            Action<LightGrid, Position, Position> a = null;
            Position[] p = new Position[2];

            if (input.StartsWith("turn on "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.TurnOn(s, e));
                p = GetPositions(input, "turn on ");
            }
            if (input.StartsWith("turn off "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.TurnOff(s, e));
                p = GetPositions(input, "turn off ");
            }
            if (input.StartsWith("toggle "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.Toggle(s, e));
                p = GetPositions(input, "toggle ");
            }

            return new LightCommand()
            {
                Start = p[0],
                End = p[1],
                action = a
            };
        }

        public static LightCommand Parse2(string input)
        {
            Action<LightGrid, Position, Position> a = null;
            Position[] p = new Position[2];

            if (input.StartsWith("turn on "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.Increase(s, e, 1));
                p = GetPositions(input, "turn on ");
            }
            if (input.StartsWith("turn off "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.Decrease(s, e));
                p = GetPositions(input, "turn off ");
            }
            if (input.StartsWith("toggle "))
            {
                a = new Action<LightGrid, Position, Position>((g, s, e) => g.Increase(s, e, 2));
                p = GetPositions(input, "toggle ");
            }

            return new LightCommand()
            {
                Start = p[0],
                End = p[1],
                action = a
            };
        }

        private static Position[] GetPositions(string input, string text)
        {
            var result = new Position[2];

            int s = input.IndexOf(text) + text.Length;
            int e = input.IndexOf(" ", s);
            string value = input.Substring(s, e - s);
            string[] xy = value.Split(',');
            result[0] = new Position(int.Parse(xy[0]), int.Parse(xy[1]));

            text = "through ";
            s = input.IndexOf(text) + text.Length;
            value = input.Substring(s);
            xy = value.Split(',');
            result[1] = new Position(int.Parse(xy[0]), int.Parse(xy[1]));

            return result;
        }
    }

}