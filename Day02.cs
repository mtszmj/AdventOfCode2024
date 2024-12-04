namespace AdventOfCode2024;

public class Day02Part1
{
    private const int MaxChange = 3;
    private const int MinChange = 1;

    public int Solve(string input)
    {
        var reports = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var safe = 0;
        foreach (var report in reports)
        {
            var levels = report.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var start = levels[0];
            var next = levels[1];
            var increasing = start < next;
            var decreasing = start > next;
            if (!increasing && !decreasing)
            {
                continue;
            }

            var previous = start;
            var result = true;
            if (increasing)
            {
                foreach (var level in levels.Skip(1))
                {
                    var change = level - previous;
                    if (level > previous && change >= MinChange && change <= MaxChange)
                    {
                        previous = level;
                    }
                    else
                    {
                        result = false;
                        break;
                    };
                }

                safe += result ? 1 : 0;
            }
            else
            {
                foreach (var level in levels.Skip(1))
                {
                    var change = previous - level;
                    if (level < previous && change >= MinChange && change <= MaxChange)
                    {
                        previous = level;
                    }
                    else
                    {
                        result = false;
                        break;
                    };
                }

                safe += result ? 1 : 0;
            }
        }

        return safe;
    }
}

public class Day02Part2
{
    private const int MaxChange = 3;
    private const int MinChange = 1;

    public int Solve(string input)
    {
        var reports = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var safe = 0;
        foreach (var report in reports)
        {
            safe += VerifyReport(report);
        }

        return safe;
    }

    public int VerifyReport(string report)
    {
        var levels = report.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        var increase = 0;
        var equal = 0;
        var decrease = 0;
        for (var i = 0; i < levels.Count - 1; i++)
        {
            var change = levels[i + 1] - levels[i];
            if (change < 0)
            {
                decrease++;
            }
            else if (change == 0)
            {
                equal++;
            }
            else increase++;
        }

        if (equal > 1)
        {
            return 0;
        }

        if (increase > 1 && decrease > 1)
        {
            return 0;
        }

        var isIncreasing = increase > decrease;

        var changeFunc = (int previous, int current) => current - previous;
        if (!isIncreasing)
        {
            changeFunc = (int previous, int current) => previous - current;
        }

        var error = false;
        var toCheck = new List<List<int>>(); 
        for (var i = 0; i < levels.Count - 1; i++)
        {
            var change = changeFunc(levels[i], levels[i + 1]);
            if (change >= MinChange && change <= MaxChange)
            {
                continue;
            }
            if (!error)
            {
                List<int> first = [.. levels[..i], .. levels[(i + 1)..]];
                toCheck.Add(first);

                List<int> second = [.. levels[..(i + 1)], .. levels[(i+2)..]];
                toCheck.Add(second);
                
                error = true;
            }

            break;
        }

        if (!error)
        {
            return 1;
        }

        foreach (var check in toCheck)
        {
            error = false;
            for (var i = 0; i < check.Count - 1; i++)
            {
                var change = changeFunc(check[i], check[i + 1]);
                if (change >= MinChange && change <= MaxChange)
                {
                    continue;
                }

                error = true;
                break;
            }

            if (!error)
            {
                return 1;
            }
        }
        
        return 0;
    }
}

public class Day02Tests
{
    private string exampleInput = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";


    [Fact]
    public void Part1Example()
    {
        new Day02Part1().Solve(exampleInput).Should().Be(2);
    }

    [Fact]
    public void Part1()
    {
        var input = Helper.ReadDay(02);
        new Day02Part1().Solve(input).Should().Be(282);
    }

    [Fact]
    public void Part2Example()
    {
        new Day02Part2().Solve(exampleInput).Should().Be(4);
    }

    [Fact]
    public void Part2()
    {
        var input = Helper.ReadDay(02);
        new Day02Part2().Solve(input).Should().Be(349);
    }
    
    [Theory]
    [InlineData("42 44 47 49 51 52 54 52", 1)]
    public void Verify(string input, int expected)
    {
        new Day02Part2().Solve(input).Should().Be(expected);
    }
}