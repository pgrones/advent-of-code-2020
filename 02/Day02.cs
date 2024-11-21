using advent_of_code_2020.lib;

namespace advent_of_code_2020._02;

public class Day02 : IRunnable
{
    private record Model(int Min, int Max, char Letter, string Password);

    private readonly IReadOnlyList<Model> passwords;

    public Day02(FileReader fileReader)
    {
        var input = fileReader.ReadFile();

        passwords = input.Split(Environment.NewLine)
            .Select(x =>
            {
                var parts = x.Split(" ");
                var numbers = parts[0].Split("-");

                var min = int.Parse(numbers[0]);
                var max = int.Parse(numbers[1]);
                var letter = parts[1].ToCharArray()[0];
                var password = parts[2];

                return new Model(min, max, letter, password);
            })
            .ToList()
            .AsReadOnly();
    }

    public object RunPartOne()
    {
        var validPasswords = 0;
        foreach (var password in passwords)
        {
            var occurrences = password.Password.Count(letter => letter == password.Letter);

            if (occurrences >= password.Min && occurrences <= password.Max)
                validPasswords++;
        }

        return validPasswords;
    }

    public object RunPartTwo()
    {
        var validPasswords = passwords
            .Select(password => password.Password[password.Min - 1] != password.Password[password.Max - 1] &&
                                (password.Password[password.Min - 1] == password.Letter ||
                                 password.Password[password.Max - 1] == password.Letter))
            .Count(isValid => isValid);

        return validPasswords;
    }
}