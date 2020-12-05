using System;

namespace AdventOfCode.Solutions.Year2020.Shared
{
    public static class RuleExtensions
    {
        public static IRules And(this IRules source, IRules rule)
        {
            return new AndRuleAgregator(new[] { source, rule });
        }

        public static IRules Or(this IRules source, IRules rule)
        {
            return new OrRuleAgregator(new[] { source, rule });
        }

        public static IRules With(this IRules source, Func<string, string> manipulation)
        {
            return new ModifiedInputPassthru(source, manipulation);
        }
    }

    public interface IRules
    {
        bool ValidateFor(string input);
    }

    public class AndRuleAgregator : IRules
    {
        private readonly IRules[] rules;

        public AndRuleAgregator(IRules[] rules)
        {
            this.rules = rules;
        }

        public virtual bool ValidateFor(string input)
        {
            foreach (IRules rule in rules)
                if (!rule.ValidateFor(input))
                    return false;
            return true;
        }
    }

    public class OrRuleAgregator : IRules
    {
        private readonly IRules[] rules;

        public OrRuleAgregator(IRules[] rules)
        {
            this.rules = rules;
        }

        public virtual bool ValidateFor(string input)
        {
            foreach (IRules rule in rules)
                if (rule.ValidateFor(input))
                    return true;
            return false;
        }
    }

    public class ModifiedInputPassthru : IRules
    {
        private readonly IRules rule;
        private readonly Func<string, string> manipulation;

        public ModifiedInputPassthru(IRules rule, Func<string,string> manipulation)
        {
            this.rule = rule;
            this.manipulation = manipulation;
        }

        public bool ValidateFor(string input)
        {
            return rule.ValidateFor(manipulation.Invoke(input));
        }
    }


    public class AlwaysTrueRule : IRules
    {
        public bool ValidateFor(string input)
        {
            return true;
        }
    }

    public class LengthRule : IRules
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
    public class DigitRule : IRules
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsDigit(input[index]))
                    return false;
            return true;
        }
    }
    public class LetterRule : IRules
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsLetter(input[index]))
                    return false;
            return true;
        }
    }
    public class LetterOrDigitRule : IRules
    {
        public bool ValidateFor(string input)
        {
            for (int index = 0; index < input.Length; index++)
                if (!char.IsLetterOrDigit(input[index]))
                    return false;
            return true;
        }
    }
    public class DigitBetweenRule : IRules
    {
        private readonly IRules prerequisites;
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
    }
    public class ChoiceRule : IRules
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
    public class StartsWithRule : IRules
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
    public class EndsWithRule : IRules
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
