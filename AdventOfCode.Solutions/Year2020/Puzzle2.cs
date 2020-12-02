using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle2_1 : IPuzzle
    {
        private readonly int _policyVersion;

        public Puzzle2_1() : this(1) { }
        public Puzzle2_1(int policyVersion)
        {
            _policyVersion = policyVersion;
        }

        public string Solve(string inputs)
        {
            var count = 0;
            var lines = inputs.SplitByLine();

            foreach (var line in lines)
            {
                var values = line.SplitOn(":");
                var policy = PasswordPolicy.Parse(values[0], _policyVersion);
                var password = values[1].Trim();

                if (policy.Validate(password))
                    count++;
            }

            return count.ToString();
        }
    }

    public class Puzzle2_2 : Puzzle2_1
    {
        public Puzzle2_2() : base(2)
        { }
    }

    internal class PasswordPolicy
    {
        private readonly char letter;
        private readonly int min;
        private readonly int max;
        private readonly int version;

        public PasswordPolicy(char letter, int min, int max, int version)
        {
            this.letter = letter;
            this.min = min;
            this.max = max;
            this.version = version;
        }

        public bool Validate(string password)
        {
            if (version == 2)
                return (password[min - 1] == letter ^ password[max - 1] == letter);

            int count = 0;
            foreach (char l in password)
                if (l == letter)
                    count++;
            return (count >= min && count <= max);
        }

        public static PasswordPolicy Parse(string line, int policyVersion)
        {
            var values = line.SplitOn(" ");
            var l = values[1].ToCharArray().Last();

            values = values[0].SplitOn("-");
            var min = int.Parse(values[0]);
            var max = int.Parse(values[1]);

            return new PasswordPolicy(l, min, max, policyVersion);
        }
    }
}
