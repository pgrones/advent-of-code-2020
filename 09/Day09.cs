using advent_of_code_2020.lib;

namespace advent_of_code_2020._09;

public class Day09 : IRunnable
{
    private readonly List<long> numbers;
    private long invalidNumber;

    public Day09(FileReader fileReader)
    {
        numbers = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(long.Parse)
            .ToList();
    }

    private static bool IsValidSum(long sum, List<long> preamble)
    {
        for (var i = 0; i < preamble.Count - 1; i++)
        {
            for (var j = i + 1; j < preamble.Count; j++)
            {
                if (preamble[i] + preamble[j] == sum)
                    return true;
            }
        }

        return false;
    }

    public object RunPartOne()
    {
        for (var i = 25; i < numbers.Count; i++)
        {
            if (IsValidSum(numbers[i], numbers.Skip(i - 25).Take(25).ToList()))
                continue;

            invalidNumber = numbers[i];
            return invalidNumber;
        }

        throw new Exception("No result found");
    }

    public object RunPartTwo()
    {
        throw new NotImplementedException();
    }
}