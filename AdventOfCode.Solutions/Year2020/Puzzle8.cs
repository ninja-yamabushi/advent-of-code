using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle8_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var instructions = Instruction.Parse(inputs);
            var bootstrap = new Bootstrapper(instructions);
            bootstrap.Run();

            return bootstrap.Accumulator.ToString();
        }
    }
    public class Puzzle8_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var instructions = Instruction.Parse(inputs);
            var bootstrap = new Bootstrapper(instructions);
            bootstrap.SimulateFix();

            return bootstrap.Accumulator.ToString();
        }
    }

    public interface IBootstrapper
    {
        void IncreaseAccumulator(int value);
        void CalculateInstructionIndex(int value);
    }

    public class Bootstrapper : IBootstrapper
    {
        private readonly Instruction[] originalInstructions;
        private Instruction[] instructions;
        private bool bootCompleted;
        private int changeIndex;
        private int index;

        public int Accumulator { get; internal set; }

        public Bootstrapper(Instruction[] instructions)
        {
            originalInstructions = instructions;
            this.instructions = originalInstructions.Copy();
        }

        public void IncreaseAccumulator(int value)
        {
            Accumulator += value;
        }

        public void CalculateInstructionIndex(int value)
        {
            index += value;
        }

        public void Run()
        {
            Accumulator = 0;
            bootCompleted = false;

            index = 0;
            var instruction = instructions[index];

            while (!instruction.Ran)
            {
                instruction.ExecuteOn(this);

                if (index >= instructions.Length)
                {
                    bootCompleted = true;
                    return;
                }
                instruction = instructions[index];
            }
        }
        public void SimulateFix()
        {
            changeIndex = 0;

            Run();
            while (!bootCompleted)
            {
                instructions = originalInstructions.Copy();
                Instruction toChange = GetInstructionToChange();
                if (toChange == null)
                    return;
                toChange.Switch();

                Run();
            }
        }

        private Instruction GetInstructionToChange()
        {
            while (changeIndex < instructions.Length)
            {
                var instruction = instructions[changeIndex];
                changeIndex++;

                if (instruction.CanChange)
                    return instruction;
            }
            return null;
        }
    }

    public class Instruction
    {
        public Instruction(string command, int value)
        {
            Command = command;
            Value = value;
            Ran = false;
        }

        public string Command { get; internal set; }
        public int Value { get; }
        public bool Ran { get; private set; }

        public bool CanChange
        {
            get { return Command != "acc"; }
        }

        public void ExecuteOn(IBootstrapper bootstrapper)
        {
            switch (Command)
            {
                case "nop":
                    bootstrapper.CalculateInstructionIndex(1);
                    break;
                case "acc":
                    bootstrapper.CalculateInstructionIndex(1);
                    bootstrapper.IncreaseAccumulator(Value);
                    break;
                case "jmp":
                    bootstrapper.CalculateInstructionIndex(Value);
                    break;
            }
            Ran = true;
        }
        public void Reset()
        {
            Ran = false;
        }

        public void Switch()
        {
            switch (Command)
            {
                case "jmp": Command = "nop"; break;
                case "nop": Command = "jmp"; break;
            }
        }

        public override string ToString()
        {
            return $"{Command}:{Value}, Ran:{Ran}";
        }

        public static Instruction[] Parse(string inputs)
        {
            var instructions = inputs.SplitByLine();
            return instructions.Select(s => ParseSingle(s)).ToArray();
        }
        public static Instruction ParseSingle(string input)
        {
            var cmd = input.SplitOn(" ");
            return new Instruction(cmd[0], int.Parse(cmd[1]));
        }
    }

    public static class InstructionExtensions
    {
        public static Instruction Copy(this Instruction source)
        {
            return new Instruction(source.Command, source.Value);
        }
        public static Instruction[] Copy(this Instruction[] source)
        {
            return source.Select(i => i.Copy()).ToArray();
        }
    }
}

