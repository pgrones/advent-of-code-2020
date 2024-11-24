using advent_of_code_2020.lib;

namespace advent_of_code_2020._11;

public class Day11 : IRunnable
{
    private class Seat((int x, int y) position, bool occupied = false)
    {
        private (int x, int y) Position { get; } = position;

        public bool IsOccupied()
            => occupied;

        public Seat ApplyRules(List<Seat> seats)
        {
            List<(int x, int y)> adjacentPositions =
            [
                (Position.x, Position.y - 1),
                (Position.x, Position.y + 1),
                (Position.x - 1, Position.y),
                (Position.x + 1, Position.y),
                (Position.x - 1, Position.y - 1),
                (Position.x + 1, Position.y - 1),
                (Position.x - 1, Position.y + 1),
                (Position.x + 1, Position.y + 1)
            ];

            var occupiedAdjacentSeats = seats.Where(s => adjacentPositions.Contains(s.Position) && s.IsOccupied())
                .ToList();

            if (!IsOccupied() && occupiedAdjacentSeats.Count == 0)
                return new Seat(Position, true);

            if (IsOccupied() && occupiedAdjacentSeats.Count >= 4)
                return new Seat(Position);

            return this;
        }
    }

    private readonly List<Seat> initialSeats = [];

    public Day11(FileReader fileReader)
    {
        var seats = fileReader.ReadFile()
            .Split(Environment.NewLine)
            .Select(x => x.ToCharArray())
            .ToList();

        for (var y = 0; y < seats.Count; y++)
        {
            for (var x = 0; x < seats.First().Length; x++)
            {
                if (seats[y][x] == '.')
                    continue;

                initialSeats.Add(new Seat((x, y)));
            }
        }
    }

    public object RunPartOne()
    {
        var seats = initialSeats;

        while (true)
        {
            var nextSeats = seats.Select(x => x.ApplyRules(seats)).ToList();
            
            if(seats.SequenceEqual(nextSeats))
                return seats.Count(x => x.IsOccupied());
            
            seats = nextSeats;
        }
    }

    public object RunPartTwo()
    {
        throw new NotImplementedException();
    }
}