using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2015.Shared;
using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2015
{
    public class Puzzle5_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var rules = new RuleAgregator(
                new IRule[] {
                    new Rule_ContainsAtLeastThreeVowels(),
                    new Rule_DoesNotContainNaughtyLetters(),
                    new Rule_ContainsDoubleLetter()
                }); ;

            int niceCounter = 0;

            string[] strings = inputs.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in strings)
            {
                if (rules.RespectedBy(s))
                    niceCounter++;
            }

            return niceCounter.ToString();
        }

        private class Rule_ContainsAtLeastThreeVowels : IRule
        {
            private char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            public bool RespectedBy(string input)
            {
                int count = 0;
                for (int index = 0; index < input.Length; index++)
                    if (vowels.Contains(input[index]))
                        count++;
                return count >= 3;
            }
        }

        private class Rule_DoesNotContainNaughtyLetters : IRule
        {
            private string[] naughty = new string[] { "ab", "cd", "pq", "xy" };

            public bool RespectedBy(string input)
            {
                foreach (var n in naughty)
                    if (input.Contains(n))
                        return false;
                return true;
            }
        }

        private class Rule_ContainsDoubleLetter : IRule
        {
            public bool RespectedBy(string input)
            {
                for (int index = 0; index < input.Length - 1; index++)
                    if (input[index] == input[index + 1])
                        return true;
                return false;
            }
        }
    }

    public class Puzzle5_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var rules = new RuleAgregator(
                new IRule[] {
                    new Rule_TwoPairOfLettersNotOverlapping(),
                    new Rule_ContainsSameLettreSeparatedByAnother()
                }); ;

            int niceCounter = 0;

            string[] strings = inputs.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in strings)
            {
                if (rules.RespectedBy(s))
                    niceCounter++;
            }

            return niceCounter.ToString();
        }

        private class Rule_TwoPairOfLettersNotOverlapping : IRule
        {
            public bool RespectedBy(string input)
            {
                for (int index = 0; index < input.Length - 1; index++)
                {
                    var dl = $"{input[index]}{input[index + 1]}";
                    if (input.LastIndexOf(dl) > index + 1)
                        return true;
                }
                return false;
            }
        }
        private class Rule_ContainsSameLettreSeparatedByAnother : IRule
        {
            public bool RespectedBy(string input)
            {
                for (int index = 0; index < input.Length; index++)
                {
                    if (input.IndexOf(input[index], index + 1) == index + 2)
                        return true;
                }
                return false;
            }
        }
    }
}