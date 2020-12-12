using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    public class Puzzle9_999 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var preamble = new Preamble(5, inputs.GetArrayOfLong());
            var validator = new Validator();

            return validator.FindFirstUnmatch(preamble).ToString();
        }
    }

    public class Puzzle9_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var preamble = new Preamble(25, inputs.GetArrayOfLong());
            var validator = new Validator();

            return validator.FindFirstUnmatch(preamble).ToString();
        }
    }

    public class Puzzle9_998 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var preamble = new Preamble(5, inputs.GetArrayOfLong());
            var validator = new Validator();
            var number = validator.FindFirstUnmatch(preamble);

            return preamble.FindMinMaxSumOfSequenceFor(number).ToString();
        }
    }

    public class Puzzle9_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var preamble = new Preamble(25, inputs.GetArrayOfLong());
            var validator = new Validator();
            var number = validator.FindFirstUnmatch(preamble);

            return preamble.FindMinMaxSumOfSequenceFor(number).ToString();
        }
    }





    public class NumberCruncher
    {
        public bool FindSumOfFrom(long sum, long[] from)
        {
            for (int number1 = 0; number1 < from.Length; number1++)
                for (int number2 = number1 + 1; number2 < from.Length; number2++)
                {
                    if (from[number1] + from[number2] == sum)
                        return true;
                }

            return false;
        }
    }

    public class Validator
    {
        public long FindFirstUnmatch(Preamble preamble)
        {
            var cruncher = new NumberCruncher();

            while (preamble.Peek != long.MinValue)
            {
                if (!cruncher.FindSumOfFrom(preamble.CurrentNumber, preamble.CurrentSubset))
                    return preamble.CurrentNumber;

                preamble.MoveNext();
            }

            return long.MinValue;
        }
    }

    public class Preamble
    {
        private readonly int size;
        private readonly long[] numbers;

        private int index;

        public Preamble(int size, long[] numbers)
        {
            this.size = size;
            this.numbers = numbers;
            index = 0;
        }

        public long[] Numbers => numbers;

        public long[] CurrentSubset
        {
            get { return numbers.Subset(index, size); }
        }

        public long CurrentNumber
        {
            get { return numbers[index + size]; }
        }
        private int NumberIndex
        {
            get { return index + size; }
        }

        public long Peek
        {
            get
            {
                if (NumberIndex >= numbers.Length)
                    return long.MinValue;
                return numbers[NumberIndex];
            }
        }

        public void MoveNext()
        {
            index++;
        }

        public long FindMinMaxSumOfSequenceFor(long value)
        {
            int startIndex = 0;
            while (startIndex < numbers.Length)
            {
                long sum = numbers[startIndex];
                for (int index = startIndex+1; index < numbers.Length; index++)
                {
                    sum += numbers[index];
                    if (sum == value)
                    {
                        var result = numbers.Subset(startIndex, (index - startIndex)+1);
                        return result.Min() + result.Max();
                    }
                    if (sum > value)
                        break;

                }
                startIndex++;
            }
            return long.MinValue;
        }
    }
}

