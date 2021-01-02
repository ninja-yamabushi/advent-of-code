using System.Resources;

namespace AdventOfCode.Solutions.Tests
{

    public class BasePuzzle2020Fixture : BasePuzzleFixture
    {
        public enum InputTypes
        {
            Resource, Value
        }

        internal void solve(int day, int challenge, string input, string expected, InputTypes inputType = InputTypes.Resource)
        {
            const int year = 2020;
            ResourceManager rm = (inputType == InputTypes.Resource) ? PuzzleInputs2020.ResourceManager : null;

            solve(year, day, challenge, input, expected, rm);
        }
    }
}
