namespace AdventOfCode2024;

public class Day04Part1
{
    public long Solve(string input)
    {
        var array = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToArray())
            .ToArray();

        var count = 0L;
        var sizeI = array.Length;
        var sizeJ = array[0].Length;
        for (var i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                if (array[i][j] != 'X')
                    continue;
                
                // horizontal normal
                if(j+3 < sizeJ && array[i][j+1] == 'M' && array[i][j+2] == 'A' && array[i][j+3] == 'S')
                    count++;
                // horizontal backwards
                if(j >= 3 && array[i][j-1] == 'M' && array[i][j-2] == 'A' && array[i][j-3] == 'S')
                    count++;
                // vertical top to bottom
                if(i+3 < sizeI && array[i+1][j] == 'M' && array[i+2][j] == 'A' && array[i+3][j] == 'S')
                    count++;
                // vertical bottom to top
                if(i >= 3 && array[i-1][j] == 'M' && array[i-2][j] == 'A' && array[i-3][j] == 'S')
                    count++;
                // diagonal left-right-top-bottom
                if(j+3 < sizeJ && i+3 < sizeI && array[i+1][j+1] == 'M' && array[i+2][j+2] == 'A' && array[i+3][j+3] == 'S')
                    count++;
                // diagonal right-left-top-bottom
                if(j >= 3 && i+3 < sizeI && array[i+1][j-1] == 'M' && array[i+2][j-2] == 'A' && array[i+3][j-3] == 'S')
                    count++;
                // diagonal left-right-bottom-top
                if(j+3 < sizeJ && i >= 3 && array[i-1][j+1] == 'M' && array[i-2][j+2] == 'A' && array[i-3][j+3] == 'S')
                    count++;
                // diagonal right-left-bottom-top
                if(j >= 3 && i >= 3 && array[i-1][j-1] == 'M' && array[i-2][j-2] == 'A' && array[i-3][j-3] == 'S')
                    count++;
            }
        }
        return count;
    }
}

public class Day04Part2
{
    public long Solve(string input)
    {
        var array = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToArray())
            .ToArray();

        var count = 0L;
        var sizeI = array.Length;
        var sizeJ = array[0].Length;
        for (var i = 0; i < sizeI; i++)
        {
            for (int j = 0; j < sizeJ; j++)
            {
                if (array[i][j] != 'A')
                    continue;

                if (i == 0 || j == 0 || i == sizeI - 1 || j == sizeJ - 1)
                    continue;
                
                //left top - right bottom
                var ltrb = array[i-1][j-1] == 'M' && array[i+1][j+1] == 'S' || array[i-1][j-1] == 'S' && array[i+1][j+1] == 'M';
                //left bottom - right top
                var lbrt = ltrb && (array[i+1][j-1] == 'M' && array[i-1][j+1] == 'S' || array[i+1][j-1] == 'S' && array[i-1][j+1] == 'M');

                if (lbrt)
                {
                    count++;
                }
            }
        }
        return count;
    }
}

public class Day04Tests
{
    private string exampleInput = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
    
    private string exampleInput2 = @".M.S......
..A..MSMS.
.M.S.MAA..
..A.ASMSM.
.M.S.M....
..........
S.S.S.S.S.
.A.A.A.A..
M.M.M.M.M.
..........";


    [Fact]
    public void Part1Example()
    {
        new Day04Part1().Solve(exampleInput).Should().Be(18);
    }

    [Fact]
    public void Part1()
    {
        var input = Helper.ReadDay(04);

        new Day04Part1().Solve(input).Should().Be(2567L);
    }

    [Fact]
    public void Part2Example()
    {
        new Day04Part2().Solve(exampleInput2).Should().Be(9L);
    }

    [Fact]
    public void Part2()
    {
        var input = Helper.ReadDay(04);

        new Day04Part2().Solve(input).Should().Be(2029L);
    }
}