using AdventOfCode.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass]
    public class Puzzle2020Fixture
    {
        private int year = 2020;
        private IPuzzle subject;

        #region "Day 1"

        [TestMethod]
        public void PuzzleDay01_1_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 1);

            var inputs =  PuzzleInputs2020._2020_01_01_example;
            var expected = "514579";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay01_1_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 1);

            var inputs = PuzzleInputs2020._2020_01_01;
            var expected = "1019904";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay01_2_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 2);

            var inputs = PuzzleInputs2020._2020_01_01_example;
            var expected = "241861950";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay01_2_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 2);

            var inputs = PuzzleInputs2020._2020_01_01;
            var expected = "176647680";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        #endregion

    }
}
