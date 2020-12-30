using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var dimension = PocketDimension.Parse(inputs, 4);
            var source = new EnergySource(dimension);
            source.Boot(6);

            return dimension.GetActiveCubes().Length.ToString();
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
            var snapshot = dimension.GetSnapshot();

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
            dimension.Compress();
        }
    }

    public class PocketDimension
    {
        public readonly DimensionBoundaries boundaries;
        private readonly Dictionary<ArrayIndices, ConwayCube> cubes;

        public PocketDimension(DimensionBoundaries boundaries)
        {
            this.boundaries = boundaries;
            cubes = new Dictionary<ArrayIndices, ConwayCube>();

            foreach (var indices in boundaries.AllIndices())
                cubes.Add(new ConwayCube(indices));
        }

        public ConwayCube this[ArrayIndices coordinate]
        {
            get { return Find(coordinate); }
        }

        public ConwayCube[] GetAllCubes() => cubes.Values.ToArray();

        public ConwayCube[] GetActiveCubes() => cubes.Values.Where(c => c.IsActive).ToArray();

        public ConwayCube[] GetNeighbors(ConwayCube cube)
        {
            var neighborCoordinates = cube.Coordinate.GetNeighbors();
            return neighborCoordinates.Select(c => Find(c, true)).Where(c => c != null).ToArray();
        }

        public void Expand()
        {
            boundaries.IncreaseBothWays(1);

            var condition = new Func<ArrayIndices, bool>(value => boundaries.IsEdge(value));
            foreach (var edgeIndices in boundaries.AllIndices(condition))
                    cubes.Add(new ConwayCube(edgeIndices));
        }

        public enum Edge { Min, Max }

        private ConwayCube[] GetWall(int boudaryIndex, Edge edge)
        {
            var value = edge == Edge.Min ? boundaries[boudaryIndex].Min : boundaries[boudaryIndex].Max;
            return cubes.Values.Where(c => c.Coordinate[boudaryIndex] == value).ToArray();
        }

        private bool RemoveWallIfNoActiveCube(int boundaryIndex, Edge edge)
        {
            var wallCubes = GetWall(boundaryIndex, edge);
            if (wallCubes.Where(c => c.IsActive).Count() == 0)
            {
                foreach (var cube in wallCubes)
                    cubes.Remove(cube);

                return true;
            }
            return false;
        }

        public void Compress()
        {
            for (int index = 0; index < boundaries.Count(); index++)
            {
                while (RemoveWallIfNoActiveCube(index, Edge.Min)) { boundaries[index].ReduceMin(1); };
                while (RemoveWallIfNoActiveCube(index, Edge.Max)) { boundaries[index].ReduceMax(1); };
            }
        }

        private ConwayCube Find(ArrayIndices coordinate, bool allowNulls = false)
        {
            cubes.TryGetValue(coordinate, out ConwayCube cube);
            if (cube == null && !allowNulls)
            {
                cube = new ConwayCube(coordinate);
                cubes.Add(cube);
            }
            return cube;
        }

        public PocketDimension GetSnapshot()
        {
            var pd = new PocketDimension(boundaries);
            foreach (var cube in GetActiveCubes())
                pd[cube.Coordinate].Activate();
            return pd;
        }

        public static PocketDimension Parse(string inputs, int numberOfDimensions = 3)
        {
            var lines = inputs.SplitByLine();
            var size = lines.Length;

            var boundaries = new List<DimensionBoundary> { DimensionBoundary.FromSize(size), DimensionBoundary.FromSize(size) };
            if (numberOfDimensions >= 3) boundaries.Add(DimensionBoundary.FromSize(1, true));
            if (numberOfDimensions >= 4) boundaries.Add(DimensionBoundary.FromSize(1, true));

            var dimension = new PocketDimension(new DimensionBoundaries(boundaries.ToArray()));

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    var c = numberOfDimensions == 3 ? new ArrayIndices(x, y, 0) : new ArrayIndices(x, y, 0, 0);
                    var cube = new ConwayCube(c, lines[y][x]);

                    if (cube.IsActive)
                        dimension[cube.Coordinate].Activate();
                }

            return dimension;
        }
    }

    public static class DictionaryOfConwayCubeExtensions
    {
        public static void Add(this Dictionary<ArrayIndices, ConwayCube> source, ConwayCube cube)
        {
            source.Add(cube.Coordinate, cube);
        }
        public static void Remove(this Dictionary<ArrayIndices, ConwayCube> source, ConwayCube cube)
        {
            source.Remove(cube.Coordinate);
        }
    }

    public class DimensionBoundaries
    {
        private DimensionBoundary[] boundaries;
        public DimensionBoundaries(params DimensionBoundary[] boundaries)
        {
            this.boundaries = boundaries;
        }

        public DimensionBoundary this[int index] => boundaries[index];

        public int Count() => boundaries.Length;

        public void IncreaseBothWays(int value)
        {
            foreach (var b in boundaries)
                b.IncreaseBothWays(value);
        }

        public bool IsEdge(ArrayIndices coordinate)
        {
            for (int index = 0; index < boundaries.Length; index++)
                if (this[index].IsMinOrMax(coordinate[index]))
                    return true;
            return false;
        }

        public IEnumerable<ArrayIndices> AllIndices(Func<ArrayIndices, bool> condition = null)
        {
            int numberOfDimensions = boundaries.Length;
            if (numberOfDimensions == 1)
            {
                for (int x = boundaries[0].Min; x <= boundaries[0].Max; x++)
                {
                    var result = new ArrayIndices(x);
                    if (condition == null || condition(result))
                        yield return result;
                }
            }
            else
            {
                int[] coordinate = new int[numberOfDimensions];
                int[] upperBounds = new int[numberOfDimensions];

                for (int dimensionIndex = 0; dimensionIndex < numberOfDimensions; dimensionIndex++)
                {
                    coordinate[dimensionIndex] = boundaries[dimensionIndex].Min;
                    upperBounds[dimensionIndex] = boundaries[dimensionIndex].Max;
                }

                int[] lowerBounds = (int[])coordinate.Clone();

            Repeater:
                {
                    var result = new ArrayIndices(coordinate.ToArray());
                    if (condition == null || condition(result))
                        yield return result;

                    for (int dimensionIndex = numberOfDimensions - 1; dimensionIndex >= 0; dimensionIndex--)
                    {
                        coordinate[dimensionIndex]++;
                        if (coordinate[dimensionIndex] <= upperBounds[dimensionIndex])
                            break;
                        coordinate[dimensionIndex] = lowerBounds[dimensionIndex];
                        if (dimensionIndex == 0)
                            yield break;
                    }
                    goto Repeater;
                }
            }
            yield break;
        }

        public static DimensionBoundaries FromPoint(ArrayIndices source)
        {
            var boundaries = new List<DimensionBoundary>();
            for (int d = 0; d < source.Count(); d++)
                boundaries.Add(DimensionBoundary.FromPoint(source[d]));
            return new DimensionBoundaries(boundaries.ToArray());
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
        public static DimensionBoundary FromPoint(int point)
        {
            return new DimensionBoundary(point, point);
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
        public void ReduceMin(int value)
        {
            Min += value;
        }
        public void ReduceMax(int value)
        {
            Max -= value;
        }

        public override string ToString()
        {
            return $"{Min} to {Max} ({Length})";
        }
    }

    public static class ArrayIndicesExtensions
    {
        public static ArrayIndices[] GetNeighbors(this ArrayIndices source)
        {
            var boundaries = DimensionBoundaries.FromPoint(source);
            boundaries.IncreaseBothWays(1);

            var condition = new Func<ArrayIndices, bool>(value => !value.Is(source));

            var result = new List<ArrayIndices>();
            foreach (var indices in boundaries.AllIndices(condition))
                result.Add(indices);

            return result.ToArray();
        }
    }

    public class ConwayCube
    {
        private const char STATE_ACTIVE = '#';
        private const char STATE_INACTIVE = '.';
        private char state;

        public ConwayCube(ArrayIndices coordinate) : this(coordinate, STATE_INACTIVE)
        { }
        public ConwayCube(ArrayIndices coordinate, char state)
        {
            Coordinate = coordinate;
            this.state = state;
        }

        public ArrayIndices Coordinate { get; }

        public bool IsActive => state == STATE_ACTIVE;

        public void Activate() => state = STATE_ACTIVE;
        public void DeActivate() => state = STATE_INACTIVE;

        public override string ToString()
        {
            return $"[{Coordinate} : {state}]";
        }
    }
}

