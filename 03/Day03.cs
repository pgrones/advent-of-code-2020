using advent_of_code_2020.lib;

namespace advent_of_code_2020._03;

public class Day03 : IRunnable
{
    private readonly char[][] map;
    private readonly int width;

    public Day03(FileReader fileReader)
    {
        map = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToArray();

        width = map[0].Length;
    }

    private bool IsTree(int x, int y)
        => map[y][x] == '#';

    private bool Done(int y)
        => map.Length <= y;

    private int SimulateSlope(int deltaX, int deltaY)
    {
        var x = 0;
        var y = 0;
        var trees = 0;

        while (!Done(y))
        {
            if (IsTree(x, y))
                trees++;

            x = (x + deltaX) % width;
            y += deltaY;
        }

        return trees;
    }

    public object RunPartOne()
        => SimulateSlope(3, 1).ToString();

    public object RunPartTwo()
    {
        (int, int)[] slopes = [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)];
        var result = 1L;

        foreach (var (x, y) in slopes)
        {
            result *= SimulateSlope(x, y);
        }

        return result;
    }
}