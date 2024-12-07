using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

[TestClass]
public class Day7
{
    static List<ulong> GenerateCombinations(List<ulong> numbers, bool includeConcatOperator)
    {
        Queue<ulong> queue = new();
        queue.Enqueue(numbers[0]);

        for (int i = 1; i < numbers.Count; i++)
        {
            int n = queue.Count;

            while (n > 0)
            {
                ulong current = queue.Dequeue();

                // +
                queue.Enqueue(current + numbers[i]);

                // *
                queue.Enqueue(current * numbers[i]);

                // ||
                if (includeConcatOperator)
                {
                    queue.Enqueue(ulong.Parse(current.ToString() + numbers[i].ToString()));
                }

                n--;
            }
        }

        return queue.ToList();
    }


    [TestMethod]
    public async Task Part1Async()
    {
        ulong testValues = 0;
        string[] input = await File.ReadAllLinesAsync("input/day7.txt");
        foreach (string line in input)
        {
            string[] data = line.Split(':', StringSplitOptions.TrimEntries);
            ulong total = ulong.Parse(data[0]);
            List<ulong> values = data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();
            List<ulong> combnations = GenerateCombinations(values, false);
            if (combnations.Contains(total))
            {
                testValues += total;
            }
        }
        Assert.AreEqual(testValues, (ulong)1611660863222);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        ulong testValues = 0;
        string[] input = await File.ReadAllLinesAsync("input/day7.txt");
        foreach (string line in input)
        {
            string[] data = line.Split(':', StringSplitOptions.TrimEntries);
            ulong total = ulong.Parse(data[0]);
            List<ulong> values = data[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).ToList();
            List<ulong> combnations = GenerateCombinations(values, true);
            if (combnations.Contains(total))
            {
                testValues += total;
            }
        }
        Assert.AreEqual(testValues, (ulong)945341732469724);
    }
}