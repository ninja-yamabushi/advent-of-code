using System;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020.Shared
{
    public static class RuleExtensions
    {
        public static IRule And(this IRule source, IRule rule)
        {
            return new AndRuleAgregator(new[] { source, rule });
        }

        public static IRule Or(this IRule source, IRule rule)
        {
            return new OrRuleAgregator(new[] { source, rule });
        }

        public static IRule With(this IRule source, Func<string, string> manipulation)
        {
            return new ModifiedInputPassthru(source, manipulation);
        }
    }

    public interface IRule
    {
        bool ValidateFor(string input);
    }

    public class AndRuleAgregator : IRule
    {
        private readonly IRule[] rules;

        public AndRuleAgregator(IRule[] rules)
        {
            this.rules = rules;
        }

        public virtual bool ValidateFor(string input)
        {
            foreach (IRule rule in rules)
                if (!rule.ValidateFor(input))
                    return false;
            return true;
        }

        public override string ToString()
        {
            var text = "";
            foreach (var rule in rules)
            {
                if (!string.IsNullOrEmpty(text))
                    text += " AND ";
                text += rule.ToString();
            }

            return $"(" + text + ")";
        }
    }

    public class OrRuleAgregator : IRule
    {
        private readonly IRule[] rules;

        public OrRuleAgregator(IRule[] rules)
        {
            this.rules = rules;
        }

        public virtual bool ValidateFor(string input)
        {
            foreach (IRule rule in rules)
                if (rule.ValidateFor(input))
                    return true;
            return false;
        }

        public override string ToString()
        {
            var text = "";
            foreach (var rule in rules)
            {
                if (!string.IsNullOrEmpty(text))
                    text += " OR ";
                text += rule.ToString();
            }

            return $"("+text+")";
        }
    }

    public class ModifiedInputPassthru : IRule
    {
        private readonly IRule rule;
        private readonly Func<string, string> manipulation;

        public ModifiedInputPassthru(IRule rule, Func<string,string> manipulation)
        {
            this.rule = rule;
            this.manipulation = manipulation;
        }

        public bool ValidateFor(string input)
        {
            return rule.ValidateFor(manipulation.Invoke(input));
        }
    }


    public class AlwaysTrueRule : IRule
    {
        public bool ValidateFor(string input)
        {
            return true;
        }
    }

    public class LengthRule : IRule
    {
        private readonly int length;

        public LengthRule(int length)
        {
            this.length = length;
        }
        public bool ValidateFor(string input)
        {
            return input.Length == length;
        }
    }
    public class DigitRule : IRule
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsDigit(input[index]))
                    return false;
            return true;
        }
    }
    public class LetterRule : IRule
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsLetter(input[index]))
                    return false;
            return true;
        }
    }
    public class LetterOrDigitRule : IRule
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsLetterOrDigit(input[index]))
                    return false;
            return true;
        }
    }
    public class DigitBetweenRule : IRule
    {
        private readonly IRule prerequisites;
        private readonly int min;
        private readonly int max;

        public DigitBetweenRule(int min, int max)
        {
            prerequisites = new DigitRule();
            this.min = min;
            this.max = max;
        }
        public bool ValidateFor(string input)
        {
            if (!prerequisites.ValidateFor(input))
                return false;

            var value = int.Parse(input);
            return (value >= min && value <= max);
        }

        public override string ToString()
        {
            return $"between {min} and {max}";
        }
    }
    public class ChoiceRule : IRule
    {
        private readonly string[] possibilities;

        public ChoiceRule(string[] possibilities)
        {
            this.possibilities = possibilities;
        }
        public bool ValidateFor(string input)
        {
            foreach (var possibility in possibilities)
                if (input == possibility)
                    return true;
            return false;
        }
    }
    public class StartsWithRule : IRule
    {
        private readonly string prefix;

        public StartsWithRule(string prefix)
        {
            this.prefix = prefix;
        }
        public bool ValidateFor(string input)
        {
            return input.StartsWith(prefix);
        }
    }
    public class EndsWithRule : IRule
    {
        private readonly string suffix;

        public EndsWithRule(string suffix)
        {
            this.suffix = suffix;
        }
        public bool ValidateFor(string input)
        {
            return input.EndsWith(suffix);
        }
    }

}
