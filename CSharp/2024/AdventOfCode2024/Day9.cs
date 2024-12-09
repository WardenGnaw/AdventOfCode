using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

[TestClass]
public class Day9
{
    private readonly ulong GAP = ulong.MaxValue;
    public class Range
    {
        public ulong? Idx { get; set; }
        public ulong Id { get; set; }

        public int Length { get; set; }

        public Range(ulong id, int length, ulong? idx = null)
        {
            Id = id;
            Length = length;
            Idx = idx;
        }
    }


    [TestMethod]
    public async Task Part1Async()
    {
        ulong id = 0;
        string input = (await File.ReadAllTextAsync("test/Day9.txt")).Trim();
        int[] data = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
        LinkedList<Range> ranges = new LinkedList<Range>();
        for (int i = 0; i < data.Length; i++)
        {
            if (ranges.Count == 0 || ranges.Last.Value.Id == GAP)
            {
                ranges.AddLast(new Range(id++, data[i]));
            }
            else
            {
                if (data[i] >= 0)
                {
                    Range r = new Range(GAP, data[i]);
                    ranges.AddLast(r);
                }
            }
        }

        List<ulong> finalList = new List<ulong>();

        Range last = new Range(0, 0);
        int remainder = 0;

        while (ranges.Count > 0)
        {
            Range r = ranges.First();
            ranges.RemoveFirst();

            if (r.Id != GAP)
            {
                for (int i = 0; i <  r.Length; i++)
                {
                    finalList.Add(r.Id);
                }
            }
            else
            {
                for (int i = 0; i < r.Length; i++)
                {
                    if (remainder == 0)
                    {
                        do
                        {
                            last = ranges.Last();
                            ranges.RemoveLast();
                        } while (last.Id == GAP);
                        remainder = last.Length;
                    }

                    finalList.Add(last.Id);
                    remainder--;
                }
            }
        }

        while (remainder > 0)
        {
            finalList.Add(last.Id);
            remainder--;
        }

        ulong total = 0;

        for (int i = 0; i < finalList.Count; i++)
        {
            total += finalList[i] * (ulong)i;
        }

        Assert.AreEqual(total, (ulong)6283170117911);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        ulong id = 0;
        ulong idx = 0;
        string input = (await File.ReadAllTextAsync("test/Day9.txt")).Trim();
        int[] data = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
        LinkedList<Range> ranges = new LinkedList<Range>();
        for (int i = 0; i < data.Length; i++)
        {
            if (ranges.Count == 0 || ranges.Last.Value.Id == GAP)
            {
                ranges.AddLast(new Range(id++, data[i], idx++));
            }
            else
            {
                Range r = new Range(GAP, data[i], idx++);
                ranges.AddLast(r);
            }
        }

        LinkedListNode<Range> current = ranges.Last;

        while (current != null)
        {
            Range r = current.Value;
            if (r.Id != GAP)
            {
                IEnumerable<Range> freeRangeEnum = ranges.Where(x => x.Id == GAP && x.Idx < r.Idx && r.Length <= x.Length);
                if (freeRangeEnum.Any())
                {
                    LinkedListNode<Range>? node = ranges.Find(freeRangeEnum.First());

                    if (node is not null)
                    {
                        int length = node.Value.Length - r.Length;

                        ranges.AddBefore(node, new Range(r.Id, r.Length, node.Value.Idx));
                        if (length > 0)
                        {
                            Range newFree = new Range(GAP, length, node.Value.Idx);
                            ranges.AddBefore(node, newFree);
                        }
                        Range endNewFree = new Range(GAP, r.Length, r.Idx);

                        ranges.Remove(node);
                        node = ranges.Find(r);
                        current = current.Previous;
                        ranges.AddBefore(node, endNewFree);

                        ranges.Remove(r);
                    }
                }
                else
                {
                    current = current.Previous;
                }
            }
            else
            {
                current = current.Previous;
            }
        }

        ulong total = 0;

        ulong index = 0;

        foreach (Range range in ranges)
        {
            if (range.Id != GAP)
            {
                for (int i = 0; i < range.Length; i++)
                {
                    total += index++ * range.Id;
                }
            }
            else
            {
                for (int i = 0; i < range.Length; i++)
                {

                    index++;
                }
            }
        }


        Assert.AreEqual(total, (ulong)1928);
    }
}