using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle1_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var values = inputs.GetArrayOfInt();
            for (int number1 = 0; number1 < values.Length; number1++)
                for (int number2 = number1 + 1; number2 < values.Length; number2++)
                {
                    if (values[number1] + values[number2] == 2020)
                        return (values[number1] * values[number2]).ToString();
                }

            return "NO RESULTS";
        }
    }

    public class Puzzle1_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var values = inputs.GetArrayOfInt();
            for (int number1 = 0; number1 < values.Length; number1++)
                for (int number2 = number1 + 1; number2 < values.Length; number2++)
                    for (int number3 = number2 + 1; number3 < values.Length; number3++)
                    {
                        if (values[number1] + values[number2] + values[number3] == 2020)
                            return (values[number1] * values[number2] * values[number3]).ToString();
                    }

            return "NO RESULTS";
        }
    }
}