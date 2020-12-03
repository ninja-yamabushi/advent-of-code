using AdventOfCode.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var inputs = PuzzleInputs2020._2020_01_01_example;
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

        #region "Day 2"

        [TestMethod]
        public void PuzzleDay02_1_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 1);

            var inputs = PuzzleInputs2020._2020_02_01_example;
            var expected = "2";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay02_1_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 1);

            var inputs = PuzzleInputs2020._2020_02_01;
            var expected = "418";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay02_2_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 2);

            var inputs = PuzzleInputs2020._2020_02_01_example;
            var expected = "1";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay02_2_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 2);

            var inputs = PuzzleInputs2020._2020_02_01;
            var expected = "616";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        #endregion

        #region "Day 3"

        [TestMethod]
        public void PuzzleDay03_1_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 1);

            var inputs = PuzzleInputs2020._2020_03_01_example;
            var expected = "7";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay03_1_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 1);

            var inputs = PuzzleInputs2020._2020_03_01;
            var expected = "244";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay03_2_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 2);

            var inputs = PuzzleInputs2020._2020_03_01_example;
            var expected = "336";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay03_2_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 2);

            var inputs = PuzzleInputs2020._2020_03_01;
            var expected = "9406609920";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }
        #endregion
    }
}
