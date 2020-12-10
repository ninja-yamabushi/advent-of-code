using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.Shared;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass]
    public class Puzzle2020Fixture
    {
        public enum InputTypes
        {
            Resource, Value
        }

        private int year = 2020;
        private IPuzzle subject;

        [DataRow(9, 999, "2020_09_01_example", "127")]
        [DataRow(9, 1, "2020_09_01", "542529149")]
        [DataRow(9, 998, "2020_09_01_example", "62")]
        [DataRow(9, 2, "2020_09_01", "75678618")]

        [DataRow(8, 1, "2020_08_01_example", "5")]
        [DataRow(8, 1, "2020_08_01", "1384")]
        [DataRow(8, 2, "2020_08_01_example", "8")]
        [DataRow(8, 2, "2020_08_01", "761")]

        [DataRow(7, 1, "2020_07_01_example", "4")]
        [DataRow(7, 1, "2020_07_01", "265")]
        [DataRow(7, 2, "2020_07_01_example", "32")]
        [DataRow(7, 2, "2020_07_02_example", "126")]
        [DataRow(7, 2, "2020_07_01", "14177")]

        [DataRow(6, 1, "2020_06_01_example", "11")]
        [DataRow(6, 1, "2020_06_01", "6878")]
        [DataRow(6, 2, "2020_06_01_example", "6")]
        [DataRow(6, 2, "2020_06_01", "3464")]

        [DataRow(5, 1, "FBFBBFFRLR", "357", InputTypes.Value)]
        [DataRow(5, 1, "BFFFBBFRRR", "567", InputTypes.Value)]
        [DataRow(5, 1, "FFFBBBFRRR", "119", InputTypes.Value)]
        [DataRow(5, 1, "BBFFBBFRLL", "820", InputTypes.Value)]
        [DataRow(5, 1, "2020_05_01_example", "820")]
        [DataRow(5, 1, "2020_05_01", "874")]
        [DataRow(5, 2, "2020_05_01", "594")]

        [DataRow(4, 1, "2020_04_01_example", "2")]
        [DataRow(4, 1, "2020_04_01", "190")]
        [DataRow(4, 2, "2020_04_02_valid", "4")]
        [DataRow(4, 2, "2020_04_02_invalid", "0")]
        [DataRow(4, 2, "2020_04_02_mixed", "4")]
        [DataRow(4, 2, "2020_04_01", "121")]

        [DataRow(3, 1, "2020_03_01_example", "7")]
        [DataRow(3, 1, "2020_03_01", "244")]
        [DataRow(3, 2, "2020_03_01_example", "336")]
        [DataRow(3, 2, "2020_03_01", "9406609920")]

        [DataRow(2, 1, "2020_02_01_example", "2")]
        [DataRow(2, 1, "2020_02_01", "418")]
        [DataRow(2, 2, "2020_02_01_example", "1")]
        [DataRow(2, 2, "2020_02_01", "616")]

        [DataRow(1, 1, "2020_01_01_example", "514579")]
        [DataRow(1, 1, "2020_01_01", "1019904")]
        [DataRow(1, 2, "2020_01_01_example", "241861950")]
        [DataRow(1, 2, "2020_01_01", "176647680")]
        [DataTestMethod]
        public void ShouldSolvePuzzles(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            subject = SolutionProvider.GetPuzzle(year, day, challenge);

            var inputs = (inputType == InputTypes.Resource) ? PuzzleInputs2020.ResourceManager.GetString(input) : input;
            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }


        #region "Day 4"

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

    }
}
