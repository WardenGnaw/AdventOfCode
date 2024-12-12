namespace AdventOfCode2024;

[TestClass]
public class Day12
{
    private Tuple<int, int> FloodFill(char c, char[][] data, int rowMax, int colMax, bool[,] seen, bool[,] up, bool[,] down, bool[,] left, bool[,] right, int i, int j, bool part2)
    {
        if (seen[i, j])
        {
            return Tuple.Create(0, 0);
        }
        seen[i, j] = true;

        int count = 1;
        int perimiter = 0;

        // Up
        if (i - 1 >= 0 && data[i - 1][j] == c)
        {
            Tuple<int, int> ret = FloodFill(c, data, rowMax, colMax, seen, up, down, left, right, i - 1, j, part2);
            count += ret.Item1;
            perimiter += ret.Item2;
        }
        else
        {
            if (!part2 || !up[i, j])
            {
                up[i, j] = true;
                for (int temp = j - 1; temp >= 0 && data[i][temp] == c; temp--)
                {
                    if (i - 1 >= 0 && data[i - 1][temp] == c)
                    {
                        break;
                    }
                    up[i, temp] = true;
                }
                for (int temp = j + 1; temp < colMax && data[i][temp] == c; temp++)
                {
                    if (i - 1 >= 0 && data[i - 1][temp] == c)
                    {
                        break;
                    }
                    up[i, temp] = true;
                }
                perimiter++;
            }
        }

        // Down
        if (i + 1 < rowMax && data[i + 1][j] == c)
        {
            Tuple<int, int> ret = FloodFill(c, data, rowMax, colMax, seen, up, down, left, right, i + 1, j, part2);
            count += ret.Item1;
            perimiter += ret.Item2;
        }
        else
        {
            if (!part2 || !down[i, j])
            {
                down[i, j] = true;
                for (int temp = j - 1; temp >= 0 && data[i][temp] == c; temp--)
                {
                    if (i + 1 < rowMax && data[i + 1][temp] == c)
                    {
                        break;
                    }
                    down[i, temp] = true;
                }
                for (int temp = j + 1; temp < colMax && data[i][temp] == c; temp++)
                {
                    if (i + 1 < rowMax && data[i + 1][temp] == c)
                    {
                        break;
                    }
                    down[i, temp] = true;
                }
                perimiter++;
            }
        }

        // Left
        if (j - 1 >= 0 && data[i][j - 1] == c)
        {
            Tuple<int, int> ret = FloodFill(c, data, rowMax, colMax, seen, up, down, left, right, i, j - 1, part2);
            count += ret.Item1;
            perimiter += ret.Item2;
        }
        else
        {
            if (!part2 || !left[i, j])
            {
                left[i, j] = true;
                for (int temp = i - 1; temp >= 0 && data[temp][j] == c; temp--)
                {
                    if (j - 1 >= 0 && data[temp][j - 1] == c)
                    {
                        break;
                    }
                    left[temp, j] = true;
                }
                for (int temp = i + 1; temp < rowMax && data[temp][j] == c; temp++)
                {
                    if (j - 1 >= 0 && data[temp][j - 1] == c)
                    {
                        break;
                    }
                    left[temp, j] = true;
                }
                perimiter++;
            }
        }

        // Right
        if (j + 1 < colMax && data[i][j + 1] == c)
        {
            Tuple<int, int> ret = FloodFill(c, data, rowMax, colMax, seen, up, down, left, right, i, j + 1, part2);
            count += ret.Item1;
            perimiter += ret.Item2;
        }
        else
        {
            if (!part2 || !right[i, j])
            {
                right[i, j] = true;
                for (int temp = i - 1; temp >= 0 && data[temp][j] == c; temp--)
                {
                    if (j + 1 < colMax && data[temp][j + 1] == c)
                    {
                        break;
                    }
                    right[temp, j] = true;
                }
                for (int temp = i + 1; temp < rowMax && data[temp][j] == c; temp++)
                {
                    if (j + 1 < colMax && data[temp][j + 1] == c)
                    {
                        break;
                    }
                    right[temp, j] = true;
                }
                perimiter++;
            }
        }

        return Tuple.Create(count, perimiter);
    }

    [TestMethod]
    public async Task Part1Async()
    {
        Dictionary<char, Tuple<int, int>> map = new();
        string input = await File.ReadAllTextAsync("input/Day12.txt");
        char[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().ToCharArray()).ToArray();
        int rowMax = data.Length;
        int colMax = data[0].Length;
        bool[,] seen = new bool[rowMax, colMax];
        int price = 0;
        for (int i = 0; i < rowMax; i++)
        {
            for (int j = 0; j < colMax; j++)
            {
                if (seen[i, j])
                {
                    continue;
                }
                bool[,] up = new bool[rowMax, colMax];
                bool[,] down = new bool[rowMax, colMax];
                bool[,] left = new bool[rowMax, colMax];
                bool[,] right = new bool[rowMax, colMax];
                Tuple<int, int> count = FloodFill(data[i][j], data, rowMax, colMax, seen, up, down, left, right, i, j, false);
                price += count.Item1 * count.Item2;
            }
        }
        Assert.AreEqual(price, 1546338);
    }


    [TestMethod]
    public async Task Part2Async()
    {
        Dictionary<char, Tuple<int, int>> map = new();
        string input = await File.ReadAllTextAsync("input/Day12.txt");
        char[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().ToCharArray()).ToArray();
        int rowMax = data.Length;
        int colMax = data[0].Length;
        bool[,] seen = new bool[rowMax, colMax];
        int price = 0;
        for (int i = 0; i < rowMax; i++)
        {
            for (int j = 0; j < colMax; j++)
            {
                if (seen[i, j])
                {
                    continue;
                }
                bool[,] up = new bool[rowMax, colMax];
                bool[,] down = new bool[rowMax, colMax];
                bool[,] left = new bool[rowMax, colMax];
                bool[,] right = new bool[rowMax, colMax];
                Tuple<int, int> count = FloodFill(data[i][j], data, rowMax, colMax, seen, up, down, left, right, i, j, true);
                price += count.Item1 * count.Item2;
            }
        }
        Assert.AreEqual(price, 978590);
    }
}