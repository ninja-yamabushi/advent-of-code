using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.Day17;
using AdventOfCode.Solutions.Year2020.Day18;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass] //Day 13 to 19
    public class Puzzle2020_Week3_Fixture : BasePuzzle2020Fixture
    {
        //Day19

        //Day18
        [DataRow(18, 1, "1 + 2 * 3 + 4 * 5 + 6", "71", InputTypes.Value)]
        [DataRow(18, 1, "1 + (2 * 3) + (4 * (5 + 6))", "51", InputTypes.Value)]
        [DataRow(18, 1, "2 * 3 + (4 * 5)", "26", InputTypes.Value)]
        [DataRow(18, 1, "5 + (8 * 3 + 9 + 3 * 4 * 3)", "437", InputTypes.Value)]
        [DataRow(18, 1, "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "12240", InputTypes.Value)]
        [DataRow(18, 1, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "13632", InputTypes.Value)]
        [DataRow(18, 1, "2020_18_01", "11004703763391")]
        [DataRow(18, 2, "1 + 2 * 3 + 4 * 5 + 6", "231", InputTypes.Value)]
        [DataRow(18, 2, "1 + (2 * 3) + (4 * (5 + 6))", "51", InputTypes.Value)]
        [DataRow(18, 2, "2 * 3 + (4 * 5)", "46", InputTypes.Value)]
        [DataRow(18, 2, "5 + (8 * 3 + 9 + 3 * 4 * 3)", "1445", InputTypes.Value)]
        [DataRow(18, 2, "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", "669060", InputTypes.Value)]
        [DataRow(18, 2, "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", "23340", InputTypes.Value)]
        [DataRow(18, 2, "2020_18_01", "290726428573651")]
        //Day 16
        [DataRow(16, 1, "2020_16_01_example", "71")]
        [DataRow(16, 1, "2020_16_01", "21081")]
        [DataRow(16, 2, "2020_16_01", "314360510573")]
        //Day 15
        [DataRow(15, 1, "0,3,6", "436", InputTypes.Value)]
        [DataRow(15, 1, "1,3,2", "1", InputTypes.Value)]
        [DataRow(15, 1, "2,1,3", "10", InputTypes.Value)]
        [DataRow(15, 1, "1,2,3", "27", InputTypes.Value)]
        [DataRow(15, 1, "2,3,1", "78", InputTypes.Value)]
        [DataRow(15, 1, "3,2,1", "438", InputTypes.Value)]
        [DataRow(15, 1, "3,1,2", "1836", InputTypes.Value)]
        [DataRow(15, 1, "0,1,4,13,15,12,16", "1665", InputTypes.Value)]
        //Day 14
        [DataRow(14, 1, "2020_14_01_example", "165")]
        [DataRow(14, 1, "2020_14_01", "13727901897109")]
        [DataRow(14, 2, "2020_14_02_example", "208")]
        [DataRow(14, 2, "2020_14_01", "5579916171823")]
        //Day 13
        [DataRow(13, 1, "2020_13_01_example", "295")]
        [DataRow(13, 1, "2020_13_01", "222")]
        [DataRow(13, 2, "2020_13_01_example", "1068781")]
        [DataRow(13, 2, "0\r\n17,x,13,19", "3417", InputTypes.Value)]
        [DataRow(13, 2, "0\r\n67,7,59,61", "754018", InputTypes.Value)]
        [DataRow(13, 2, "0\r\n67,x,7,59,61", "779210", InputTypes.Value)]
        [DataRow(13, 2, "0\r\n67,7,x,59,61", "1261476", InputTypes.Value)]
        [DataRow(13, 2, "0\r\n1789,37,47,1889", "1202161486", InputTypes.Value)]
        //[DataRow(13, 2, "2020_13_01", "-1")]
        [DataTestMethod]
        public void AllPuzzlesShouldSolve(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            solve(day, challenge, input, expected, inputType);
        }

        //Day17
        [DataRow(17, 1, "2020_17_01_example", "112")]
        [DataRow(17, 1, "2020_17_01", "315")]
        [DataRow(17, 2, "2020_17_01_example", "848")]
        [DataRow(17, 2, "2020_17_01", "1520")]
        //Day15
        //[DataRow(15, 2, "0,3,6", "175594", InputTypes.Value)]
        //[DataRow(15, 2, "1,3,2", "2578", InputTypes.Value)]
        //[DataRow(15, 2, "2,1,3", "3544142", InputTypes.Value)]
        //[DataRow(15, 2, "1,2,3", "261214", InputTypes.Value)]
        //[DataRow(15, 2, "2,3,1", "6895259", InputTypes.Value)]
        //[DataRow(15, 2, "3,2,1", "18", InputTypes.Value)]
        //[DataRow(15, 2, "3,1,2", "362", InputTypes.Value)]
        //[DataRow(15, 2, "0,1,4,13,15,12,16", "16439", InputTypes.Value)]
        //Day13
        //[DataRow(13, 2, "2020_13_01", "-1")]
        [DataTestMethod]
        [Ignore("Long running")]
        public void AllPuzzlesShouldSolveLongRunning(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            solve(day, challenge, input, expected, inputType);
        }
    }

    public static class DimensionBoundaryTestExtensions
    {
        public static bool IsEdge(this DimensionBoundaries source, int x, int y, int z)
        {
            return source.IsEdge(new ArrayIndices(x, y, z));
        }
    }

    [TestClass]
    public class Puzzle2020_Week3_Day14_Fixture
    {
        [TestMethod]
        public void PuzzleDay14_1_ShouldConvertBinaryToLong()
        {
            string _0 = "000000000000000000000000000000000000";
            string _11 = "000000000000000000000000000000001011";
            string _73 = "000000000000000000000000000001001001";
            string max = "111111111111111111111111111111111111";

            Assert.AreEqual(0, _0.BinaryToLong());
            Assert.AreEqual(11, _11.BinaryToLong());
            Assert.AreEqual(73, _73.BinaryToLong());
            Assert.AreEqual(68719476735, max.BinaryToLong());
        }
        [TestMethod]
        public void PuzzleDay14_1_ShouldConvertIntToBinary36()
        {
            long l0 = 0;
            long l11 = 11;
            long l73 = 73;
            string _0 = "000000000000000000000000000000000000";
            string _11 = "000000000000000000000000000000001011";
            string _73 = "000000000000000000000000000001001001";

            Assert.AreEqual(_0, l0.ToBinaryString(36));
            Assert.AreEqual(_11, l11.ToBinaryString(36));
            Assert.AreEqual(_73, l73.ToBinaryString(36));
        }
        [TestMethod]
        public void PuzzleDay14_1_ShouldParseValue()
        {
            Assert.AreEqual(586700041, Year2020.Day14.InputLine.ParseValue("mem[51810] = 586700041").Value);
        }
        [TestMethod]
        public void PuzzleDay14_1_ShouldParseValueAddress()
        {
            Assert.AreEqual(51810, Year2020.Day14.InputLine.ParseAddress("mem[51810] = 586700041").Value);
        }

        [TestMethod]
        public void PuzzleDay14_1_ShouldApplyMask()
        {

            Assert.AreEqual(0, applyMaskToValue(0, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX").Value);
            Assert.AreEqual(101, applyMaskToValue(101, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX").Value);

            Assert.AreEqual(64, applyMaskToValue(0, "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X").Value);
        }
        private Year2020.Day14.MemoryValue applyMaskToValue(int value, string mask)
        {
            var v = new Year2020.Day14.MemoryValue(value);
            var m = new Year2020.Day14.Mask(mask);
            v = m.ApplyOn(v);
            return v;
        }
    }

    [TestClass]
    public class Puzzle2020_Week3_Day17_Fixture
    {
        private const int X = 0;
        private const int Y = 1;
        private const int Z = 2;
        private const int W = 3;

        [TestMethod]
        public void PuzzleDay17_1_CubeCoordinateShouldBeReadable()
        {
            new ArrayIndices(1, 2).ToString().Should().Be("1,2");
            new ArrayIndices(1, 2, 3).ToString().Should().Be("1,2,3");
            new ArrayIndices(1, 2, 3, 4).ToString().Should().Be("1,2,3,4");
        }
        [TestMethod]
        public void PuzzleDay17_1_CubeCoordinateShouldReturnAllNeighbors4D()
        {
            var coordinate = new ArrayIndices(2, 2, 2, 2);
            var actual = coordinate.GetNeighbors().Length;

            actual.Should().Be(80);
        }
        [TestMethod]
        public void PuzzleDay17_1_CubeCoordinateShouldReturnAllNeighbors()
        {
            ArrayIndices[] expected = new[]
            {
                //top of cube back
                new ArrayIndices(1,1,1),
                new ArrayIndices(2,1,1),
                new ArrayIndices(3,1,1),
                //top of cube center
                new ArrayIndices(1,1,2),
                new ArrayIndices(2,1,2),
                new ArrayIndices(3,1,2),
                //top of cube front
                new ArrayIndices(1,1,3),
                new ArrayIndices(2,1,3),
                new ArrayIndices(3,1,3),
                //bottom of cube back
                new ArrayIndices(1,3,1),
                new ArrayIndices(2,3,1),
                new ArrayIndices(3,3,1),
                //bottom of cube center
                new ArrayIndices(1,3,2),
                new ArrayIndices(2,3,2),
                new ArrayIndices(3,3,2),
                //bottom of cube front
                new ArrayIndices(1,3,3),
                new ArrayIndices(2,3,3),
                new ArrayIndices(3,3,3),
                //front of cube center
                new ArrayIndices(1,2,3),
                new ArrayIndices(2,2,3),
                new ArrayIndices(3,2,3),
                //back of cube center
                new ArrayIndices(1,2,1),
                new ArrayIndices(2,2,1),
                new ArrayIndices(3,2,1),
                //left of cube
                new ArrayIndices(1,2,2),
                //right of cube
                new ArrayIndices(3,2,2)
            };

            var coordinate = new ArrayIndices(2, 2, 2);
            var actual = coordinate.GetNeighbors();

            actual.Length.Should().Be(26);
            actual.Should().BeEquivalentTo(expected);
        }
        [TestMethod]
        public void PuzzleDay17_1_CubeCoordinateShouldReturnAllNeighborsWithNegatives()
        {
            ArrayIndices[] expected = new[]
            {
                //top of cube back
                new ArrayIndices(-1,-1,-1),
                new ArrayIndices(0,-1,-1),
                new ArrayIndices(1,-1,-1),
                //top of cube center
                new ArrayIndices(-1,-1,0),
                new ArrayIndices(0,-1,0),
                new ArrayIndices(1,-1,0),
                //top of cube front
                new ArrayIndices(-1,-1,1),
                new ArrayIndices(0,-1,1),
                new ArrayIndices(1,-1,1),
                //bottom of cube back
                new ArrayIndices(-1,1,-1),
                new ArrayIndices(0,1,-1),
                new ArrayIndices(1,1,-1),
                //bottom of cube center
                new ArrayIndices(-1,1,0),
                new ArrayIndices(0,1,0),
                new ArrayIndices(1,1,0),
                //bottom of cube front
                new ArrayIndices(-1,1,1),
                new ArrayIndices(0,1,1),
                new ArrayIndices(1,1,1),
                //front of cube center
                new ArrayIndices(-1,0,1),
                new ArrayIndices(0,0,1),
                new ArrayIndices(1,0,1),
                //back of cube center
                new ArrayIndices(-1,0,-1),
                new ArrayIndices(0,0,-1),
                new ArrayIndices(1,0,-1),
                //left of cube
                new ArrayIndices(-1,0,0),
                //right of cube
                new ArrayIndices(1,0,0)
            };

            var coordinate = new ArrayIndices(0, 0, 0);
            var actual = coordinate.GetNeighbors();

            actual.Should().BeEquivalentTo(expected);
        }
        [TestMethod]
        public void PuzzleDay17_1_DimensionBoundaryShouldIncreaseBoundaries()
        {
            var b = Year2020.Day17.DimensionBoundary.FromSize(3);
            b.Min.Should().Be(0);
            b.Max.Should().Be(2);
            b.Length.Should().Be(3);

            b.Increase(2);
            b.Min.Should().Be(0);
            b.Max.Should().Be(4);
            b.Length.Should().Be(5);

            b.IncreaseBothWays(1);
            b.Min.Should().Be(-1);
            b.Max.Should().Be(5);
            b.Length.Should().Be(7);
        }
        [TestMethod]
        public void PuzzleDay17_1_DimensionBoundaryShouldBeIncludedInBoundaryRange()
        {
            var b = Year2020.Day17.DimensionBoundary.FromSize(3);
            b.Includes(-1).Should().BeFalse();
            b.Includes(3).Should().BeFalse();
            b.Includes(0).Should().BeTrue();
            b.Includes(1).Should().BeTrue();
            b.Includes(2).Should().BeTrue();

            b = Year2020.Day17.DimensionBoundary.FromSize(3, true);
            b.Includes(-2).Should().BeFalse();
            b.Includes(2).Should().BeFalse();
            b.Includes(-1).Should().BeTrue();
            b.Includes(0).Should().BeTrue();
            b.Includes(1).Should().BeTrue();
        }

        [TestMethod]
        public void PuzzleDay17_1_PocketDimensionShouldExpand()
        {
            var inputs = @"...
.#.
...";

            var dimension = Year2020.Day17.PocketDimension.Parse(inputs);
            dimension.GetAllCubes().Length.Should().Be(9);

            dimension.Expand();

            dimension.boundaries[X].Length.Should().Be(5);
            dimension.boundaries[Y].Length.Should().Be(5);
            dimension.boundaries[Z].Length.Should().Be(3);
            dimension.GetAllCubes().Length.Should().Be(75);
        }
        [TestMethod]
        public void PuzzleDay17_1_PocketDimensionShouldCompress()
        {
            var inputs = @"...
.#.
...";

            var dimension = Year2020.Day17.PocketDimension.Parse(inputs);
            dimension.Compress();

            dimension.boundaries[X].Length.Should().Be(1);
            dimension.boundaries[Y].Length.Should().Be(1);
            dimension.boundaries[Z].Length.Should().Be(1);
            dimension.GetAllCubes().Length.Should().Be(1);
        }
        [TestMethod]
        public void PuzzleDay17_1_PocketDimensionShouldExpandAndCompress()
        {
            var inputs = @"...
.#.
...";

            var dimension = Year2020.Day17.PocketDimension.Parse(inputs);
            dimension.Expand();
            dimension.Expand();
            dimension.Expand();
            dimension.Compress();

            dimension.boundaries[X].Length.Should().Be(1);
            dimension.boundaries[Y].Length.Should().Be(1);
            dimension.boundaries[Z].Length.Should().Be(1);
            dimension.GetAllCubes().Length.Should().Be(1);
        }
        [TestMethod]
        public void PuzzleDay17_1_DimensionBoundariesShouldFindEdges()
        {
            var boundaries = new Year2020.Day17.DimensionBoundaries(
                Year2020.Day17.DimensionBoundary.FromSize(3),
                Year2020.Day17.DimensionBoundary.FromSize(3),
                Year2020.Day17.DimensionBoundary.FromSize(3, true));

            boundaries.IsEdge(1, 1, 0).Should().BeFalse();
            //left
            boundaries.IsEdge(0, 0, -1).Should().BeTrue();
            boundaries.IsEdge(0, 1, -1).Should().BeTrue();
            boundaries.IsEdge(0, 2, -1).Should().BeTrue();
            boundaries.IsEdge(0, 0, 0).Should().BeTrue();
            boundaries.IsEdge(0, 1, 0).Should().BeTrue();
            boundaries.IsEdge(0, 2, 0).Should().BeTrue();
            boundaries.IsEdge(0, 0, 1).Should().BeTrue();
            boundaries.IsEdge(0, 1, 1).Should().BeTrue();
            boundaries.IsEdge(0, 2, 1).Should().BeTrue();
            //right
            boundaries.IsEdge(2, 0, -1).Should().BeTrue();
            boundaries.IsEdge(2, 1, -1).Should().BeTrue();
            boundaries.IsEdge(2, 2, -1).Should().BeTrue();
            boundaries.IsEdge(2, 0, 0).Should().BeTrue();
            boundaries.IsEdge(2, 1, 0).Should().BeTrue();
            boundaries.IsEdge(2, 2, 0).Should().BeTrue();
            boundaries.IsEdge(2, 0, 1).Should().BeTrue();
            boundaries.IsEdge(2, 1, 1).Should().BeTrue();
            boundaries.IsEdge(2, 2, 1).Should().BeTrue();
            //top
            boundaries.IsEdge(1, 0, -1).Should().BeTrue();
            boundaries.IsEdge(1, 0, 0).Should().BeTrue();
            boundaries.IsEdge(2, 0, 1).Should().BeTrue();
            //bottom
            boundaries.IsEdge(1, 2, -1).Should().BeTrue();
            boundaries.IsEdge(1, 2, 0).Should().BeTrue();
            boundaries.IsEdge(2, 2, 1).Should().BeTrue();
            //back
            boundaries.IsEdge(1, 1, -1).Should().BeTrue();
            //front
            boundaries.IsEdge(1, 1, 1).Should().BeTrue();
        }
        [TestMethod]
        public void PuzzleDay17_1_DictionaryShouldUseCoordinateAsKey()
        {

            int a = "1,0,-1".GetHashCode();
            int b = "1,0,-1".GetHashCode();
            int c = "-1,0,1".GetHashCode();

            a.Should().Be(b);
            a.Should().NotBe(c);
        }

        [TestMethod]
        public void PuzzleDay17_1_ShouldGetAllArrayIndices()
        {
            var inputs = new int[,,] { { { 0, 0 }, { 0, 0 } }, { { 0, 0 }, { 0, 0 } } };

            int counter = 0;
            foreach (var item in inputs.AllIndices())
                counter++;

            counter.Should().Be(8);
        }

        [DataRow(3, 3)]
        [DataRow(9, 3, 3)]
        [DataRow(18, 3, 3, 2)]
        [DataTestMethod]
        public void PuzzleDay17_1_ShouldGetAllPossibleIndices(int expected, params int[] boundaryQuantities)
        {
            var b = new List<DimensionBoundary>();
            for (int i = 0; i < boundaryQuantities.Length; i++)
                b.Add(DimensionBoundary.FromSize(boundaryQuantities[i]));

            var inputs = new DimensionBoundaries(b.ToArray());
            int actual = 0;
            foreach (var item in inputs.AllIndices())
                actual++;
            actual.Should().Be(expected);
        }
    }
}
