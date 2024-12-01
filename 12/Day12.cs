using advent_of_code_2020.lib;

namespace advent_of_code_2020._12;

public class Day12 : IRunnable
{
    private enum Direction
    {
        N,
        E,
        S,
        W,
        L,
        R,
        F
    }

    private readonly List<(Direction direction, int value)> instructions;

    public Day12(FileReader fileReader)
    {
        instructions = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(line => (Enum.Parse<Direction>(line[0].ToString()), int.Parse(line[1..])))
            .ToList();
    }

    public object RunPartOne()
    {
        List<Direction> directions = [Direction.N, Direction.E, Direction.S, Direction.W];
        var currDir = Direction.E;
        (int x, int y) position = (0, 0);

        foreach (var (direction, value) in instructions)
        {
            if (direction is Direction.L or Direction.R)
            {
                var rotations = value / 90;

                if (direction is Direction.L)
                    rotations *= -1;

                var index = directions.IndexOf(currDir) + rotations;
                currDir = directions[(index % 4 + 4) % 4];
                continue;
            }

            Move(direction, value);
            Console.WriteLine(position);
        }

        return Math.Abs(position.x) + Math.Abs(position.y);

        void Move(Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.F:
                    Move(currDir, value);
                    break;
                case Direction.N:
                    position.y -= value;
                    break;
                case Direction.E:
                    position.x += value;
                    break;
                case Direction.S:
                    position.y += value;
                    break;
                case Direction.W:
                    position.x -= value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }

    public object RunPartTwo()
    {
        throw new NotImplementedException();
    }
}