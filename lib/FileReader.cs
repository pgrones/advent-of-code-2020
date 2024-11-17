namespace advent_of_code_2020.lib;

public class FileReader(string filename)
{
    public string ReadFile()
        => File.ReadAllText(filename);
}