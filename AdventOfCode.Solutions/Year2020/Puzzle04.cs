using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using AdventOfCode.Solutions.Year2020.Shared;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle4_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var count = 0;

            var passports = Passport.ParseMany(inputs);
            foreach (var id in passports)
                if (id.ContainsRequiredFields) 
                    count++;

            return count.ToString();
        }
    }
    public class Puzzle4_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var count = 0;

            var passports = Passport.ParseMany(inputs);
            foreach (var id in passports)
                if (id.Validate())
                    count++;

            return count.ToString();
        }
    }

    internal class Passport
    {
        public Passport(FieldValuePair[] fields)
        {
            Fields = fields;
            ContainsRequiredFields = (fields.Count(f => f.Field.Required == true) == 7);
        }

        public bool ContainsRequiredFields { get; }

        public FieldValuePair[] Fields { get; }

        public bool Validate()
        {
            if (!ContainsRequiredFields)
                return false;

            foreach (FieldValuePair id in Fields)
                if (!id.Validate())
                    return false;
            return true;
        }

        public static Passport[] ParseMany(string inputs)
        {
            var result = new List<Passport>();

            var values = inputs.SplitByEmptyLine();
            foreach (var value in values)
                result.Add(Passport.Parse(value));

            return result.ToArray();
        }
        public static Passport Parse(string input)
        {
            return new Passport(PassportField.ParseMany(input.Replace(Environment.NewLine, " ")));
        }
    }

    internal class FieldValuePair
    {
        public FieldValuePair(PassportField field, string value)
        {
            Field = field;
            Value = value;
        }

        public PassportField Field { get; }

        public string Value { get; }

        public bool Validate()
        {
            return Field.ValidateFor(Value);
        }

        public override string ToString()
        {
            return $"{Field.Name}:{Value}:{Validate()}";
        }
    }


    internal class PassportField
    {
        public static PassportField CountryId = new PassportField("cid");
        public static PassportField BirthYear = new PassportField("byr", new LengthRule(4).And(new DigitBetweenRule(1920, 2002)));
        public static PassportField IssueYear = new PassportField("iyr", new LengthRule(4).And(new DigitBetweenRule(2010, 2020)));
        public static PassportField ExpirationYear = new PassportField("eyr", new LengthRule(4).And(new DigitBetweenRule(2020, 2030)));
        public static PassportField Height = new PassportField("hgt", new EndsWithRule("cm").And(new DigitBetweenRule(150, 193).With(input => input.Substring(0, input.Length - 2))).Or(new EndsWithRule("in").And(new DigitBetweenRule(59, 76).With(input => input.Substring(0, input.Length - 2)))));
        public static PassportField HairColor = new PassportField("hcl", new LengthRule(7).And(new StartsWithRule("#").And(new LetterOrDigitRule().With(input => input.Substring(1)))));
        public static PassportField EyeColor = new PassportField("ecl", new ChoiceRule(new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }));
        public static PassportField PassportId = new PassportField("pid", new LengthRule(9).And(new DigitRule()));

        public static PassportField[] All = { CountryId, BirthYear, IssueYear, ExpirationYear, Height, HairColor, EyeColor, PassportId };


        #region " Implementation "

        public string Name { get; }
        private readonly IRule rules;

        public bool Required { get; }

        private PassportField(string name, IRule rules)
        {
            this.Name = name;
            this.rules = rules;
            Required = true;
        }
        private PassportField(string name)
        {
            this.Name = name;
            this.rules = new AlwaysTrueRule();
            Required = false;
        }

        public bool ValidateFor(string input)
        {
            return rules.ValidateFor(input);
        }

        public static FieldValuePair[] ParseMany(string input)
        {
            var result = new List<FieldValuePair>();
            var values = input.SplitOn(" ");

            foreach (var value in values)
                result.Add(Parse(value));

            return result.ToArray();
        }
        public static FieldValuePair Parse(string input)
        {
            var values = input.SplitOn(":");
            foreach (var field in All)
                if (values[0] == field.Name)
                    return new FieldValuePair(field, values[1]);

            throw new ArgumentException();
        }

        #endregion
    }    
}
