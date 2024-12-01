using FluentAssertions;

namespace AdventOfCode2024;

public class Day01Part1
{
    public long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var left = new List<long>();
        var right = new List<long>();
        foreach(var line in lines)
        {
            var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(long.Parse(numbers[0]));
            right.Add(long.Parse(numbers[1]));
        }
        
        left.Sort();
        right.Sort();
        var result = left.Zip(right).Select(numbers => Math.Abs(numbers.First - numbers.Second)).Sum();

        return result;
    }
}
public class Day01Part2
{
    public long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var left = new List<long>();
        var right = new Dictionary<long, long>();
        foreach(var line in lines)
        {
            var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            left.Add(long.Parse(numbers[0]));
            var rightValue = long.Parse(numbers[1]);
            if (!right.TryAdd(rightValue, 1))
            {
                right[rightValue]++;
            }
        }

        var result = left.Select(x => x * right.GetValueOrDefault(x, 0)).Sum();

        return result;
    }
}

public class Day01Tests
{
    private string exampleInput = @"3   4
4   3
2   5
1   3
3   9
3   3";
    
    [Fact]
    public void Part1Example()
    {
        new Day01Part1().Solve(exampleInput).Should().Be(11);
    }
    
    [Fact]
    public void Part1()
    {
        var input = Helper.ReadDay(1);
        new Day01Part1().Solve(input).Should().Be(2742123L);
    }

    [Fact]
    public void Part2Example()
    {
        new Day01Part2().Solve(exampleInput).Should().Be(31);
    }

    [Fact]
    public void Part2()
    {
        var input = Helper.ReadDay(1);
        new Day01Part2().Solve(input).Should().Be(21328497);
    }
}