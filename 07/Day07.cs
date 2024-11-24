using System.Text.RegularExpressions;
using advent_of_code_2020.lib;

namespace advent_of_code_2020._07;

public class Day07 : IRunnable
{
    private class Bag(string name)
    {
        public string Name { get; } = name;
        public List<Bag> Parents { get; } = [];
        public List<(int count, Bag child)> Children { get; } = [];
    }

    private readonly List<Bag> bags = [];

    public Day07(FileReader fileReader)
    {
        var lines = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .ToList();

        foreach (var line in lines)
        {
            var parentName = line[..(line.IndexOf("bags", StringComparison.Ordinal) + 3)];
            Bag parent;

            if (bags.Any(b => b.Name == parentName))
                parent = bags.Single(b => b.Name == parentName);
            else
            {
                parent = new Bag(parentName);
                bags.Add(parent);
            }

            var children = line[(parentName.Length + 10)..]
                .Split(", ")
                .Select(x => new Tuple<int, string>(Regex.IsMatch(x, @"\d+")
                        ? int.Parse(Regex.Match(x, @"\d+").Value)
                        : 0,
                    Regex.Replace(x.TrimEnd('.', 's'), @"\d+\s", string.Empty))).ToList();

            if (children is [(0, "no other bag")])
                continue;

            foreach (var childName in children)
            {
                Bag child;
                
                if (bags.Any(b => b.Name == childName.Item2))
                    child = bags.Single(b => b.Name == childName.Item2);
                else
                {
                    child = new Bag(childName.Item2);
                    bags.Add(child);
                }

                child.Parents.Add(parent);
                parent.Children.Add((childName.Item1, child));
            }
        }
    }

    public object RunPartOne()
    {
        var uniqueBags = new HashSet<string>();
        var shinyGoldBag = bags.Single(x => x.Name == "shiny gold bag");

        Traverse(shinyGoldBag);

        return uniqueBags.Count;

        void Traverse(Bag bag)
        {
            foreach (var parent in bag.Parents)
            {
                uniqueBags.Add(parent.Name);
                Traverse(parent);
            }
        }
    }

    public object RunPartTwo()
    {
        var shinyGoldBag = bags.Single(x => x.Name == "shiny gold bag");

        return Traverse(shinyGoldBag);

        int Traverse(Bag bag)
        {
            var localTotal = 0;

            foreach (var (count, child) in bag.Children)
            {
                localTotal += count;
                
                if (child.Children.Count != 0)
                    localTotal += count * Traverse(child);
            }

            return localTotal;
        }
    }
}