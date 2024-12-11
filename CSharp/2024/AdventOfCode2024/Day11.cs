using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

[TestClass]
public class Day11
{
    private List<ulong> ApplyRule(ulong data)
    {
        if (data == 0)
        {
            return new List<ulong>() { 1 };
        }
        else if (data.ToString().Length % 2 == 0)
        {
            string newValue = data.ToString();
            return new List<ulong> { ulong.Parse(newValue.Substring(0, data.ToString().Length / 2)), ulong.Parse(newValue.Substring(data.ToString().Length / 2)) };
        }
        else
        {
            return new List<ulong> { data * 2024 };
        }
    }

    [TestMethod]
    public async Task Part1Async()
    {
        string input = await File.ReadAllTextAsync("input/Day11.txt");
        List<ulong> data = input.Split(" ").Select(ulong.Parse).ToList();
        for (int i = 0; i < 25; i++)
        {
            List<ulong> newList = new();
            for (int j = 0; j < data.Count; j++)
            {
                newList.AddRange(ApplyRule(data[j]));
            }
            data = newList;
        }
        Assert.AreEqual(data.Count, 203228);
    }

    public static Dictionary<ulong, ulong> ApplyRule2(ulong data)
    {
        var result = new Dictionary<ulong, ulong>();

        if (data == 0)
        {
            if (!result.ContainsKey(1))
                result[1] = 0;
            result[1]++;
        }
        else if (data.ToString().Length % 2 == 0)
        {
            string newValue = data.ToString();
            ulong left = ulong.Parse(newValue.Substring(0, data.ToString().Length / 2));
            ulong right = ulong.Parse(newValue.Substring(data.ToString().Length / 2));

            if (!result.ContainsKey(left))
                result[left] = 0;
            result[left]++;

            if (!result.ContainsKey(right))
                result[right] = 0;
            result[right]++;
        }
        else
        {
            ulong newStone = data * 2024;
            if (!result.ContainsKey(newStone))
                result[newStone] = 0;
            result[newStone]++;
        }

        return result;
    }


    [TestMethod]
    public async Task Part2Async()
    {
        string input = await File.ReadAllTextAsync("input/Day11.txt");

        // Assumes input values are unique
        var data = input.Split(" ").Select(ulong.Parse).ToDictionary(x => x, x => (ulong)1);

        for (int i = 0; i < 75; i++)
        {
            var newStoneCounts = new Dictionary<ulong, ulong>();
            foreach (var kvp in data)
            {
                var applied = ApplyRule2(kvp.Key);
                foreach (var appliedKvp in applied)
                {
                    if (!newStoneCounts.ContainsKey(appliedKvp.Key))
                        newStoneCounts[appliedKvp.Key] = 0;
                    newStoneCounts[appliedKvp.Key] += appliedKvp.Value * kvp.Value;
                }
            }
            data = newStoneCounts;
        }

        ulong totalCount = 0;
        foreach (var count in data.Values) 
        { 
            totalCount += count; 
        }
        Assert.AreEqual(totalCount, (ulong)240884656550923);
    }
}