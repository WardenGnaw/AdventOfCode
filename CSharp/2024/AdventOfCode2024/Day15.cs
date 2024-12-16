using ScottPlot;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2024;

[TestClass]
public class Day15
{
    public class GraphA
    {
        public (int X, int Y) Robot { get; set; }
        public HashSet<(int X, int Y)> Boxes = new();
        public HashSet<(int X, int Y)> Wall = new();

        public void PrintGraph(int maxX, int maxY)
        {
            for (int x= 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (Robot == (x, y))
                    {
                        Console.Write("@");
                    }
                    else if (Boxes.Contains((x, y)))
                    {
                        Console.Write("O");
                    }
                    else if (Wall.Contains((x, y)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        public void Move(char c)
        {
            (int X, int Y) next = (0, 0);
            switch (c)
            {
                case '<':
                    next = (0, -1);
                    break;
                case '>':
                    next = (0, 1);
                    break;
                case 'v':
                    next = (1, 0);
                    break;
                case '^':
                    next = (-1, 0);
                    break;
                default:
                    throw new Exception();
            }

            var candidate = (Robot.X + next.X, Robot.Y + next.Y);

            if (Wall.Contains(candidate))
            {
                return;
            }

            if (Boxes.Contains(candidate))
            {
                (int X, int Y) box = candidate;
                do
                {
                    box = (box.X + next.X,  box.Y + next.Y);
                } while (Boxes.Contains(box));
                if (Wall.Contains(box))
                {
                    // Do nothing
                }
                else
                {
                    Boxes.Remove(candidate);
                    Robot = candidate;
                    Boxes.Add(box);
                }
                return;
            }

            Robot = (Robot.X + next.X, Robot.Y + next.Y);
        }
    }

    public class BoxW
    {
        public (int X, int Y) Left { get; set; }
        public (int X, int Y) Right { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is BoxW other)
            {
                return Left.Equals(other.Left) && Right.Equals(other.Right);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Right.GetHashCode();
        }
    }

    public class BoxWComparer : IEqualityComparer<BoxW>
    {
        public bool Equals(BoxW b1, BoxW b2)
        {
            if (b1 == null && b2 == null)
                return true;
            if (b1 == null || b2 == null)
                return false;
            return b1.Left.Equals(b2.Left) && b1.Right.Equals(b2.Right);
        }

        public int GetHashCode(BoxW b)
        {
            if (b == null)
                return 0;
            return b.Left.GetHashCode() ^ b.Right.GetHashCode();
        }
    }


    public class GraphW
    {
        public (int X, int Y) Robot { get; set; }
        public HashSet<BoxW> Boxes = new(new BoxWComparer());
        public HashSet<(int X, int Y)> Wall = new();

        public void PrintGraph(int maxX, int maxY)
        {
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    if (Robot == (x, y))
                    {
                        Console.Write("@");
                    }
                    else if (Boxes.Where(b => b.Left == (x, y)).Any())
                    {
                        Console.Write("[");
                    }
                    else if (Boxes.Where(b => b.Right == (x, y)).Any())
                    {
                        Console.Write("]");
                    }
                    else if (Wall.Contains((x, y)))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    [TestMethod]
    public async Task Part1Async()
    {
        bool isMap = true;
        var graph = new GraphA();
        string moves = string.Empty;
        string[] input = (await File.ReadAllTextAsync("input/Day15.cs")).Split("\r\n");
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                isMap = false;
            }
            if (isMap)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        graph.Wall.Add((i, j));
                    }
                    else if (input[i][j] == '@')
                    {
                        graph.Robot = (i, j);
                    }
                    else if (input[i][j] == 'O')
                    {
                        graph.Boxes.Add((i, j));
                    }
                }
            }
            else
            {
                moves += input[i].Trim();
            }
        }

        foreach (char c in moves)
        {
            graph.Move(c);
            //Console.WriteLine($"Move {c}:");
            //graph.PrintGraph(input.Length - 2, input[0].Length);
        }

        int total = 0;

        foreach (var box in graph.Boxes)
        {
            total += box.X * 100 + box.Y;
        }

        Assert.AreEqual(total, 1505963);
    }


    [TestMethod]
    public async Task Part2Async()
    {
        bool isMap = true;
        var graph = new GraphW();
        string moves = string.Empty;
        string[] input = (await File.ReadAllTextAsync("input/Day15.cs")).Split("\r\n");
        for (int i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrEmpty(input[i]))
            {
                isMap = false;
            }
            if (isMap)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#')
                    {
                        graph.Wall.Add((i, j * 2));
                        graph.Wall.Add((i, j * 2 + 1));
                    }
                    else if (input[i][j] == '@')
                    {
                        graph.Robot = (i, j * 2);
                    }
                    else if (input[i][j] == 'O')
                    {
                        graph.Boxes.Add(new BoxW()
                        {
                            Left = (i, j * 2),
                            Right = (i, j * 2 + 1)
                        });
                    }
                }
            }
            else
            {
                moves += input[i].Trim();
            }
        }

        foreach (char c in moves)
        {
            //graph.Move(c);
            Console.WriteLine($"Move {c}:");
            graph.PrintGraph(input.Length - 2, input[0].Length * 2);
        }

        int total = 0;

        foreach (var box in graph.Boxes.Select(x => x.Left))
        {
            total += box.X * 100 + box.Y;
        }

        Assert.AreEqual(total, 0);
    }
}