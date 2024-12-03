using System.Text.RegularExpressions;
using FluentAssertions;

namespace AdventOfCode2024;

public class Day03Part1
{
    public long Solve(string input)
    {
        var matches = Regex.Matches(input, @"(mul\(\d+,\d+\))");

        var result = matches.Aggregate(0L,
            (sum, match) => sum + (match.Value.Split(new [] { "(", ")", ",", "mul" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse).Aggregate(1L, (a, b) => a * b)));
        
        return result;
    }
}

public class Day03Part2
{
    public long Solve(string input)
    {
        var matches = Regex.Matches(input, @"(mul\(\d+,\d+\)|do\(\)|don't\(\))");
        var result = 0L;
        var enabled = true;

        foreach (Match? match in matches)
        {
            switch (match.Value)
            {
                case "do()":
                    enabled = true;
                    break;
                case "don't()":
                    enabled = false;
                    break;
                default:
                    if (!enabled)
                        break;

                    var vals = match.Value.Split(new[] { "(", ")", ",", "mul" }, StringSplitOptions.RemoveEmptyEntries);
                    result += long.Parse(vals[0]) * long.Parse(vals[1]);
                    break;
            }
        }

        return result;
    }
}

public class Day03Tests
{
    private string exampleInput = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    private string exampleInput2 = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";


    [Fact]
    public void Part1Example()
    {
        new Day03Part1().Solve(exampleInput).Should().Be(161L);
    }

    [Fact]
    public void Part1()
    {
        var input = Helper.ReadDay(03);

        new Day03Part1().Solve(input).Should().Be(173419328L);
    }

    [Fact]
    public void Part2Example()
    {
        new Day03Part2().Solve(exampleInput2).Should().Be(48L);
    }

    [Fact]
    public void Part2()
    {
        var input = Helper.ReadDay(03);

        new Day03Part2().Solve(input).Should().Be(0L);
    }
}