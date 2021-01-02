using AdventOfCode.Solutions.Year2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Solutions.Tests
{
    [TestClass] //Day 6 to 12
    public class Puzzle2020_Week2_Fixture : BasePuzzle2020Fixture
    {
        //Day 12
        [DataRow(12, 1, "2020_12_01_example", "25")]
        [DataRow(12, 1, "2020_12_01", "381")]
        [DataRow(12, 2, "2020_12_01_example", "286")]
        [DataRow(12, 2, "2020_12_01", "28591")]
        //Day 11
        [DataRow(11, 1, "2020_11_01_example", "37")]
        [DataRow(11, 1, "2020_11_01", "2263")]
        [DataRow(11, 2, "2020_11_01_example", "26")]
        [DataRow(11, 2, "2020_11_01", "2002")]
        //Day 10
        [DataRow(10, 1, "2020_10_01_example1", "35")]
        [DataRow(10, 1, "2020_10_01_example2", "220")]
        [DataRow(10, 1, "2020_10_01", "2176")]
        //[DataRow(10, 2, "2020_10_01_example1", "8")]
        //[DataRow(10, 2, "2020_10_01_example2", "19208")]
        //[DataRow(10, 2, "2020_10_01", "-1")]
        //Day 9
        [DataRow(9, 999, "2020_09_01_example", "127")]
        [DataRow(9, 1, "2020_09_01", "542529149")]
        [DataRow(9, 998, "2020_09_01_example", "62")]
        [DataRow(9, 2, "2020_09_01", "75678618")]
        //Day 8
        [DataRow(8, 1, "2020_08_01_example", "5")]
        [DataRow(8, 1, "2020_08_01", "1384")]
        [DataRow(8, 2, "2020_08_01_example", "8")]
        [DataRow(8, 2, "2020_08_01", "761")]
        //Day 7
        [DataRow(7, 1, "2020_07_01_example", "4")]
        [DataRow(7, 1, "2020_07_01", "265")]
        [DataRow(7, 2, "2020_07_01_example", "32")]
        [DataRow(7, 2, "2020_07_02_example", "126")]
        [DataRow(7, 2, "2020_07_01", "14177")]
        //Day 6
        [DataRow(6, 1, "2020_06_01_example", "11")]
        [DataRow(6, 1, "2020_06_01", "6878")]
        [DataRow(6, 2, "2020_06_01_example", "6")]
        [DataRow(6, 2, "2020_06_01", "3464")]
        public void AllPuzzlesShouldSolve(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            solve(day, challenge, input, expected, inputType);
        }

        //Day10
        [DataRow(10, 2, "2020_10_01", "-1")]
        [DataTestMethod]
        [Ignore("Long running")]
        public void AllPuzzlesShouldSolveLongRunning(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            solve(day, challenge, input, expected, inputType);
        }
    }

    [TestClass]
    public class Puzzle2020_Week2_Day11_Fixture
    {
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
    }

    [TestClass]
    public class Puzzle2020_Week2_Day12_Fixture
    {
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
            Assert.IsTrue(Direction.North.RotateRightBy(270).Is(Direction.West));
            Assert.IsTrue(Direction.North.RotateRightBy(360).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateRightBy(450).Is(Direction.East));

            Assert.IsTrue(Direction.North.RotateLeftBy(0).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateLeftBy(90).Is(Direction.West));
            Assert.IsTrue(Direction.North.RotateLeftBy(180).Is(Direction.South));
            Assert.IsTrue(Direction.North.RotateLeftBy(270).Is(Direction.East));
            Assert.IsTrue(Direction.North.RotateLeftBy(360).Is(Direction.North));
            Assert.IsTrue(Direction.North.RotateLeftBy(450).Is(Direction.West));
        }
    }
}