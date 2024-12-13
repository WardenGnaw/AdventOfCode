using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

[TestClass]
public class Day13
{
    class Claw<T>
    {
        public Tuple<T, T> A;
        public Tuple<T, T> B;
        public Tuple<T, T> Prize;
    }

    // Math ¯\_(ツ)_/¯

    // a = aX
    // b = bX
    // c = aY
    // d = bY

    // prizeX = a * aPresses + c * bPresses
    // prizeY = b * aPresses + d * bPresses

    // Multiply d
    // d * prizeX = d * a * aPresses + c * d * bPresses

    // Multiple c
    // c * prizeY = b * c * aPresses + c * d * bPresses

    // Subtract equations
    // d * prizeX - c * prizeY = d * a * aPresses - b * c * aPresses + c * d * bPresses - c * d * bPresses

    // c * d * bPresses - c * d * bPresses == 0
    // d * prizeX - c * prizeY = d * a * aPresses - b * c * aPresses

    // Results
    // aPresses = (d * prizeX - b * prizeY) / (d * a - b * c)
    // bPresses = (prizeX - a * aPresses) / b


    Tuple<int, int> SolveClawMachine(int a, int c, int b, int d, int prizeX, int prizeY)
    {
        int aPresses = (d * prizeX - b * prizeY) / (d * a - b * c);
        int bPresses = (prizeX - a * aPresses) / b;

        return Tuple.Create(aPresses, bPresses);
    }

    Tuple<BigInteger, BigInteger> SolveBigClawMachine(BigInteger a, BigInteger c, BigInteger b, BigInteger d, BigInteger prizeX, BigInteger prizeY)
    {
        BigInteger aPresses = (d * prizeX - b * prizeY) / (d * a - b * c);
        BigInteger bPresses = (prizeX - a * aPresses) / b;

        return Tuple.Create(aPresses, bPresses);
    }

    [TestMethod]
    public async Task Part1Async()
    {
        List<Claw<int>> claws = new List<Claw<int>>();
        string input = await File.ReadAllTextAsync("input/Day13.txt");
        Claw<int> currClaw = new Claw<int>();
        string buttonPattern = @"Button [A|B]: X\+(\d+), Y\+(\d+)";
        string prizePattern = @"Prize: X=(\d+), Y=(\d+)";
        foreach (var item in input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            if (string.IsNullOrEmpty(item))
            {
                continue;
            }

            if (item.StartsWith("Button A:"))
            {
                MatchCollection matches = Regex.Matches(item, buttonPattern);
                if (matches.Count > 0)
                {
                    currClaw.A = Tuple.Create(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value));
                }
            }
            else if (item.StartsWith("Button B:"))
            {
                MatchCollection matches = Regex.Matches(item, buttonPattern);
                if (matches.Count > 0)
                {
                    currClaw.B = Tuple.Create(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value));
                }
            }
            else if (item.StartsWith("Prize:"))
            {
                MatchCollection matches = Regex.Matches(item, prizePattern);
                if (matches.Count > 0)
                {
                    currClaw.Prize = Tuple.Create(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value));
                    claws.Add(currClaw);
                    currClaw = new();
                }
            }
        }

        int tokens = 0;

        foreach (var claw in claws)
        {
            var presses = SolveClawMachine(claw.A.Item1, claw.A.Item2, claw.B.Item1, claw.B.Item2, claw.Prize.Item1, claw.Prize.Item2);

            int totalCost = presses.Item1 * 3 + presses.Item2 * 1;

            // Test to see if the presses that were calculated made it
            int finalX = presses.Item1 * claw.A.Item1 + presses.Item2 * claw.B.Item1;
            int finalY = presses.Item1 * claw.A.Item2 + presses.Item2 * claw.B.Item2;

            bool success = finalX == claw.Prize.Item1 && finalY == claw.Prize.Item2;
            if (success)
            {
                tokens += totalCost;
            }
        }

        Assert.AreEqual(tokens, 36838);
    }


    [TestMethod]
    public async Task Part2Async()
    {
        List<Claw<BigInteger>> claws = new List<Claw<BigInteger>>();
        string input = await File.ReadAllTextAsync("input/Day13.txt");
        Claw<BigInteger> currClaw = new Claw<BigInteger>();
        string buttonPattern = @"Button [A|B]: X\+(\d+), Y\+(\d+)";
        string prizePattern = @"Prize: X=(\d+), Y=(\d+)";
        foreach (var item in input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
        {
            if (string.IsNullOrEmpty(item))
            {
                continue;
            }

            if (item.StartsWith("Button A:"))
            {
                MatchCollection matches = Regex.Matches(item, buttonPattern);
                if (matches.Count > 0)
                {
                    currClaw.A = Tuple.Create(BigInteger.Parse(matches[0].Groups[1].Value), BigInteger.Parse(matches[0].Groups[2].Value));
                }
            }
            else if (item.StartsWith("Button B:"))
            {
                MatchCollection matches = Regex.Matches(item, buttonPattern);
                if (matches.Count > 0)
                {
                    currClaw.B = Tuple.Create(BigInteger.Parse(matches[0].Groups[1].Value), BigInteger.Parse(matches[0].Groups[2].Value));
                }
            }
            else if (item.StartsWith("Prize:"))
            {
                MatchCollection matches = Regex.Matches(item, prizePattern);
                if (matches.Count > 0)
                {
                    currClaw.Prize = Tuple.Create(BigInteger.Parse(matches[0].Groups[1].Value) + 10000000000000, BigInteger.Parse(matches[0].Groups[2].Value) + 10000000000000);
                    claws.Add(currClaw);
                    currClaw = new();
                }
            }
        }

        BigInteger tokens = 0;

        foreach (var claw in claws)
        {
            var presses = SolveBigClawMachine(claw.A.Item1, claw.A.Item2, claw.B.Item1, claw.B.Item2, claw.Prize.Item1, claw.Prize.Item2);

            BigInteger totalCost = presses.Item1 * 3 + presses.Item2 * 1;

            // Test to see if the presses that were calculated made it
            BigInteger finalX = presses.Item1 * claw.A.Item1 + presses.Item2 * claw.B.Item1;
            BigInteger finalY = presses.Item1 * claw.A.Item2 + presses.Item2 * claw.B.Item2;

            bool success = finalX == claw.Prize.Item1 && finalY == claw.Prize.Item2;
            if (success)
            {
                tokens += totalCost;
            }
        }

        Assert.AreEqual(tokens, 83029436920891);
    }
}