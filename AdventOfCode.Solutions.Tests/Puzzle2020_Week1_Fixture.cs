using AdventOfCode.Solutions.Year2020.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass] //Day 1 to 5
    public class Puzzle2020_Week1_Fixture : BasePuzzle2020Fixture
    {
        //Day 5
        [DataRow(5, 1, "FBFBBFFRLR", "357", InputTypes.Value)]
        [DataRow(5, 1, "BFFFBBFRRR", "567", InputTypes.Value)]
        [DataRow(5, 1, "FFFBBBFRRR", "119", InputTypes.Value)]
        [DataRow(5, 1, "BBFFBBFRLL", "820", InputTypes.Value)]
        [DataRow(5, 1, "2020_05_01_example", "820")]
        [DataRow(5, 1, "2020_05_01", "874")]
        [DataRow(5, 2, "2020_05_01", "594")]
        //Day 4
        [DataRow(4, 1, "2020_04_01_example", "2")]
        [DataRow(4, 1, "2020_04_01", "190")]
        [DataRow(4, 2, "2020_04_02_valid", "4")]
        [DataRow(4, 2, "2020_04_02_invalid", "0")]
        [DataRow(4, 2, "2020_04_02_mixed", "4")]
        [DataRow(4, 2, "2020_04_01", "121")]
        //Day 3
        [DataRow(3, 1, "2020_03_01_example", "7")]
        [DataRow(3, 1, "2020_03_01", "244")]
        [DataRow(3, 2, "2020_03_01_example", "336")]
        [DataRow(3, 2, "2020_03_01", "9406609920")]
        //Day 2
        [DataRow(2, 1, "2020_02_01_example", "2")]
        [DataRow(2, 1, "2020_02_01", "418")]
        [DataRow(2, 2, "2020_02_01_example", "1")]
        [DataRow(2, 2, "2020_02_01", "616")]
        //Day 1
        [DataRow(1, 1, "2020_01_01_example", "514579")]
        [DataRow(1, 1, "2020_01_01", "1019904")]
        [DataRow(1, 2, "2020_01_01_example", "241861950")]
        [DataRow(1, 2, "2020_01_01", "176647680")]
        [DataTestMethod]
        public void AllPuzzlesShouldSolve(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            solve(day, challenge, input, expected, inputType);
        }
    }

    [TestClass]
    public class Puzzle2020_Week1_Day04_Fixture
    {
        [TestMethod]
        public void PuzzleDay04_2_ComplexRuleShouldWork()
        {
            IRule r1 = new EndsWithRule("cm").And(new DigitBetweenRule(150, 193).With(input => input.Substring(0, input.Length - 2)));
            Assert.IsTrue(r1.ValidateFor("160cm"));

            IRule r2 = new EndsWithRule("in").And(new DigitBetweenRule(59, 76).With(input => input.Substring(0, input.Length - 2)));
            Assert.IsTrue(r2.ValidateFor("60in"));

            IRule r3 = r1.Or(r2);
            Assert.IsTrue(r3.ValidateFor("160cm"));
            Assert.IsTrue(r3.ValidateFor("60in"));

            IRule r4 = new EndsWithRule("cm").And(new DigitBetweenRule(150, 193).With(input => input.Substring(0, input.Length - 2)))
                .Or(new EndsWithRule("in").And(new DigitBetweenRule(59, 76).With(input => input.Substring(0, input.Length - 2))));
            Assert.IsTrue(r4.ValidateFor("160cm"));
            Assert.IsTrue(r4.ValidateFor("60in"));
        }
    }
}
