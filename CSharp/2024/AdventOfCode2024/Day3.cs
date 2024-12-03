using System.Collections.Generic;

namespace AdventOfCode2024;

[TestClass]
public class Day3
{
    private class Mul(int left, int right, bool enabled) {
        public bool Enabled => enabled;
        public int Value => left * right;
    }

    [TestMethod]
    public async Task Part1Async()
    {
        List<Mul> muls = new();
        string input = await File.ReadAllTextAsync("input/day3_p1.txt");
        int i = 0;
        while (i < input.Length)
        {
            if ((i + 3) < input.Length && input.Substring(i, 3).Equals("mul"))
            {
                i += 3;
                if (input[i] == '(')
                {
                    i += 1;
                    int end = i;
                    while (char.IsDigit(input[end]))
                    {
                        end++;
                    }
                    if (end != i)
                    {
                        int left = int.Parse(input[i..end] ?? "0");
                        i = end;
                        if (input[i] == ',')
                        {
                            i += 1;
                            end = i;
                            while (char.IsDigit(input[end]))
                            {
                                end++;
                            }
                            if (end != i)
                            {
                                int right = int.Parse(input[i..end] ?? "0");
                                i = end;
                                if (input[i] == ')')
                                {
                                    i++;
                                    muls.Add(new Mul(left, right, true));
                                }
                            }
                        }
                    }
                }
            }
            else {
                i++;
            }
        }
        Assert.AreEqual(muls.Sum(m => m.Value), 179571322);
    }

    [TestMethod]
    public async Task Part2Async()
    {        
        bool isEnabled = true;
        List<Mul> muls = new();
        string input = await File.ReadAllTextAsync("input/day3_p1.txt");
        int i = 0;
        while (i < input.Length)
        {
            if ((i + 3) < input.Length && input.Substring(i, 3).Equals("mul"))
            {
                i += 3;
                if (input[i] == '(')
                {
                    i += 1;
                    int end = i;
                    while (char.IsDigit(input[end]))
                    {
                        end++;
                    }
                    if (end != i)
                    {
                        int left = int.Parse(input[i..end] ?? "0");
                        i = end;
                        if (input[i] == ',')
                        {
                            i += 1;
                            end = i;
                            while (char.IsDigit(input[end]))
                            {
                                end++;
                            }
                            if (end != i)
                            {
                                int right = int.Parse(input[i..end] ?? "0");
                                i = end;
                                if (input[i] == ')')
                                {
                                    i++;
                                    muls.Add(new Mul(left, right, isEnabled));
                                }
                            }
                        }
                    }
                }
            }
            else if ((i + 4) < input.Length && input.Substring(i, 4).Equals("do()"))
            {
                isEnabled = true;
                i += 4;
            }
            else if ((i + 7) < input.Length && input.Substring(i, 7).Equals("don't()"))
            {
                isEnabled = false;
                i += 7;
            }
            else {
                i++;
            }
        }
        Assert.AreEqual(muls.Where(m => m.Enabled).Sum(m => m.Value), 103811193);
    }
}