﻿using advent_of_code_2020.lib;

namespace advent_of_code_2020._01;

public class Day01 : IRunnable
{
    private readonly IReadOnlyList<int> entries;

    public Day01(FileReader fileReader)
    {
        var input = fileReader.ReadFile();

        entries = input.Split(Environment.NewLine)
            .Select(int.Parse)
            .ToList()
            .AsReadOnly();
    }

    public object RunPartOne()
    {
        foreach (var (index, entry) in entries.Select((value, i) => (i, value)))
        {
            foreach (var entry2 in entries.Skip(index + 1))
            {
                if (entry + entry2 == 2020)
                    return entry * entry2;
            }
        }

        throw new Exception("Result not found");
    }

    public object RunPartTwo()
    {
        foreach (var (index, entry) in entries.Select((value, i) => (i, value)))
        {
            foreach (var (index2, entry2) in entries.Skip(index + 1).Select((value, i) => (i, value)))
            {
                foreach (var entry3 in entries.Skip(index + index2 + 2))
                {
                    if (entry + entry2 + entry3 == 2020)
                        return entry * entry2 * entry3;
                }
            }
        }

        throw new Exception("Result not found");
    }
}