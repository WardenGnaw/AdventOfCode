using System.Collections.Generic;

namespace AdventOfCode2024;

[TestClass]
public class Day1
{
    [TestMethod]
    public async Task Part1Async()
    {
        List<int> one = new List<int>();
        List<int> two = new List<int>();
        string input = await File.ReadAllTextAsync("input/day1_p1.txt");
        string[] data = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < data.Length; i++)
        {
            string[] line = data[i].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            one.Add(int.Parse(line[0]));
            two.Add(int.Parse(line[1]));
        }
        one.Sort();
        two.Sort();

        int sum = 0;
        for (int i = 0; i < one.Count; i++)
        {
            sum += one[i] > two[i] ? one[i] - two[i] : two[i] - one[i];
        }
        Assert.AreEqual(sum, 1660292);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        List<int> initial = new List<int>();
        Dictionary<int, int> counts = new Dictionary<int, int>();
        string input = await File.ReadAllTextAsync("input/day1_p1.txt");
        string[] data = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < data.Length; i++)
        {
            string[] line = data[i].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            initial.Add(int.Parse(line[0]));
            int num = int.Parse(line[1]);
            if (counts.TryGetValue(num, out int value))
            {
                counts[num] = value + 1;
            }
            else 
            {
                counts[num] = 1;
            }
        }

        int sum = 0;
        for (int i = 0; i < initial.Count; i++)
        {
            int key = initial[i];
            if (counts.TryGetValue(initial[i], out int value))
            {
                sum += key * value;
            }
        }
        Assert.AreEqual(sum, 22776016);
    }
}