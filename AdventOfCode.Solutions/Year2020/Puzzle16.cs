using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.AdventOfCode.Solutions.Year2020.Day16;
using AdventOfCode.Solutions.Year2020.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle16_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var sections = inputs.SplitByEmptyLine();
            var rules = TicketRule.Parse(sections[0].SplitByLine());
            var myTicket = Ticket.ParseSingle(sections[1].SplitByLine().Subset(1)[0]);
            var tickets = Ticket.Parse(sections[2].SplitByLine().Subset(1));

            long sum = 0;
            foreach (var ticket in tickets)
            {
                foreach (var number in ticket.Numbers)
                {
                    if (!AtLeastOneRuleIsValid(rules, number))
                    {
                        sum += long.Parse(number);
                        break;
                    }
                }
            }
            return sum.ToString();
        }

        private bool AtLeastOneRuleIsValid(TicketRule[] rules, string number)
        {
            foreach (var rule in rules)
                if (rule.ValidateFor(number))
                    return true;
            return false;
        }
    }

    public class Puzzle16_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var sections = inputs.SplitByEmptyLine();
            var rules = TicketRule.Parse(sections[0].SplitByLine());
            var myTicket = Ticket.ParseSingle(sections[1].SplitByLine().Subset(1)[0]);
            var tickets = Ticket.Parse(sections[2].SplitByLine().Subset(1)).Where(t => t.IsValidFor(rules)).ToArray();

            var possibilities = GetPossibilities(rules, tickets);
            RearangePossibilities(possibilities);

            long result = 0;
            foreach (var pair in possibilities)
            {
                if (pair.Key.Name.StartsWith("departure"))
                {
                    var value = long.Parse(myTicket.Numbers[pair.Value[0]]);
                    result = result == 0 ? 1 * value : result *= value;
                }
            }
            return result.ToString();
        }

        private void RearangePossibilities(Dictionary<TicketRule, List<int>> possibilities)
        {
            bool madeChange = false;

            do
            {
                madeChange = false;
                foreach (var pair in possibilities)
                {
                    if (pair.Value.Count == 1)
                        if (RemovePossibiliy(possibilities, pair.Value[0]))
                            madeChange = true;
                }
            } while (madeChange);
        }
        private bool RemovePossibiliy(Dictionary<TicketRule, List<int>> possibilities, int possibility)
        {
            bool changed = false;
            foreach (var pair in possibilities)
                if (pair.Value.Count > 1 && pair.Value.Contains(possibility))
                {
                    pair.Value.Remove(possibility);
                    changed = true;
                }
            return changed;
        }
        private Dictionary<TicketRule, List<int>> GetPossibilities(TicketRule[] rules, Ticket[] tickets)
        {
            var possibilities = new Dictionary<TicketRule, List<int>>();

            var length = rules.Length;
            for (int ruleIndex = 0; ruleIndex < length; ruleIndex++)
            {
                var rule = rules[ruleIndex];
                var possibleFields = new List<int>();
                for (int fieldIndex = 0; fieldIndex < length; fieldIndex++)
                {
                    var values = tickets.Select(t => t.Numbers[fieldIndex]).ToArray();
                    if (IsValidForAllNumbers(rule, values))
                        possibleFields.Add(fieldIndex);
                }
                possibilities.Add(rule, possibleFields);
            }
            return possibilities;
        }

        private bool IsValidForAllNumbers(TicketRule rule, string[] numbers)
        {
            foreach (var number in numbers)
                if (!rule.ValidateFor(number))
                    return false;
            return true;
        }
    }

    namespace AdventOfCode.Solutions.Year2020.Day16
    {

        public class Ticket
        {
            public string[] Numbers { get; }

            public Ticket(string[] numbers)
            {
                Numbers = numbers;
            }

            public static Ticket[] Parse(string[] lines)
            {
                return lines.Select(line => Ticket.ParseSingle(line)).ToArray();
            }
            public static Ticket ParseSingle(string line)
            {
                return new Ticket(line.SplitOn(","));
            }

            public bool IsValidFor(TicketRule[] rules)
            {
                foreach (var number in Numbers)
                    if (!AtLeastOneRuleIsValid(rules, number))
                        return false;
                return true;
            }
            private bool AtLeastOneRuleIsValid(TicketRule[] rules, string number)
            {
                foreach (var rule in rules)
                    if (rule.ValidateFor(number))
                        return true;
                return false;
            }
        }
        public class TicketRule : IRule
        {
            private readonly IRule rule;

            public string Name { get; }

            public TicketRule(string name, IRule rule)
            {
                Name = name;
                this.rule = rule;
            }
            public bool ValidateFor(string input)
            {
                return rule.ValidateFor(input);
            }

            public static TicketRule[] Parse(string[] lines)
            {
                return lines.Select(line => TicketRule.ParseSingle(line)).ToArray();
            }
            public static TicketRule ParseSingle(string line)
            {
                var nameAndConditions = line.SplitOn(":");

                var conditions = nameAndConditions[1].SplitOn("or").Select(s => s.Trim()).ToArray();
                var numbers1 = conditions[0].SplitOn("-").Select(n => int.Parse(n)).ToArray();
                var numbers2 = conditions[1].SplitOn("-").Select(n => int.Parse(n)).ToArray();
                var rules = new DigitBetweenRule(numbers1[0], numbers1[1]).Or(new DigitBetweenRule(numbers2[0], numbers2[1]));

                return new TicketRule(nameAndConditions[0], rules);
            }
        }
    }
}

