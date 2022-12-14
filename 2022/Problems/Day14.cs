using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Problems
{
    [TestClass]
    public class Day14
    {
        public class Cave
        {
            public Dictionary<Tuple<int, int>, char> Grid;
            public int MinX;
            public int MaxX;
            public int MinY;
            public int MaxY;
            public bool Endless = false;

            public bool DropSand(Tuple<int, int> start)
            {
                bool dropping = true;
                while (dropping)
                {
                    Tuple<int, int> test = Tuple.Create(start.Item1, start.Item2 + 1);
                    if (Grid.TryGetValue(test, out char value))
                    {
                        if (value == '#' || value == 'o')
                        {
                            Tuple<int, int> left = Tuple.Create(test.Item1 - 1, test.Item2);
                            Tuple<int, int> right = Tuple.Create(test.Item1 + 1, test.Item2);
                            if (!Grid.TryGetValue(left, out char leftChar))
                            {
                                dropping = DropSand(left);
                            }
                            else if (!Grid.TryGetValue(right, out char rightChar))
                            {
                                dropping = DropSand(right);
                            }
                            else
                            {
                                Grid.Add(start, 'o');
                                dropping = false;
                            }
                        }
                    }
                    else
                    {
                        // Straight down
                        start = test;
                    }

                    if (start.Item2 >= this.MaxY + 5)
                    {
                        Endless = true;
                        return false;
                    }
                }

                return false;
            }
        }

        [TestMethod]
        public void Part1()
        {
            Dictionary<Tuple<int, int>, char> mapToType = new Dictionary<Tuple<int, int>, char>();
            int minX = int.MaxValue; // Grows left
            int maxX = int.MinValue;
            int minY = 0; 
            int maxY = int.MinValue; // Grows Down
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day14.txt")).Split("\r\n");
            foreach(string s in data)
            {
                List<Tuple<int, int>> list = new List<Tuple<int, int>>();
                string[] cords = s.Split("->");
                foreach(string cord in cords)
                {
                    string[] c = cord.Trim().Split(',');
                    int x = int.Parse(c[0]);
                    int y = int.Parse(c[1]);
                    list.Add(Tuple.Create(x, y));
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }

                for (int i = 0; i < list.Count - 1; i++)
                {
                    Tuple<int, int> cur = list[i];
                    Tuple<int, int> next = list[i + 1];

                    // Same Column
                    if (cur.Item1 == next.Item1)
                    {
                        int direction = cur.Item2 - next.Item2;
                        // Go down
                        if (direction < 0)
                        {
                            for (int j = cur.Item2; j <= next.Item2; j++)
                            {
                                mapToType.TryAdd(Tuple.Create(cur.Item1, j), '#');
                            }
                        }
                        // Go up
                        else if (direction > 0)
                        {
                            for (int j = cur.Item2; j >= next.Item2; j--)
                            {
                                mapToType.TryAdd(Tuple.Create(cur.Item1, j), '#');
                            }
                        }
                        else
                        {
                            throw new InvalidDataException("Are we a dot?");
                        }
                    }
                    // Same Row
                    else if (cur.Item2 == next.Item2)
                    {
                        int direction = cur.Item1 - next.Item1;
                        // Go right
                        if (direction < 0)
                        {
                            for (int j = cur.Item1; j <= next.Item1; j++)
                            {
                                mapToType.TryAdd(Tuple.Create(j, cur.Item2), '#');
                            }
                        }
                        // Go left
                        else if (direction > 0)
                        {
                            for (int j = cur.Item1; j >= next.Item1; j--)
                            {
                                mapToType.TryAdd(Tuple.Create(j, cur.Item2), '#');
                            }
                        }
                        else
                        {
                            throw new InvalidDataException("Are we a dot?");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("This is not a straight line.");
                    }
                }
            }

            Cave cave = new Cave()
            {
                Grid = mapToType,
                MaxX = maxX,
                MaxY = maxY,
                MinX = minX,
                MinY = minY
            };
            using StreamWriter file = new(Path.Combine(Directory.GetCurrentDirectory(), "input\\out14.txt"));

            int count = 0;
            while (!cave.DropSand(Tuple.Create(500, 0)))
            {
                if (cave.Endless)
                {
                    break;
                }
                count++;

                file.WriteLine($"Count {count}");
                file.Write(PrintGrid(cave));
            }

            file.Close();

            Assert.AreEqual(799, count);
        }

        public string PrintGrid(Cave c)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = c.MinY; i <= c.MaxY + 2; i++)
            {
                for (int j = c.MinX; j <= c.MaxX; j++)
                {
                    if (c.Grid.TryGetValue(Tuple.Create(j, i), out char value))
                    {
                        sb.Append(value);
                    }
                    else
                    {
                        sb.Append('.');
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        [TestMethod]
        public void Part2()
        {
            Dictionary<Tuple<int, int>, char> mapToType = new Dictionary<Tuple<int, int>, char>();
            int minX = int.MaxValue; // Grows left
            int maxX = int.MinValue;
            int minY = 0;
            int maxY = int.MinValue; // Grows Down
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day14.txt")).Split("\r\n");
            foreach (string s in data)
            {
                List<Tuple<int, int>> list = new List<Tuple<int, int>>();
                string[] cords = s.Split("->");
                foreach (string cord in cords)
                {
                    string[] c = cord.Trim().Split(',');
                    int x = int.Parse(c[0]);
                    int y = int.Parse(c[1]);
                    list.Add(Tuple.Create(x, y));
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }

                for (int i = 0; i < list.Count - 1; i++)
                {
                    Tuple<int, int> cur = list[i];
                    Tuple<int, int> next = list[i + 1];

                    // Same Column
                    if (cur.Item1 == next.Item1)
                    {
                        int direction = cur.Item2 - next.Item2;
                        // Go down
                        if (direction < 0)
                        {
                            for (int j = cur.Item2; j <= next.Item2; j++)
                            {
                                mapToType.TryAdd(Tuple.Create(cur.Item1, j), '#');
                            }
                        }
                        // Go up
                        else if (direction > 0)
                        {
                            for (int j = cur.Item2; j >= next.Item2; j--)
                            {
                                mapToType.TryAdd(Tuple.Create(cur.Item1, j), '#');
                            }
                        }
                        else
                        {
                            throw new InvalidDataException("Are we a dot?");
                        }
                    }
                    // Same Row
                    else if (cur.Item2 == next.Item2)
                    {
                        int direction = cur.Item1 - next.Item1;
                        // Go right
                        if (direction < 0)
                        {
                            for (int j = cur.Item1; j <= next.Item1; j++)
                            {
                                mapToType.TryAdd(Tuple.Create(j, cur.Item2), '#');
                            }
                        }
                        // Go left
                        else if (direction > 0)
                        {
                            for (int j = cur.Item1; j >= next.Item1; j--)
                            {
                                mapToType.TryAdd(Tuple.Create(j, cur.Item2), '#');
                            }
                        }
                        else
                        {
                            throw new InvalidDataException("Are we a dot?");
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("This is not a straight line.");
                    }
                }
            }

            Cave cave = new Cave()
            {
                Grid = mapToType,
                MaxX = maxX,
                MaxY = maxY,
                MinX = minX,
                MinY = minY
            };
            for (int i = minX - 100000; i<= maxX + 100000; i++)
            {
                cave.Grid.Add(Tuple.Create(i, maxY + 2), '#');
            }
            using StreamWriter file = new(Path.Combine(Directory.GetCurrentDirectory(), "input\\out14.txt"));

            int count = 0;
            while (!cave.DropSand(Tuple.Create(500, 0)))
            {
                count++;
                if (cave.Grid.TryGetValue(Tuple.Create(500, 0), out char value))
                {
                    break;
                }

                file.WriteLine($"Count {count}");
                file.Write(PrintGrid(cave));
            }

            file.Close();

            Assert.AreEqual(0, count);
        }
    }
}