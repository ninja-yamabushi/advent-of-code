using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.Shared;
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

        #region "Day 4"

        [TestMethod]
        public void PuzzleDay04_1_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 1);

            var inputs = PuzzleInputs2020._2020_04_01_example;
            var expected = "2";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_1_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 1);

            var inputs = PuzzleInputs2020._2020_04_01;
            var expected = "190";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_2_ShouldSolveValidExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 2);

            var inputs = PuzzleInputs2020._2020_04_02_valid;
            var expected = "4";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_2_ShouldSolveInvalidExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 2);

            var inputs = PuzzleInputs2020._2020_04_02_invalid;
            var expected = "0";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_2_ShouldSolveMixedExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 2);

            var inputs = PuzzleInputs2020._2020_04_02_mixed;
            var expected = "4";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_2_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 2);

            var inputs = PuzzleInputs2020._2020_04_01;
            var expected = "121";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay04_2_ComplexRuleShouldWork()
        {
            IRules r1 = new EndsWithRule("cm").And(new DigitBetweenRule(150, 193).With(input => input.Substring(0, input.Length - 2)));
            Assert.IsTrue(r1.ValidateFor("160cm"));
 
            IRules r2 = new EndsWithRule("in").And(new DigitBetweenRule(59, 76).With(input => input.Substring(0, input.Length - 2)));
            Assert.IsTrue(r2.ValidateFor("60in"));

            IRules r3 = r1.Or(r2);
            Assert.IsTrue(r3.ValidateFor("160cm"));
            Assert.IsTrue(r3.ValidateFor("60in"));

            IRules r4 = new EndsWithRule("cm").And(new DigitBetweenRule(150, 193).With(input => input.Substring(0, input.Length - 2)))
                .Or(new EndsWithRule("in").And(new DigitBetweenRule(59, 76).With(input => input.Substring(0, input.Length - 2))));
            Assert.IsTrue(r4.ValidateFor("160cm"));
            Assert.IsTrue(r4.ValidateFor("60in"));
        }

        #endregion

        #region "Day 5"

        [DataRow("FBFBBFFRLR", "357")]
        [DataRow("BFFFBBFRRR", "567")]
        [DataRow("FFFBBBFRRR", "119")]
        [DataRow("BBFFBBFRLL", "820")]
        [DataTestMethod]
        public void PuzzleDay05_1_ShouldSolveDataInputs(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay05_1_ShouldSolveExampleInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 1);

            var inputs = PuzzleInputs2020._2020_05_01_example;
            var expected = "820";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void PuzzleDay05_1_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 1);

            var inputs = PuzzleInputs2020._2020_05_01;
            var expected = "874";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay05_2_ShouldSolveOfficialInputs()
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 2);

            var inputs = PuzzleInputs2020._2020_05_01;
            var expected = "594";

            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }
        #endregion
    }
}
