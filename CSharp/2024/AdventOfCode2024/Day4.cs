namespace AdventOfCode2024;

[TestClass]
public class Day4
{
    public int FindXMAS(char[][] grid, int i, int j)
    {
        int rowLength = grid[i].Length;
        int colLength = grid.Length;
        int count = 0;

        // Right
        if (j + 3 < rowLength && grid[i][j + 1] == 'M' && grid[i][j + 2] == 'A' && grid[i][j + 3] == 'S')
        {
            count++;
        }

        // Left
        if (j - 3 >= 0 && grid[i][j - 1] == 'M' && grid[i][j - 2] == 'A' && grid[i][j - 3] == 'S')
        {
            count++;
        }

        // Up
        if (i - 3 >= 0 && grid[i - 1][j] == 'M' && grid[i - 2][j] == 'A' && grid[i - 3][j] == 'S')
        {
            count++;
        }

        // Down
        if (i + 3 < colLength && grid[i + 1][j] == 'M' && grid[i + 2][j] == 'A' && grid[i + 3][j] == 'S')
        {
            count++;
        }

        // Up-Left
        if (i - 3 >= 0 && j - 3 >= 0 && grid[i - 1][j - 1] == 'M' && grid[i - 2][j - 2] == 'A' && grid[i - 3][j - 3] == 'S')
        {
            count++;
        }

        // Up-Right
        if (i - 3 >= 0 && j + 3 < rowLength && grid[i - 1][j + 1] == 'M' && grid[i - 2][j + 2] == 'A' && grid[i - 3][j + 3] == 'S')
        {
            count++;
        }

        // Down-Left
        if (i + 3 < colLength && j - 3 >= 0 && grid[i + 1][j - 1] == 'M' && grid[i + 2][j - 2] == 'A' && grid[i + 3][j - 3] == 'S')
        {
            count++;
        }

        // Down-Right
        if (i + 3 < colLength && j + 3 < rowLength && grid[i + 1][j + 1] == 'M' && grid[i + 2][j + 2] == 'A' && grid[i + 3][j + 3] == 'S')
        {
            count++;
        }

        return count;
    }

    public int FindX_MAS(char[][] grid, int i, int j)
    {
        int rowLength = grid[i].Length;
        int colLength = grid.Length;
        int count = 0;

        // M   S
        //   A
        // M   S
        if (grid[i-1][j-1] == 'M' && grid[i+1][j+1] == 'S' && grid[i+1][j-1] == 'M' && grid[i-1][j+1] == 'S')
        {
            count++;
        }

        // M   M
        //   A
        // S   S
        if (grid[i-1][j-1] == 'M' && grid[i+1][j+1] == 'S' && grid[i+1][j-1] == 'S' && grid[i-1][j+1] == 'M')
        {
            count++;
        }

        // S   M
        //   A
        // S   M
        if (grid[i-1][j-1] == 'S' && grid[i+1][j+1] == 'M' && grid[i+1][j-1] == 'S' && grid[i-1][j+1] == 'M')
        {
            count++;
        }

        // S   S
        //   A
        // M   M
        if (grid[i-1][j-1] == 'S' && grid[i+1][j+1] == 'M' && grid[i+1][j-1] == 'M' && grid[i-1][j+1] == 'S')
        {
            count++;
        }

        return count;
    }

    [TestMethod]
    public async Task Part1Async()
    {
        string input = await File.ReadAllTextAsync("input/day4.txt");
        char[][] grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
        int count = 0;
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 'X')
                {
                    count += FindXMAS(grid, i, j);
                }
            }
        }
        Assert.AreEqual(count, 2454);
    }

    [TestMethod]
    public async Task Part2Async()
    {        
        string input = await File.ReadAllTextAsync("input/day4.txt");
                char[][] grid = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
        int count = 0;
        for (int i = 1; i < grid.Length - 1; i++)
        {
            for (int j = 1; j < grid[i].Length - 1; j++)
            {
                if (grid[i][j] == 'A')
                {
                    count += FindX_MAS(grid, i, j);
                }
            }
        }
        Assert.AreEqual(count, 1858);
    }
}