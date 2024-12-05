using System.Collections.Generic;

namespace AdventOfCode2024;

[TestClass]
public class Day5
{
    public class Node {
        public int Value;
        public HashSet<Node> beforeOrdering = new HashSet<Node>();
    }

    [TestMethod]
    public async Task Part1Async()
    {
        Dictionary<int, Node> nodes = new Dictionary<int, Node>();

        string input = await File.ReadAllTextAsync("input/day5.txt");
        string[] data = input.Split("\n");
        int next = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i]))
            {
                next = i + 1;
                break;
            }
            string[] order = data[i].Split('|');
            int first = int.Parse(order[0]);
            int second = int.Parse(order[1]);

            if (!nodes.TryGetValue(first, out Node? value))
            {
                value = new Node() { Value = first };
                nodes.Add(first, value);
            }

            if (!nodes.TryGetValue(second, out Node? secondNode))
            {
                secondNode = new Node() { Value = second };
                nodes.Add(second, secondNode);
            }

            value.beforeOrdering.Add(secondNode);
        }

        int sum = 0;
        for (int i = next; i < data.Length; i++)
        {
            bool isValid = true;
            int[] path = data[i].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            for (int j = 0; j < path.Length; j++)
            {
                if (nodes.TryGetValue(path[j], out Node? n))
                {
                    for (int k = j + 1; k < path.Length; k++)
                    {
                        if (!n.beforeOrdering.Where(x => x.Value == path[k]).Any())
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
                if (!isValid)
                {
                    break;
                }
            }

            if (isValid)
            {
                sum += path[path.Length / 2];
            }
        }

        Assert.AreEqual(sum, 4766);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        string input = await File.ReadAllTextAsync("input/day5.txt");
        Dictionary<int, Node> nodes = new Dictionary<int, Node>();
        string[] data = input.Split("\n");
        int next = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i]))
            {
                next = i + 1;
                break;
            }
            string[] order = data[i].Split('|');
            int first = int.Parse(order[0]);
            int second = int.Parse(order[1]);

            if (!nodes.TryGetValue(first, out Node? value))
            {
                value = new Node() { Value = first };
                nodes.Add(first, value);
            }

            if (!nodes.TryGetValue(second, out Node? secondNode))
            {
                secondNode = new Node() { Value = second };
                nodes.Add(second, secondNode);
            }

            value.beforeOrdering.Add(secondNode);
        }
        List<int[]> invalidOrderings = new();

        for (int i = next; i < data.Length; i++)
        {
            bool isValid = true;
            int[] path = data[i].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            for (int j = 0; j < path.Length; j++)
            {
                if (nodes.TryGetValue(path[j], out Node? n))
                {
                    for (int k = j + 1; k < path.Length; k++)
                    {
                        if (!n.beforeOrdering.Where(x => x.Value == path[k]).Any())
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
                if (!isValid)
                {
                    break;
                }
            }

            if (!isValid)
            {
                invalidOrderings.Add(path);
            }
        }

        int sum = 0;
        int idx = 0;
        while (idx < invalidOrderings.Count)
        {
            bool isValid = true;
            int[] currOrdering = invalidOrderings[idx];
            int j = 0;
            while (j < currOrdering.Length)
            {
                bool success = true;
                int k = j + 1;
                while (k < currOrdering.Length)
                {
                    if (nodes.TryGetValue(currOrdering[j], out Node? n))
                    {
                        if (!n.beforeOrdering.Where(x => x.Value == currOrdering[k]).Any())
                        {
                            success = false;
                            int temp = currOrdering[j];
                            currOrdering[j] = currOrdering[k];
                            currOrdering[k] = temp;
                            break;
                        }
                    }
                    k++;
                }
                if (success)
                {
                    j++;
                }
            }
            sum += currOrdering[currOrdering.Length/2];
            idx++;
        }

        Assert.AreEqual(sum, 6257);
    }
}