using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

[TestClass]
public class Day10
{
    public int WalkNext(int[][] data, int[,] grid, int current, int i, int j, int rowMax, int colMax)
    {
        if (i < 0 || i >= rowMax || j < 0 || j >= colMax)
        {
            return 0;
        }
        if (current == 9)
        {
            if (grid[i,j] == 1)
            {
                return 0;
            }
            grid[i, j] = 1;
            return data[i][j] == 9 ? 1 : 0;
        }

        int ret = 0;

        int next = current + 1;
        if (i + 1 < rowMax && data[i + 1][j] == next)
        {
            ret += WalkNext(data, grid, next, i + 1, j, rowMax, colMax);
        }
        if (i - 1 >= 0 && data[i - 1][j] == next)
        {
            ret += WalkNext(data, grid, next, i - 1, j, rowMax, colMax);
        }
        if (j - 1 >= 0 && data[i][j - 1] == next)
        {
            ret += WalkNext(data, grid, next, i, j - 1, rowMax, colMax);
        }
        if (j + 1 < colMax && data[i][j + 1] == next)
        {
            ret += WalkNext(data, grid, next, i, j + 1, rowMax, colMax);
        }

        return ret;
    }
    public int WalkNext2(int[][] data, int[,] grid, int current, int i, int j, int rowMax, int colMax)
    {
        if (i < 0 || i >= rowMax || j < 0 || j >= colMax)
        {
            return 0;
        }
        if (current == 9)
        {
            return data[i][j] == 9 ? 1 : 0;
        }

        int ret = 0;

        int next = current + 1;
        if (i + 1 < rowMax && data[i + 1][j] == next)
        {
            ret += WalkNext2(data, grid, next, i + 1, j, rowMax, colMax);
        }
        if (i - 1 >= 0 && data[i - 1][j] == next)
        {
            ret += WalkNext2(data, grid, next, i - 1, j, rowMax, colMax);
        }
        if (j - 1 >= 0 && data[i][j - 1] == next)
        {
            ret += WalkNext2(data, grid, next, i, j - 1, rowMax, colMax);
        }
        if (j + 1 < colMax && data[i][j + 1] == next)
        {
            ret += WalkNext2(data, grid, next, i, j + 1, rowMax, colMax);
        }

        return ret;
    }


    [TestMethod]
    public async Task Part1Async()
    {
        string input = await File.ReadAllTextAsync("input/day10.txt");
        int[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().Select(x => {
            if (x == '.')
            {
                return -10;
            }
            return int.Parse(x.ToString());
        }).ToArray()).ToArray();

        List<Tuple<int, int>> starts = new();

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                if (data[i][j] == 0)
                {
                    starts.Add(Tuple.Create(i, j));
                }
            }
        }
        int rowMax = data.Length;
        int colMax = data[0].Length;
        int total = 0;
        foreach (Tuple<int, int> s in  starts)
        {
            int[,] grid = new int[rowMax, colMax];
            total += WalkNext(data, grid, 0, s.Item1, s.Item2, rowMax, colMax);
        }
        Assert.AreEqual(719, total);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        string input = await File.ReadAllTextAsync("input/day10.txt");
        int[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().Select(x => {
            if (x == '.')
            {
                return -10;
            }
            return int.Parse(x.ToString());
        }).ToArray()).ToArray();

        List<Tuple<int, int>> starts = new();

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                if (data[i][j] == 0)
                {
                    starts.Add(Tuple.Create(i, j));
                }
            }
        }
        int rowMax = data.Length;
        int colMax = data[0].Length;
        int total = 0;
        foreach (Tuple<int, int> s in starts)
        {
            int[,] grid = new int[rowMax, colMax];
            total += WalkNext2(data, grid, 0, s.Item1, s.Item2, rowMax, colMax);
        }
        Assert.AreEqual(1530, total);
    }
}