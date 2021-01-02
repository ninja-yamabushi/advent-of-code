using AdventOfCode.Common;
using FluentAssertions;
using System.Resources;

namespace AdventOfCode.Solutions.Tests
{
    public class BasePuzzleFixture
    {
        internal void solve(int year, int day, int challenge, string input, string expected, ResourceManager rm = null)
        {
            IPuzzle subject = SolutionProvider.GetPuzzle(year, day, challenge);

            var inputs = (rm != null) ? rm.GetString(input) : input;
            var actual = subject.Solve(inputs);

            actual.Should().Be(expected);
        }
    }
}
