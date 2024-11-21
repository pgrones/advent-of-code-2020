using System.Text.RegularExpressions;
using advent_of_code_2020.lib;

namespace advent_of_code_2020._04;

public class Day04 : IRunnable
{
    private readonly Dictionary<string, string>[] passports;

    private readonly Dictionary<string, Predicate<string>> requiredFields = new()
    {
        { "byr", x => int.TryParse(x, out var year) && year >= 1920 && year <= 2002 },
        { "iyr", x => int.TryParse(x, out var year) && year >= 2010 && year <= 2020 },
        { "eyr", x => int.TryParse(x, out var year) && year >= 2020 && year <= 2030 },
        { "ecl", x => new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(x) },
        { "hcl", x => new Regex(@"^#[\da-f]{6}$").IsMatch(x) },
        { "pid", x => new Regex(@"^\d{9}$").IsMatch(x) },
        {
            "hgt", x =>
            {
                if (!new Regex(@"^\d{2,3}(cm|in)$").IsMatch(x)) return false;

                return x switch
                {
                    _ when x.EndsWith("cm")
                        => int.TryParse(x.TrimEnd('c', 'm'), out var height) && height >= 150 && height <= 193,
                    _ when x.EndsWith("in")
                        => int.TryParse(x.TrimEnd('i', 'n'), out var height) && height >= 59 && height <= 76,
                    _ => false
                };
            }
        }
    };

    public Day04(FileReader fileReader)
    {
        passports = fileReader.ReadFile()
            .Split(Environment.NewLine + Environment.NewLine)
            .Select(x =>
                x.Replace(Environment.NewLine, " ")
                    .Split(" ")
                    .ToDictionary(y => y[..y.IndexOf(':')], y => y[(y.IndexOf(':') + 1)..]))
            .ToArray();
    }

    public object RunPartOne()
        => passports.Count(x => requiredFields.Keys.All(x.ContainsKey));

    public object RunPartTwo()
        => passports.Count(x => requiredFields.Keys.All(y => x.ContainsKey(y) && requiredFields[y](x[y])));
}