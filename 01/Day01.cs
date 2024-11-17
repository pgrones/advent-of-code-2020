using advent_of_code_2020.lib;

namespace advent_of_code_2020._01;

public class Day01 : IRunnable
{
    private List<int> Entries { get; }

    public Day01(FileReader fileReader)
    {
        var input = fileReader.ReadFile();

        Entries = input.Split(Environment.NewLine)
            .Select(int.Parse)
            .ToList();
    }

    public Task<string> RunPartOne()
    {
        foreach (var (index, entry) in Entries.Select((value, i) => (i, value)))
        {
            foreach (var entry2 in Entries.Skip(index + 1))
            {
                if (entry + entry2 == 2020)
                    return Task.FromResult((entry * entry2).ToString());
            }
        }

        throw new Exception("Result not found");
    }

    public Task<string> RunPartTwo()
    {
        foreach (var (index, entry) in Entries.Select((value, i) => (i, value)))
        {
            foreach (var (index2, entry2) in Entries.Skip(index + 1).Select((value, i) => (i, value)))
            {
                foreach (var entry3 in Entries.Skip(index + index2 + 2))
                {
                    if (entry + entry2 + entry3 == 2020)
                        return Task.FromResult((entry * entry2 * entry3).ToString());
                }
            }
        }

        throw new Exception("Result not found");
    }
}