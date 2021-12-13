using System.Diagnostics;

namespace AdventOfCode2021.Day13
{
    public class Main
    {
        public abstract class Fold
        {
            public int Index;
        }
        public class XFold : Fold
        {
        }
        public class YFold : Fold
        {
        }

        public class Paper
        {
            public int[,] Grid;
            public Queue<Fold> Folds;

            public Paper(string[] input)
            {
                Folds = new();

                List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
                int maxX = int.MinValue;
                int maxY = int.MinValue;
                foreach (string s in input)
                {
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        continue;
                    }
                    if (s.StartsWith("fold along"))
                    {
                        string fold = s.Substring(11);
                        string[] foldArr = fold.Split("=");
                        if (foldArr[0] == "x")
                        {
                            Folds.Enqueue(new XFold() { Index = int.Parse(foldArr[1]) });
                        }
                        else if (foldArr[0] == "y")
                        {
                            Folds.Enqueue(new YFold() { Index = int.Parse(foldArr[1]) });
                        }
                    }
                    else
                    {
                        string[] pair = s.Split(",");
                        int x = int.Parse(pair[0]); // X goes to the right
                        int y = int.Parse(pair[1]); // Y goes down
                        pairs.Add(Tuple.Create(y, x));

                        if (x > maxX)
                        {
                            maxX = x;
                        }
                        if (y > maxY)
                        {
                            maxY = y;
                        }
                    }
                }

                Grid = new int[maxY + 1, maxX + 1];

                foreach (Tuple<int, int> tuple in pairs)
                {
                    Grid[tuple.Item1, tuple.Item2] = 1;
                }

                // PrintGrid();
            }


            public void PrintGrid()
            {
                for (int i = 0; i < Grid.GetLength(0); i++)
                {
                    for (int j = 0; j < Grid.GetLength(1); j++)
                    {
                        Debug.Write(Grid[i, j]);
                    }
                    Debug.WriteLine("");
                }
            }

            public bool CanFold => Folds.Count > 0;

            public int DotsVisable()
            {
                int count = 0;

                for (int y = 0; y < Grid.GetLength(0); y++)
                {
                    for (int x = 0; x < Grid.GetLength(1); x++)
                    {
                        count += Grid[y, x];
                    }
                }

                return count;
            }

            public void Fold()
            {
                Debug.Assert(Folds.Count > 0);
                Fold f = Folds.Dequeue();
                if (f is XFold)
                {
                    int index = f.Index;
                    int[,] newGrid = new int[Grid.GetLength(0), index];
                    for (int y = 0; y < Grid.GetLength(0); y++)
                    {
                        for (int x = 0; x < index; x++)
                        {
                            newGrid[y, x] = Grid[y, x] | Grid[y, Grid.GetLength(1) - x - 1];
                        }
                    }

                    Grid = newGrid;
                }
                else if (f is YFold)
                {
                    int index = f.Index;
                    int[,] newGrid = new int[index, Grid.GetLength(1)];
                    for (int y = 0; y < index; y++)
                    {
                        for (int x = 0; x < Grid.GetLength(1); x++)
                        {
                            newGrid[y, x] = Grid[y, x] | Grid[Grid.GetLength(0) - y - 1, x];
                        }
                    }

                    Grid = newGrid;
                }
            }
        }

        [Fact]
        public async void Part1()
        {
            string[] data = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Paper paper = new Paper(data);

            paper.Fold();

            Assert.Equal(0, paper.DotsVisable());
        }

        [Fact]
        public async void Part2()
        {
            string[] data = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Paper paper = new Paper(data);

            while (paper.CanFold)
            {
                paper.Fold();
            }

            paper.PrintGrid(); // Take output and make it visable.
        }
    }
}