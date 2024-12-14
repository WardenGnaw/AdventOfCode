using ScottPlot;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

[TestClass]
public class Day14
{
    class Robot
    {
        public Tuple<int, int> Position { get; set; }

        Tuple<int, int> Velocity { get; set; }

        public Robot(Tuple<int, int> p, Tuple<int, int> v)
        {
            Position = p;
            Velocity = v;
        }

        public void Walk(int x, int y)
        {
            var newX = Position.Item1 + Velocity.Item1;
            while (newX < 0)
            {
                newX += x;
            }
            while (newX >= x)
            {
                newX -= x;
            }
            var newY = Position.Item2 + Velocity.Item2;
            while (newY < 0)
            {
                newY += y;
            }
            while (newY >= y)
            {
                newY -= y;
            }
            Position = Tuple.Create(newX, newY);
        }
    }

    [TestMethod]
    public async Task Part1Async()
    {
        int x = 101;
        int y = 103;
        List<Robot> robots = new List<Robot>();
        string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
        Regex regex = new Regex(pattern);
        string[] input = (await File.ReadAllTextAsync("input/Day14.txt")).Split('\n');
        foreach (var item in input) {
            var data = item.Trim();
            var match = regex.Match(data);
            if (match.Success)
            {
                robots.Add(new Robot(Tuple.Create(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), Tuple.Create(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
            }
        }
        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in robots)
            {
                robot.Walk(x, y);
            }
        }

        ulong topleft = 0;
        ulong topright = 0;
        ulong bottomleft = 0;
        ulong bottomright = 0;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                foreach(var robot in robots)
                {
                    if (robot.Position.Item1 == i && robot.Position.Item2 == j)
                    {
                        if (i < x/2 && j < y/2)
                        {
                            topleft++;
                        }
                        if (i > x / 2 && j < y / 2)
                        {
                            topright++;
                        }
                        if (i < x / 2 && j > y / 2)
                        {
                            bottomleft++;
                        }
                        if (i > x / 2 && j > y / 2)
                        {
                            bottomright++;
                        }
                    }
                }
            }
        }
        Assert.AreEqual(topleft * topright * bottomleft * bottomright, (ulong)36838);
    }


    [TestMethod]
    public async Task Part2Async()
    {
        int x = 101;
        int y = 103;
        List<Robot> robots = new List<Robot>();
        string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
        Regex regex = new Regex(pattern);
        string[] input = (await File.ReadAllTextAsync("input/Day14.txt")).Split('\n');
        foreach (var item in input)
        {
            var data = item.Trim();
            var match = regex.Match(data);
            if (match.Success)
            {
                robots.Add(new Robot(Tuple.Create(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)), Tuple.Create(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))));
            }
        }
        ulong count = 0;
        while (true)
        {
            count++;
            foreach (var robot in robots)
            {
                robot.Walk(x, y);
            }

            if (count == 7132)
            {
                Plot p = new Plot();
                var markers = p.Add.Markers(robots.Select(x => x.Position.Item1).ToArray(), robots.Select(x => x.Position.Item2).ToArray(), color: Color.FromHex("00FF00"));
                markers.Axes.XAxis = p.Axes.Top;
                p.Grid.XAxis = p.Axes.Top;
                p.Axes.SetLimitsY(100, 0);
                p.ShowLegend();
                p.SaveJpeg($"{count}.jpg", 1024, 512);
                break;
            }
        }
        Assert.AreEqual(count, (ulong)7132);
    }
}