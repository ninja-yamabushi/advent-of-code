using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020;
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

        [DataRow(12, 1, "2020_12_01_example", "25")]
        [DataRow(12, 1, "2020_12_01", "381")]
        [DataRow(12, 2, "2020_12_01_example", "286")]
        [DataRow(12, 2, "2020_12_01", "28591")]

        [DataRow(11, 1, "2020_11_01_example", "37")]
        [DataRow(11, 1, "2020_11_01", "2263")]
        [DataRow(11, 2, "2020_11_01_example", "26")]
        [DataRow(11, 2, "2020_11_01", "2002")]

        [DataRow(10, 1, "2020_10_01_example1", "35")]
        [DataRow(10, 1, "2020_10_01_example2", "220")]
        [DataRow(10, 1, "2020_10_01", "2176")]
        //[DataRow(10, 2, "2020_10_01_example1", "8")]
        //[DataRow(10, 2, "2020_10_01_example2", "19208")]


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

        [DataRow(10, 2, "2020_10_01", "-1")]
        [DataTestMethod]
        [Ignore("Long running")]
        public void ShouldSolveLongRunningPuzzles(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            ShouldSolvePuzzles(day, challenge, input, expected, inputType);
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

        #region "Day 11"

        [TestMethod]
        public void PuzzleDay11_2_ShouldFindEightAdjascentSeats()
        {
            string inputs = @".......#.
...#.....
.#.......
.........
..#L....#
....#....
.........
#........
...#.....";

            var area = new WaitingArea(inputs);
            var place = area[3, 4];

            Assert.IsTrue(place.IsSeat);
            Assert.IsTrue(place.IsEmpty);

            var adj = place.FindAdjacentOrVisibleSeats();

            Assert.AreEqual(8, adj.Length);

        }
        [TestMethod]
        public void PuzzleDay11_2_ShouldFindOneAdjascentSeats()
        {
            string inputs = @".............
.L.L.#.#.#.#.
.............";

            var area = new WaitingArea(inputs);
            var place = area[1, 1];

            Assert.IsTrue(place.IsSeat);
            Assert.IsTrue(place.IsEmpty);

            var adj = place.FindAdjacentOrVisibleSeats();

            Assert.AreEqual(1, adj.Length);

        }
        [TestMethod]
        public void PuzzleDay11_2_ShouldFindNoEmptyAdjascentSeats()
        {
            string inputs = @".##.##.
#.#.#.#
##...##
...L...
##...##
#.#.#.#
.##.##.";

            var area = new WaitingArea(inputs);
            var place = area[3, 3];

            Assert.IsTrue(place.IsSeat);
            Assert.IsTrue(place.IsEmpty);

            var adj = place.FindAdjacentOrVisibleSeats();

            Assert.AreEqual(0, adj.Length);

        }
        #endregion

        #region "Day 12"

        //move, turn, keep going

        [TestMethod]
        public void PuzzleDay12_1_ShouldParseSingleAction()
        {
            string input = @"F10";

            var action = NavigationAction.ParseSingle(input);

            Assert.AreEqual("F : 10", action.ToString());
        }
        [TestMethod]
        public void PuzzleDay12_1_ShouldParseActions()
        {
            string inputs = @"F10
N3
F7
R90
F11";

            var actions = NavigationAction.Parse(inputs);

            Assert.AreEqual(5, actions.Length);
            Assert.AreEqual("F : 10", actions[0].ToString());
            Assert.AreEqual("N : 3", actions[1].ToString());
            Assert.AreEqual("F : 7", actions[2].ToString());
            Assert.AreEqual("R : 90", actions[3].ToString());
            Assert.AreEqual("F : 11", actions[4].ToString());
        }

        [TestMethod]
        public void PuzzleDay12_1_ShouldParseDirection()
        {
            var direction = Direction.Parse("N");

            Assert.IsTrue(direction.Is(Direction.North));
        }

        [TestMethod]
        public void PuzzleDay12_1_ShouldMoveDirection()
        {
            Assert.IsTrue(Direction.North.RotateRightBy(0).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateRightBy(90).Is(Direction.East));
            Assert.IsTrue(Direction.North.RotateRightBy(180).Is(Direction.South));
            Assert.IsTrue(Direction.North.RotateRightBy(360).Is(Direction.West));
            Assert.IsTrue(Direction.North.RotateRightBy(450).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateRightBy(540).Is(Direction.East));

            Assert.IsTrue(Direction.North.RotateLeftBy(0).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateLeftBy(90).Is(Direction.West));
            Assert.IsTrue(Direction.North.RotateLeftBy(180).Is(Direction.South));
            Assert.IsTrue(Direction.North.RotateLeftBy(360).Is(Direction.East));
            Assert.IsTrue(Direction.North.RotateLeftBy(450).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateLeftBy(540).Is(Direction.West));
        }

        #endregion
    }
}
