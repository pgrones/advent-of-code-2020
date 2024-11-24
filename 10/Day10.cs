using advent_of_code_2020.lib;

namespace advent_of_code_2020._10;

public class Day10 : IRunnable
{
    private readonly List<int> adapters;

    public Day10(FileReader fileReader)
    {
        adapters = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(int.Parse)
            .ToList();
    }

    public object RunPartOne()
    {
        var sortedAdapters = adapters.Order().ToList();
        var oneJoltDifferences = 0; 
        var threeJoltDifferences = 1;
        var prev = 0;
        
        foreach (var adapter in sortedAdapters)
        {
            if(adapter - prev == 1)
                oneJoltDifferences++;
            
            if(adapter - prev == 3)
                threeJoltDifferences++;
            
            prev = adapter;
        }
        
        return oneJoltDifferences * threeJoltDifferences;
    }

    public object RunPartTwo()
    {
        throw new NotImplementedException();
    }
}