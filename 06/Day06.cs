using advent_of_code_2020.lib;

namespace advent_of_code_2020._06;

public class Day06 : IRunnable
{
    private readonly List<string> groups;

    public Day06(FileReader fileReader)
    {
        groups = fileReader.ReadFile()
            .Split(Environment.NewLine + Environment.NewLine)
            .ToList();
    }

    public object RunPartOne()
        => groups.Select(x => x.Replace(Environment.NewLine, string.Empty))
            .Sum(x => x.ToHashSet().Count);

    public object RunPartTwo()
    {
        var result = 0;

        foreach (var group in groups)
        {
            var counts = new Dictionary<char, int>();
            var people = group.Split(Environment.NewLine);

            foreach (var person in people)
            {
                foreach (var answer in person)
                {
                    if (!counts.TryAdd(answer, 1))
                        counts[answer]++;
                }
            }

            result += counts.Count(x => x.Value == people.Length);
        }

        return result;
    }
}