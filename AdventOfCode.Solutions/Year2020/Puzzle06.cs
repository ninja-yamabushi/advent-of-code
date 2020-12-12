using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle6_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var forms = DeclarationForm.Parse(inputs);

            return forms.Sum(f => f.GetNumberOfYesQuestionsForAnyone()).ToString();
        }
    }
    public class Puzzle6_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var forms = DeclarationForm.Parse(inputs);

            return forms.Sum(f => f.GetNumberOfYesQuestionsForEveryone()).ToString();
        }
    }

    public class DeclarationForm : Dictionary<char, int>
    {
        private readonly int answerers;

        public DeclarationForm(string[] persons)
        {
            answerers = persons.Length;
            for (char letter = 'a'; letter <= 'z'; letter++)
                Add(letter, 0);

            foreach (var person in persons)
                foreach (var answer in person)
                    AnsweredYesTo(answer);
        }
        public void AnsweredYesTo(char question)
        {
            this[question] += 1;
        }

        public int GetNumberOfYesQuestionsForAnyone()
        {
            return Values.Count(v => v > 0);
        }

        public int GetNumberOfYesQuestionsForEveryone()
        {
            return Values.Count(v => v == answerers);
        }


        public static DeclarationForm[] Parse(string inputs)
        {
            var groups = inputs.SplitByEmptyLine();
            return groups.Select(s => ParseSingle(s)).ToArray();
        }
        public static DeclarationForm ParseSingle(string input)
        {
            return new DeclarationForm(input.SplitByLine());
        }
    }
}
