using AdventOfCode.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    public class Puzzle10_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var adapters = Adapter.Parse(inputs).OrderBy(a => a.Jolt).ToArray();
            var sequence = new AdapterSequenceBuilder();
            var differences = sequence.Build(adapters);

            return (differences.DifferenceForJolt(1) * differences.DifferenceForJolt(3)).ToString();
        }
    }

    public class Puzzle10_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var adapters = Adapter.Parse(inputs).OrderBy(a => a.Jolt).ToArray();
            adapters = Adapter.InsertWallAndDevice(adapters);

            var adapterTree = new Tree(adapters);
            adapterTree.Build();

            var traverser = new Traverser(adapterTree);

            return traverser.CountBranches(adapters.Last().Jolt).ToString();
        }
    }


    public class AdapterSequenceBuilder
    {
        public Differences Build(Adapter[] adapters)
        {
            adapters = Adapter.InsertWallAndDevice(adapters);
            var differences = new Differences();

            var needConnection = adapters[0];
            for (int index = 0; index < adapters.Length - 1; index++)
            {
                var trying = adapters[index + 1];
                if (needConnection.CanConnectTo(trying))
                {
                    differences.IncreaseForJolt(needConnection.DifferenceWith(trying));
                    needConnection = trying;
                }
            }

            return differences;
        }
    }
    public class Adapter
    {
        public Adapter(int jolt)
        {
            Jolt = jolt;
        }

        public int Jolt { get; }


        public int Min { get { return Jolt - 3; } }

        public bool CanConnectTo(Adapter adapter)
        {
            return Jolt.Between(adapter.Min, adapter.Jolt);

        }

        public int DifferenceWith(Adapter adapter)
        {
            return adapter.Jolt - Jolt;
        }

        public override string ToString()
        {
            return $"{Jolt}, min:{Min}";
        }

        public static Adapter[] Parse(string inputs)
        {
            return inputs.SplitByLine().Select(line => ParseSingle(line)).ToArray();
        }
        public static Adapter ParseSingle(string input)
        {
            return new Adapter(int.Parse(input));
        }

        public static Adapter[] InsertWallAndDevice(Adapter[] adapters)
        {
            var result = adapters.ToList();
            result.Insert(0, new Adapter(0));
            result.Add(new Adapter(adapters.Last().Jolt + 3));
            return result.ToArray();
        }
    }

    public class Differences
    {
        private Dictionary<int, int> diff = new Dictionary<int, int>();
        public void IncreaseForJolt(int jolt)
        {
            if (!diff.ContainsKey(jolt))
                diff.Add(jolt, 0);
            diff[jolt]++;
        }
        public int DifferenceForJolt(int jolt)
        {
            if (!diff.ContainsKey(jolt))
                return 0;
            return diff[jolt];
        }
    }


    public class Tree
    {
        private readonly Dictionary<int, TreeNodeOf<Adapter>> nodes;
        private readonly Adapter[] originalItems;
        public TreeNodeOf<Adapter> Root { get; private set; }

        public Tree(Adapter[] orderedItems)
        {
            this.originalItems = orderedItems;
            this.nodes = orderedItems.ToDictionary(item => item.Jolt, item => new TreeNodeOf<Adapter>(item));
        }

        public void Build()
        {
            for (int index = 0; index < originalItems.Length - 1; index++)
            {
                var current = nodes[originalItems[index].Jolt];
                if (index == 0) Root = current;

                for (int childIndex = index + 1; childIndex < originalItems.Length; childIndex++)
                {
                    var possibleChild = nodes[originalItems[childIndex].Jolt];
                    if (current.Value.CanConnectTo(possibleChild.Value))
                        current.AddChild(possibleChild);
                    else
                        break;
                }
            }
        }
    }

    public class TreeNodeOf<T>
    {
        private readonly List<TreeNodeOf<T>> children = new List<TreeNodeOf<T>>();
        private long parentCount = 0;

        public TreeNodeOf(T item)
        {
            this.Value = item;
        }

        public T Value { get; }
        public TreeNodeOf<T>[] Children { get { return children.ToArray(); } }

        public void AddChild(TreeNodeOf<T> child)
        {
            children.Add(child);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }


    public class Traverser
    {
        private readonly Tree tree;

        public Traverser(Tree tree)
        {
            this.tree = tree;
        }
        private long CountForSpecificChildren(TreeNodeOf<Adapter> node, int forJolt)
        {
            if (node.Value.Jolt == forJolt) return 1;

            long sum = 0;
            foreach (var child in node.Children)
                sum += CountForSpecificChildren(child, forJolt);

            return sum;
        }
        public long CountBranches(int forJolt)
        {
            return CountForSpecificChildren(tree.Root, forJolt);
        }

    }
}

