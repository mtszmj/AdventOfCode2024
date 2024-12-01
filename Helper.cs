namespace AdventOfCode2024;

public static class Helper
{
    public static string ReadDay(int day)
    {
        return File.ReadAllText($"Data/Day{day:D2}.txt");
    }
}