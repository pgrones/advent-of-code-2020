using System.Reflection;
using advent_of_code_2020.lib;

var day = string.Empty;

while (day == string.Empty)
{
    Console.WriteLine("Day: ");

    var input = Console.ReadLine();

    if (input is null || input.Length > 2)
        continue;

    if (!int.TryParse(input, out var dayNumber))
        continue;

    if (dayNumber is < 1 or > 25)
        continue;

    day = input.PadLeft(2, '0');
}

var runnable = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(type => typeof(IRunnable).IsAssignableFrom(type))
    .Where(type => type.Name == $"Day{day}")
    .Select(type => (IRunnable)Activator.CreateInstance(type, new FileReader($"{day}/input.txt"))!)
    .SingleOrDefault();

if (runnable is null)
    throw new Exception("Runnable not found");

Console.WriteLine($"--- Running Day {day} ---");

var result = runnable.RunPartOne();
Console.WriteLine(result);

result = runnable.RunPartTwo();
Console.WriteLine(result);
