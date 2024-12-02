using System.Collections.Generic;

namespace AdventOfCode2024;

[TestClass]
public class Day2
{
    [TestMethod]
    public async Task Part1Async()
    {
        string input = await File.ReadAllTextAsync("input/day2_p1.txt");
        string[] data = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        int safe = 0;
        for (int i = 0; i < data.Length; i++)
        {
            bool isValid = true;
            int[] row = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int last = row[0];
            bool direction = last < row[1];
            for (int j = 1; j < row.Length; j++)
            {
                int next = row[j];
                int diff = next - last;

                if ((!direction && diff < 0 && diff > -4) || (direction && diff > 0 && diff < 4))
                {
                }
                else
                {
                    isValid = false;
                    break;
                }
                last = next;
            }

            if (isValid)
            {
                safe++;
            }
        }
        Assert.AreEqual(safe, 402);
    }

    [TestMethod]
    public async Task Part2Async()
    {        
        string input = await File.ReadAllTextAsync("input/day2_p1.txt");
                string[] data = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        int safe = 0;
        for (int i = 0; i < data.Length; i++)
        {
            int[] row = data[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            bool isValid = IsValid(row, 0, 1, -1);
            if (!isValid)
            {
                for (int j = 0; j < row.Length; j++)
                {
                    if (j == 0)
                    {
                        isValid = IsValid(row, 1, 2, j);
                    }
                    else if (j == 1)
                    {
                        isValid = IsValid(row, 0, 2, j);
                    }
                    else
                    {
                        isValid = IsValid(row, 0, 1, j);
                    }

                    if (isValid)
                    {
                        break;
                    }
                }
            }
            if (isValid)
            {
                safe++;
            }
        }
        Assert.AreEqual(safe, 455);
    }

    private bool IsValid(int[] row, int initial, int start, int skipIdx)
    {
        bool isValid = true;
        int last = row[initial];
        bool direction = last < row[start];
        for (int j = start; j < row.Length; j++)
        {
            if (j == skipIdx)
            {
                continue;
            }
            int next = row[j];
            int diff = next - last;

            if ((!direction && diff < 0 && diff > -4) || (direction && diff > 0 && diff < 4))
            {
            }
            else
            {
                isValid = false;
                break;
            }
            last = next;
        }
        return isValid;
    }
}