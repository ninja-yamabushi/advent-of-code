namespace AdventOfCode.Solutions.Year2015.Shared
{
    public interface IRule
    {
        bool RespectedBy(string input);
    }


    public class RuleAgregator : IRule
    {
        private readonly IRule[] rules;

        public RuleAgregator(IRule[] rules)
        {
            this.rules = rules;
        }

        public bool RespectedBy(string input)
        {
            foreach (IRule rule in rules)
                if (!rule.RespectedBy(input))
                    return false;
            return true;
        }
    }

}