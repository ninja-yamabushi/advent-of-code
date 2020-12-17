using AdventOfCode.Common;
using AdventOfCode.Solutions.Year2020.Day14;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle14_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var lines = inputs.SplitByLine();

            var memory = new Memory();
            var currentMask = Mask.Empty;
            foreach (var line in lines)
            {
                if (InputLine.IsMask(line))
                    currentMask = InputLine.ParseMask(line);
                else
                {
                    var value = InputLine.ParseValue(line);
                    var address = InputLine.ParseAddress(line);
                    memory[address.Value] = currentMask.ApplyOn(value);
                }
            }

            return memory.Sum().ToString();
        }
    }

    public class Puzzle14_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var lines = inputs.SplitByLine();

            var memory = new Memory();
            var currentMask = Mask.Empty;
            foreach (var line in lines)
            {
                if (InputLine.IsMask(line))
                    currentMask = InputLine.ParseMask(line);
                else
                {
                    var value = InputLine.ParseValue(line);
                    var address = InputLine.ParseAddress(line);
                    var addresses = currentMask.ApplyOn(address);
                    foreach (var adr in addresses)
                        memory[adr.Value] = value;
                }
            }

            return memory.Sum().ToString();
        }
    }
}

namespace AdventOfCode.Solutions.Year2020.Day14
{
    public class InputLine
    {
        public static bool IsMask(string line) => line.StartsWith("mask");
        public static Mask ParseMask(string line)
        {
            return new Mask(line.SplitOn("=")[1].Trim());
        }
        public static MemoryValue ParseValue(string line)
        {
            return new MemoryValue(long.Parse(line.SplitOn("=")[1].Trim()));
        }
        public static MemoryAddress ParseAddress(string line)
        {
            int index = line.IndexOf("[") + 1;
            return new MemoryAddress(int.Parse(line.Substring(index, line.IndexOf("]") - index)));
        }

    }
    public class Memory
    {
        private Dictionary<long, MemoryValue> memory = new Dictionary<long, MemoryValue>();

        public MemoryValue this[long address]
        {
            get
            {
                return memory.ContainsKey(address) ? memory[address] : throw new ArgumentException(nameof(address));
            }
            set
            {
                if (!memory.ContainsKey(address))
                    memory.Add(address, value);
                else
                    memory[address] = value;
            }
        }

        public long Sum()
        {
            return memory.Values.Select(v => v.Value).Sum();
        }
    }

    public class MemoryAddress
    {
        public long Value { get; }

        public MemoryAddress(long value)
        {
            Value = value;
        }
        public MemoryAddress(string binaryValue)
        {
            Value = binaryValue.BinaryToLong();
        }
    }

    public class MemoryValue
    {
        public long Value { get; }
        public string BinaryValue { get; }

        public MemoryValue(long originalValue)
        {
            Value = originalValue;
            BinaryValue = Value.ToBinaryString(36);
        }
        public MemoryValue(string binaryValue)
        {
            Value = binaryValue.BinaryToLong();
            BinaryValue = binaryValue;
        }
    }

    public class Mask
    {
        public static Mask Empty => new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");


        private const char REPLACEMENT = '@';
        private readonly char[] mask;

        public Mask(string mask)
        {
            this.mask = mask.ToArray();
        }

        public MemoryValue ApplyOn(MemoryValue value)
        {
            var original = value.BinaryValue.ToArray();
            for (int index = 0; index < mask.Length; index++)
            {
                if (mask[index] != 'X')
                    original[index] = mask[index];
            }
            return new MemoryValue(new string(original));
        }
        public MemoryAddress[] ApplyOn(MemoryAddress value)
        {
            var original = value.Value.ToBinaryString(36).ToArray();
            for (int index = 0; index < mask.Length; index++)
            {
                switch (mask[index])
                {
                    case '1':
                        original[index] = '1';
                        break;
                    case 'X':
                        original[index] = REPLACEMENT;
                        break;
                }
            }

            var result = new List<string>(new[] { new string(original) });
            return getAddresses(result).Select(a => new MemoryAddress(a)).ToArray();
        }
        private List<string> getAddresses(List<string> source)
        {
            if (source == null || source.Count == 0 || source.Count(s => s.Contains(REPLACEMENT)) == 0)
                return source;

            var result = new List<string>();
            var index = source[0].IndexOf(REPLACEMENT);

            foreach (var s in source)
            {
                result.Add(s.ReplaceAt(index, '0'));
                result.Add(s.ReplaceAt(index, '1'));
            }
            return getAddresses(result);
        }
    }
}