using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    public class Puzzle7_1 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var builder = new BagStructureBuilder();
            var structure = builder.Build(inputs);

            var bag = structure.Find("shiny gold");
            var parents = structure.GetUniqueParentsFor(bag);
            return parents.Length.ToString();
        }
    }

    public class Puzzle7_2 : IPuzzle
    {
        public string Solve(string inputs)
        {
            var builder = new BagStructureBuilder();
            var structure = builder.Build(inputs);

            var bag = structure.Find("shiny gold");
            return structure.GetBagQuantityFor(bag).ToString();
        }
    }

    public class BagStructure
    {
        private Dictionary<string, Bag> bags;
        public BagStructure(Dictionary<string, Bag> bags)
        {
            this.bags = bags;
        }

        public Bag Find(string color)
        {
            return bags[color];
        }

        public Bag[] GetUniqueParentsFor(Bag bag)
        {
            var result = new List<Bag>();
            getUniqueParents(bag, result);
            return result.ToArray();
        }

        public int GetBagQuantityFor(Bag bag)
        {
            var sum = 0;

            foreach (var child in bag.ChildrenAndQuantities)
            {
                sum += (GetBagQuantityFor(child.Bag) * child.Quantity) + child.Quantity;
            }

            return sum;
        }

        
        private void getUniqueParents(Bag bag, List<Bag> result)
        {
            foreach (var p in bag.Parents)
                if (!result.Contains(p))
                {
                    result.Add(p);
                    getUniqueParents(p, result);
                }
        }
    }

    public class BagStructureBuilder
    {
        private Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

        public BagStructure Build(string inputs)
        {
            var lines = inputs.SplitByLine();
            foreach (var line in lines)
            {
                var info = deconstruct(line);
                var parentBag = add(info.Item1);
                foreach (string child in info.Item2)
                {
                    var qty = child.SplitOn(" ")[0];
                    var c = child.Substring(qty.Length+1);

                    var childBag = add(c);
                    childBag.AddParent(parentBag);
                    parentBag.AddChild(childBag, int.Parse(qty));
                }
            }

            return new BagStructure(bags);
        }

        private Bag add(string color)
        {
            if (!bags.ContainsKey(color))
                bags.Add(color, new Bag(color));
            return bags[color];
        }

        private Tuple<string, string[]> deconstruct(string line)
        {
            var info = line.SplitOn(" bags contain ");

            var bag = info[0];
            var canContain = clean(info[1]).SplitOn(",").Select(b => b.Trim()).ToArray();

            return new Tuple<string, string[]>(bag, canContain);
        }

        private string clean(string bags)
        {
            return bags.Replace("no other bags", "")
                .Replace(" bags", "")
                .Replace(" bag", "")
                .Replace(".", "");
        }
    }

    public static class BagExtensions
    {
        public static bool Contains(this List<Bag> source, Bag bag)
        {
            return source.Count(b => b.Color == bag.Color) > 0;
        }
        public static bool Contains(this List<BagAndQuantity> source, Bag bag)
        {
            return source.Count(b => b.Bag.Color == bag.Color) > 0;
        }
    }

    public class BagAndQuantity
    {
        public BagAndQuantity(Bag bag, int quantity)
        {
            Bag = bag;
            Quantity = quantity;
        }
        public int Quantity { get; }
        public Bag Bag { get; }

    }
    public class Bag
    {
        public string Color { get; }
        private List<Bag> parents = new List<Bag>();
        private List<BagAndQuantity> children = new List<BagAndQuantity>();

        public Bag(string color)
        {
            Color = color;
        }

        public Bag[] Parents { get { return parents.ToArray(); } }
        public Bag[] Children { get { return children.Select(bq => bq.Bag).ToArray(); } }
        public BagAndQuantity[] ChildrenAndQuantities { get { return children.ToArray(); } }

        public void AddChild(Bag bag, int quantity)
        {
            if (!children.Contains(bag))
                children.Add(new BagAndQuantity(bag, quantity));
        }

        public void AddParent(Bag bag)
        {
            if (!parents.Contains(bag))
                parents.Add(bag);
        }

        public override string ToString()
        {
            return $"{Color}, Parents:{Parents.Length}, Children:{Children.Length}";
        }
    }
}
