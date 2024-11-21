using advent_of_code_2020.lib;

namespace advent_of_code_2020._05;

public class Day05 : IRunnable
{
    private readonly Pass[] passes;

    private class Pass(string rowInstruction, string colInstruction)
    {
        public int GetId()
        {
            return DivideAndConquer(0, 127, rowInstruction, 'F', 'B') * 8 +
                   DivideAndConquer(0, 7, colInstruction, 'L', 'R');
        }

        private static int DivideAndConquer(double min, double max, string instructions, char lower, char upper)
        {
            foreach (var instruction in instructions)
            {
                if (instruction == lower)
                    max = min + Math.Floor((max - min) / 2);

                if (instruction == upper)
                    min += Math.Ceiling((max - min) / 2);
            }

            return (int)min;
        }
    }

    public Day05(FileReader fileReader)
    {
        passes = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(x => new Pass(x[..7], x[7..]))
            .ToArray();
    }

    public Task<string> RunPartOne()
    {
        return Task.FromResult(passes.Select(x => x.GetId()).Max().ToString());
    }

    public Task<string> RunPartTwo()
    {
        var sortedPasses = passes.Select(x => x.GetId()).OrderBy(x => x).ToList();
        for (var i = 0; i < sortedPasses.Count - 1; i++)
        {
            if (sortedPasses[i] + 1 != sortedPasses[i + 1])
                return Task.FromResult((sortedPasses[i] + 1).ToString());
        }

        return Task.FromResult("No result");
    }
}