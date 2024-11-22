using advent_of_code_2020.lib;

namespace advent_of_code_2020._08;

public class Day08 : IRunnable
{
    private readonly string[] instructions;

    private class Computer(string[] instructions)
    {
        private int accumulator;
        private int pointer;
        private readonly HashSet<int> visited = [];

        private void Acc(int value)
        {
            accumulator += value;
            pointer++;
        }

        private void Jmp(int value)
            => pointer += value;

        private void Nop()
            => pointer++;

        public (int accumulator, bool completed) Run()
        {
            while (pointer < instructions.Length)
            {
                if (!visited.Add(pointer))
                    return (accumulator, false);

                var instruction = instructions[pointer].Split(" ");
                var operation = instruction[0];
                var value = int.Parse(instruction[1]);

                switch (operation)
                {
                    case "acc": Acc(value); break;
                    case "jmp": Jmp(value); break;
                    case "nop": Nop(); break;
                }
            }

            return (accumulator, true);
        }
    }

    public Day08(FileReader fileReader)
        => instructions = fileReader.ReadFile().Split(Environment.NewLine);

    public object RunPartOne()
        => new Computer(instructions).Run().accumulator;

    public object RunPartTwo()
    {
        var instructionSets = new List<string[]>();

        for (var i = 0; i < instructions.Length; i++)
        {
            if (instructions[i].StartsWith("acc"))
                continue;

            var instructionSet = new string[instructions.Length];
            instructions.CopyTo(instructionSet, 0);

            instructionSet[i] = instructionSet[i].StartsWith("jmp")
                ? instructionSet[i].Replace("jmp", "nop")
                : instructionSet[i].Replace("nop", "jmp");
            
            instructionSets.Add(instructionSet);
        }


        foreach (var instructionSet in instructionSets)
        {
            var (accumulator, completed) = new Computer(instructionSet).Run();

            if (completed)
                return accumulator;
        }

        throw new Exception("Result not found");
    }
}