using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Year2020.Day17;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle17_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var dimension = PocketDimension.Parse(inputs);
            var source = new EnergySource(dimension);
            source.Boot(6);

            return dimension.GetActiveCubes().Length.ToString();
        }
    }

    public class Puzzle17_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            throw new NotImplementedException();
        }

    }
}
namespace AdventOfCode.Solutions.Year2020.Day17
{
    public class EnergySource
    {
        private PocketDimension dimension;
        public EnergySource(PocketDimension dimension)
        {
            this.dimension = dimension;
        }
        public void Boot(int numberOfCycles)
        {
            for (int count = 0; count < numberOfCycles; count++)
                applyCycle();
        }

        private void applyCycle()
        {
            dimension.Expand();
            var snapshot = dimension.Clone();

            var allCubes = snapshot.GetAllCubes();
            foreach (var cube in allCubes)
            {
                var activeNeighbors = snapshot.GetNeighbors(cube).Where(c => c.IsActive);
                var activeNeighborsCount = activeNeighbors.Count();
                if (cube.IsActive)
                {
                    if (activeNeighborsCount != 2 && activeNeighborsCount != 3)
                        dimension[cube.Coordinate].DeActivate();
                }
                else
                {
                    if (activeNeighborsCount == 3)
                        dimension[cube.Coordinate].Activate();
                }
            }
            dimension.AdjustBoundaries();
        }
    }

    public class PocketDimension
    {
        private readonly DimensionBoundaries boundaries;
        private readonly List<ConwayCube> cubes;

        public PocketDimension(DimensionBoundaries boundaries)
        {
            this.boundaries = boundaries;
            cubes = new List<ConwayCube>();

            for (int x = boundaries.Xs.Min; x <= boundaries.Xs.Max; x++)
                for (int y = boundaries.Ys.Min; y <= boundaries.Ys.Max; y++)
                    for (int z = boundaries.Zs.Min; z <= boundaries.Zs.Max; z++)
                        cubes.Add(new ConwayCube(new CubeCoordinate(x, y, z)));
        }

        public ConwayCube this[CubeCoordinate coordinate]
        {
            get { return Find(coordinate); }
        }

        public ConwayCube[] GetAllCubes() => cubes.ToArray();

        public ConwayCube[] GetActiveCubes() => cubes.Where(c => c.IsActive).ToArray();

        public ConwayCube[] GetNeighbors(ConwayCube cube)
        {
            var neighborCoordinates = cube.Coordinate.GetNeighbors();
            return neighborCoordinates.Select(c => Find(c, true)).Where(c => c != null).ToArray();
        }

        public void Expand()
        {
            boundaries.IncreaseBothWays(1);
            for (int x = boundaries.Xs.Min; x <= boundaries.Xs.Max; x++)
                for (int y = boundaries.Ys.Min; y <= boundaries.Ys.Max; y++)
                    for (int z = boundaries.Zs.Min; z <= boundaries.Zs.Max; z++)
                        if (boundaries.IsEdge(x, y, z))
                            cubes.Add(new ConwayCube(new CubeCoordinate(x, y, z)));
        }

        public void AdjustBoundaries()
        {

        }

        private ConwayCube Find(CubeCoordinate coordinate, bool allowNulls = false)
        {
            var cube = cubes.Where(c => c.Coordinate.Is(coordinate)).FirstOrDefault();
            if (cube == null && !allowNulls)
            {
                cube = new ConwayCube(coordinate);
                cubes.Add(cube);
            }

            return cube;
        }

        public PocketDimension Clone()
        {
            var pd = new PocketDimension(boundaries);
            foreach (var cube in GetActiveCubes())
                pd[cube.Coordinate].Activate();
            return pd;
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int z = boundaries.Zs.Min; z <= boundaries.Zs.Max; z++)
            {
                result.AppendLine();
                result.AppendLine($"z={z}");
                for (int y = boundaries.Ys.Min; y <= boundaries.Ys.Max; y++)
                {
                    for (int x = boundaries.Xs.Min; x <= boundaries.Xs.Max; x++)
                        result.Append(this[new CubeCoordinate(x, y, z)].ToString());
                    result.AppendLine();
                }
            }

