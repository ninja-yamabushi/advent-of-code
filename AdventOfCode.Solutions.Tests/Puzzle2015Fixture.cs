using AdventOfCode.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass]
    public class Puzzle2015Fixture
    {
        private int year = 2015;
        private IPuzzle subject;

        #region "Day 1"

        [DataRow("(())", "0")]
        [DataRow("()()", "0")]
        [DataRow("(((", "3")]
        [DataRow("(()(()(", "3")]
        [DataRow("))(((((", "3")]
        [DataRow("())", "-1")]
        [DataRow("))(", "-1")]
        [DataRow(")))", "-3")]
        [DataRow(")())())", "-3")]
        [DataTestMethod]
        public void PuzzleDay01_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay01_1_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_01_01;
            var expected = "138";

            PuzzleDay01_1_ShouldSolve(inputs, expected);
        }

        [DataRow(")", "1")]
        [DataRow("()())", "5")]
        [DataTestMethod]
        public void PuzzleDay01_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 1, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay01_2_ShouldSolveOfficialInputs()
        {
            var inputs =  PuzzleInputs2015._2015_01_01;
            var expected = "1771";

            PuzzleDay01_2_ShouldSolve(inputs, expected);
        }

        #endregion

        #region "Day 2"

        [DataRow("2x3x4", "58")]
        [DataRow("1x1x10", "43")]
        [DataTestMethod]
        public void PuzzleDay02_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay02_1_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_02_01;
            var expected = "1606483";

            PuzzleDay02_1_ShouldSolve(inputs, expected);
        }

        [DataRow("2x3x4", "34")]
        [DataRow("1x1x10", "14")]
        [DataTestMethod]
        public void PuzzleDay02_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 2, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay02_2_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_02_01;
            var expected = "3842356";

            PuzzleDay02_2_ShouldSolve(inputs, expected);
        }

        #endregion

        #region "Day 3"

        [DataRow(">", "2")]
        [DataRow("^>v<", "4")]
        [DataRow("^v^v^v^v^v", "2")]
        [DataTestMethod]
        public void PuzzleDay03_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay03_1_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_03_01;
            var expected = "2572";

            PuzzleDay03_1_ShouldSolve(inputs, expected);
        }

        [DataRow("^v", "3")]
        [DataRow("^>v<", "3")]
        [DataRow("^v^v^v^v^v", "11")]
        [DataTestMethod]
        public void PuzzleDay03_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 3, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay03_2_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_03_01;
            var expected = "2631";

            PuzzleDay03_2_ShouldSolve(inputs, expected);
        }
        #endregion

        #region "Day 4"

        [DataRow("abcdef", "609043")]
        [DataRow("pqrstuv", "1048970")]
        [DataTestMethod]
        [Ignore("Long running")]
        public void PuzzleDay04_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        [Ignore("Long running")]
        public void PuzzleDay04_1_ShouldSolveOfficialInputs()
        {
            var inputs = "bgvyzdsv";
            var expected = "254575";

            PuzzleDay04_1_ShouldSolve(inputs, expected);
        }

        [DataRow("bgvyzdsv", "1038736")]
        [DataTestMethod]
        [Ignore("Long running")]
        public void PuzzleDay04_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 4, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }
        #endregion

        #region "Day 5"

        [DataRow("ugknbfddgicrmopn", "1")]
        [DataRow("aaa", "1")]
        [DataRow("jchzalrnumimnmhp", "0")]
        [DataRow("haegwjzuvuyypxyu", "0")]
        [DataRow("dvszwmarrgswjxmb", "0")]
        [DataTestMethod]
        public void PuzzleDay05_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay05_1_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_05_01;
            var expected = "236";

            PuzzleDay05_1_ShouldSolve(inputs, expected);
        }

        [DataRow("qjhvhtzxzqqjkmpb", "1")]
        [DataRow("xxyxx", "1")]
        [DataRow("uurcxstgmygtbstg", "0")]
        [DataRow("ieodomkazucvgmuy", "0")]
        [DataTestMethod]
        public void PuzzleDay05_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 5, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay05_2_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_05_01;
            var expected = "51";

            PuzzleDay05_2_ShouldSolve(inputs, expected);
        }
        #endregion

        #region "Day 6"

        [DataRow("turn on 0,0 through 999,999", "1000000")]
        [DataRow("toggle 0,0 through 999,0", "1000")]
        [DataRow("turn off 499,499 through 500,500", "0")]
        [DataRow("turn on 0,0 through 999,999|turn off 499,499 through 500,500", "999996")]
        [DataTestMethod]
        public void PuzzleDay06_1_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 6, 1);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay06_1_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_06_01;
            inputs = inputs.Replace(Environment.NewLine, "|");
            var expected = "377891";

            PuzzleDay06_1_ShouldSolve(inputs, expected);
        }

        [DataRow("turn on 0,0 through 0,0", "1")]
        [DataRow("toggle 0,0 through 999,999", "2000000")]
        [DataTestMethod]
        public void PuzzleDay06_2_ShouldSolve(string inputs, string expected)
        {
            subject = SolutionProvider.GetPuzzle(year, 6, 2);

            var actual = subject.Solve(inputs);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void PuzzleDay06_2_ShouldSolveOfficialInputs()
        {
            var inputs = PuzzleInputs2015._2015_06_01;
            inputs = inputs.Replace(Environment.NewLine, "|");
            var expected = "14110788";

            PuzzleDay06_2_ShouldSolve(inputs, expected);
        }

        #endregion
    }
}
