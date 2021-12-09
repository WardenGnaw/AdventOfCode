using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Day9
{
    public class Basin
    {
        List<Node> nodes = new List<Node>();
        int MaxRow;
        int MaxColumn;
        private Dictionary<Tuple<int, int>, Node> Grid;

        public Basin(Node startNode, Dictionary<Tuple<int, int>, Node> grid, int maxRow, int maxCol)
        {
            MaxRow = maxRow;
            MaxColumn = maxCol;
            Grid = grid;
            FloodFill(startNode.X, startNode.Y);
        }

        // Recursively look around neighboring nodes and add to basin.
        private void FloodFill(int x, int y)
        {
            Node cur = Grid[Tuple.Create(x, y)];

            // Stop if it is max height (9) or is already marked in a basin.
            if (cur.Height == 9 || cur.IsInBasin)
            {
                return;
            }

            nodes.Add(cur);
            cur.AddToBasin(this);

            if (x > 0)
            {
                FloodFill(x - 1, y);
            }

            if (x < (MaxRow - 1))
            {
                FloodFill(x + 1, y);
            }

            if (y > 0)
            {
                FloodFill(x, y - 1);
            }

            if (y < (MaxColumn - 1))
            {
                FloodFill(x, y + 1);
            }
        }

        public int Size => nodes.Count;
    }

    public class Node
    {
        public int X;
        public int Y;
        public int Height;
        private Dictionary<Tuple<int, int>, Node> Grid;
        private Basin? basin;

        public Node(int x, int y, int height,  Dictionary<Tuple<int, int>, Node> grid)
        {
            X = x;
            Y = y;
            Height = height;
            Grid = grid;
            basin = null;
        }

        public void AddToBasin(Basin b)
        {
            this.basin = b;
        }

        public bool IsLowPoint(int maxRow, int maxCol)
        {
            bool isLow = true;

            if (X > 0)
            {
                isLow &= Height < Grid[Tuple.Create(X - 1, Y)].Height;
            }

            if (X < (maxRow - 1))
            {
                isLow &= Height < Grid[Tuple.Create(X + 1, Y)].Height;
            }

            if (Y > 0)
            {
                isLow &= Height < Grid[Tuple.Create(X, Y - 1)].Height;
            }

            if (Y < (maxCol - 1))
            {
                isLow &= Height < Grid[Tuple.Create(X, Y + 1)].Height;
            }

            return isLow;
        }

        public int Risk => Height + 1;

        public bool IsInBasin => basin != null;

    }

    public class Main
    {
        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Dictionary<Tuple<int, int>, Node> grid = new Dictionary<Tuple<int, int>, Node>();

            string[] lines = data.Split("\r\n");
            int maxRow = int.MinValue;
            int i = 0;
            foreach (string line in lines)
            {
                int[] numbers = line.ToList().Select(x => int.Parse(x.ToString())).ToArray();
                maxRow = numbers.Length;
                for (int j = 0; j < numbers.Length; j++)
                {
                    grid.Add(Tuple.Create(i, j), new Node(i, j, numbers[j], grid));
                }
                i++;
            }

            int risk = 0;

            foreach(var node in grid.Values)
            {
                if (node.IsLowPoint(i, maxRow))
                {
                    risk += node.Risk;
                }
            }

            Assert.Equal(0, risk);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Dictionary<Tuple<int, int>, Node> grid = new Dictionary<Tuple<int, int>, Node>();

            string[] lines = data.Split("\r\n");
            int maxRow = int.MinValue;
            int i = 0;
            foreach (string line in lines)
            {
                int[] numbers = line.ToList().Select(x => int.Parse(x.ToString())).ToArray();
                maxRow = numbers.Length;
                for (int j = 0; j < numbers.Length; j++)
                {
                    grid.Add(Tuple.Create(i, j), new Node(i, j, numbers[j], grid));
                }
                i++;
            }

            List<Node> lowPoints = new();

            foreach (var node in grid.Values)
            {
                if (node.IsLowPoint(i, maxRow))
                {
                    lowPoints.Add(node);
                }
            }

            List<Basin> basins = new();

            // All basins flow to a low point so start from low point.
            foreach(Node n in lowPoints)
            {
                if (!n.IsLowPoint(i, maxRow) || n.IsInBasin)
                {
                    continue;
                }
                Basin b = new Basin(n, grid, i, maxRow);

                basins.Add(b);
            }

            // Sort from greatest to least
            basins.Sort((a, b) => b.Size - a.Size);

            // Take the top 3 and multiply
            Assert.Equal(0, basins.Take(3).Aggregate(1, (count, b) => count * b.Size));
        }
    }
}