            return result.ToString();
        }

        public static PocketDimension Parse(string inputs)
        {
            var lines = inputs.SplitByLine();
            var size = lines.Length;

            var boundaries = new DimensionBoundaries(DimensionBoundary.FromSize(size), DimensionBoundary.FromSize(size), DimensionBoundary.FromSize(1, true));
            var dimension = new PocketDimension(boundaries);

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    var cube = new ConwayCube(new CubeCoordinate(x, y, 0), lines[y][x]);
                    if (cube.IsActive)
                        dimension[cube.Coordinate].Activate();
                }

            return dimension;
        }

    }

    public class DimensionBoundaries
    {
        public DimensionBoundaries(DimensionBoundary Xs, DimensionBoundary Ys, DimensionBoundary Zs)
        {
            this.Xs = Xs;
            this.Ys = Ys;
            this.Zs = Zs;
        }
        public DimensionBoundary Xs { get; }
        public DimensionBoundary Ys { get; }
        public DimensionBoundary Zs { get; }

        public void IncreaseBothWays(int value)
        {
            Xs.IncreaseBothWays(value);
            Ys.IncreaseBothWays(value);
            Zs.IncreaseBothWays(value);
        }

        public bool IsEdge(int x, int y, int z)
        {
            return (Xs.IsMinOrMax(x) && Ys.IsMinOrMax(y) && Zs.IsMinOrMax(z));
        }
    }

    public class DimensionBoundary
    {
        private DimensionBoundary(int min, int max)
        {
            Min = min;
            Max = max;
        }
        private DimensionBoundary(int size) : this(size * -1, size)
        { }

        public int Min { get; private set; }
        public int Max { get; private set; }

        public bool Includes(int value)
        {
            return (value >= Min && value <= Max);
        }

        public bool IsMinOrMax(int value)
        {
            return (value == Min || value == Max);
        }

        public int Length => (Max - Min) + 1;

        public static DimensionBoundary FromSize(int size, bool negativeEnabled = false)
        {
            if (negativeEnabled)
                return new DimensionBoundary(size / 2);
            return new DimensionBoundary(0, size - 1);
        }

        public void Increase(int value)
        {
            Max += value;
        }
        public void IncreaseBothWays(int value)
        {
            Min -= value;
            Max += value;
        }

        public override string ToString()
        {
            return $"{Min} to {Max} ({Length})";
        }
    }

    public class CubeCoordinate
    {
        public CubeCoordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public bool Is(CubeCoordinate value)
        {
            return (value.X == X && value.Y == Y && value.Z == Z);
        }

        public CubeCoordinate[] GetNeighbors()
        {
            var result = new List<CubeCoordinate>();
            for (int x = X - 1; x <= X + 1; x++)
                for (int y = Y - 1; y <= Y + 1; y++)
                    for (int z = Z - 1; z <= Z + 1; z++)
                    {
                        var cc = new CubeCoordinate(x, y, z);
                        if (!cc.Is(this)) result.Add(cc);
                    }
            return result.ToArray();
        }

        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
    }

    public class ConwayCube
    {
        private const char STATE_ACTIVE = '#';
        private const char STATE_INACTIVE = '.';
        private char state;

        public ConwayCube(CubeCoordinate coordinate) : this(coordinate, STATE_INACTIVE)
        { }
        public ConwayCube(CubeCoordinate coordinate, char state)
        {
            Coordinate = coordinate;
            this.state = state;
        }

        public CubeCoordinate Coordinate { get; }
        public bool IsActive => state == STATE_ACTIVE;

        public void Activate() => state = STATE_ACTIVE;
        public void DeActivate() => state = STATE_INACTIVE;

        public override string ToString()
        {
            return $"[{Coordinate} : {state}]";
        }
    }
}

