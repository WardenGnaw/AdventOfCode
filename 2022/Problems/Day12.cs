using System.Linq;

namespace Problems
{
    [TestClass]
    public class Day12
    {
        public class Route
        {
            public HashSet<Tuple<int, int>> Seen = new HashSet<Tuple<int, int>>();
            public List<Tuple<int, int>> Path = new List<Tuple<int, int>>();

            public int Row { get; set; }
            public int Col { get; set; }
        }

        public class Grid
        {
            public char[,] Map { get; set; }
            public int MaxRow { get; set; }
            public int MaxCol { get; set; }
            public Tuple<int, int> Start { get; set; }
            public Tuple<int, int> End { get; set; }
            public HashSet<Tuple<int, int>> Seen = new HashSet<Tuple<int, int>>();


            public Grid() { }

            public List<Route> Traverse()
            {
                List<Route> endRoutes = new List<Route>();
                Queue<Route> routes = new Queue<Route>();
                routes.Enqueue(new Route()
                {
                    Path = new List<Tuple<int, int>>() { Start },
                    Seen = new HashSet<Tuple<int, int>>() { Start },
                    Row = Start.Item1,
                    Col = Start.Item2
                });
                while (routes.Any())
                {
                    Route route = routes.Dequeue();
                    var curPath = Tuple.Create(route.Row, route.Col);
                    if (this.Seen.Contains(curPath))
                    {
                        continue;
                    }
                    this.Seen.Add(curPath);

                    if (route.Row == End.Item1 && route.Col == End.Item2) 
                    {
                        endRoutes.Add(route);
                    }

                    // Up
                    if (route.Row - 1 >= 0 && ((Map[route.Row - 1, route.Col] - Map[route.Row, route.Col]) <= 1))
                    {
                        var cur = Tuple.Create(route.Row - 1, route.Col);
                        if (!route.Seen.Contains(cur))
                        {
                            Route r = new Route()
                            {
                                Path = new List<Tuple<int, int>>(route.Path),
                                Seen = new HashSet<Tuple<int, int>>(route.Seen),
                                Row = route.Row - 1,
                                Col = route.Col
                            };

                            r.Path.Add(cur);
                            r.Seen.Add(cur);

                            routes.Enqueue(r);
                        }
                    }

                    // Down
                    if (route.Row + 1 < MaxRow && ((Map[route.Row + 1, route.Col] - Map[route.Row, route.Col]) <= 1))
                    {
                        var cur = Tuple.Create(route.Row + 1, route.Col);
                        if (!route.Seen.Contains(cur))
                        {
                            Route r = new Route()
                            {
                                Path = new List<Tuple<int, int>>(route.Path),
                                Seen = new HashSet<Tuple<int, int>>(route.Seen),
                                Row = route.Row + 1,
                                Col = route.Col
                            };

                            r.Path.Add(cur);
                            r.Seen.Add(cur);

                            routes.Enqueue(r);
                        }
                    }

                    // Left
                    if (route.Col - 1 >= 0 && ((Map[route.Row, route.Col - 1] - Map[route.Row, route.Col]) <= 1))
                    {
                        var cur = Tuple.Create(route.Row, route.Col - 1);
                        if (!route.Seen.Contains(cur))
                        {
                            Route r = new Route()
                            {
                                Path = new List<Tuple<int, int>>(route.Path),
                                Seen = new HashSet<Tuple<int, int>>(route.Seen),
                                Row = route.Row,
                                Col = route.Col - 1
                            };

                            r.Path.Add(cur);
                            r.Seen.Add(cur);

                            routes.Enqueue(r);
                        }
                    }

                    // Right
                    if (route.Col + 1 < MaxCol && ((Map[route.Row, route.Col + 1] - Map[route.Row, route.Col]) <= 1))
                    {
                        var cur = Tuple.Create(route.Row, route.Col + 1);
                        if (!route.Seen.Contains(cur))
                        {
                            Route r = new Route()
                            {
                                Path = new List<Tuple<int, int>>(route.Path),
                                Seen = new HashSet<Tuple<int, int>>(route.Seen),
                                Row = route.Row,
                                Col = route.Col + 1
                            };

                            r.Path.Add(cur);
                            r.Seen.Add(cur);

                            routes.Enqueue(r);
                        }
                    }
                }
                return endRoutes;
            }
        }

        [TestMethod]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day12.txt")).Split("\r\n");
            int rows = data.Length;
            int cols = data[0].Length;
            char[,] grid = new char[rows, cols];
            Tuple<int, int> start = null;
            Tuple<int, int> end = null;
            for (int i = 0; i < rows; i++)
            {
                char[] rowData = data[i].Trim().ToCharArray();
                for (int j = 0; j < cols; j++)
                {
                    if (rowData[j] == 'S')
                    {
                        start = Tuple.Create(i, j);
                        grid[i, j] = 'a';
                    }
                    else if (rowData[j] == 'E')
                    {
                        end = Tuple.Create(i, j);
                        grid[i, j] = 'z';
                    }
                    else
                    {
                        grid[i, j] = rowData[j];
                    }
                }
            }

            Grid g = new Grid()
            {
                Map = grid,
                MaxCol = cols,
                MaxRow = rows,
                Start = start,
                End = end
            };
            List<Route> r = g.Traverse();

            Route finalRoute = r.OrderBy(x => x.Path.Count).First();


            Assert.AreEqual(0, finalRoute.Path.Count - 1);
        }

        [TestMethod]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day12.txt")).Split("\r\n");
            int rows = data.Length;
            int cols = data[0].Length;
            char[,] grid = new char[rows, cols];
            Tuple<int, int> end = null;
            List<Tuple<int, int>> starts = new List<Tuple<int, int>>();
            for (int i = 0; i < rows; i++)
            {
                char[] rowData = data[i].Trim().ToCharArray();
                for (int j = 0; j < cols; j++)
                {
                    if (rowData[j] == 'S' || rowData[j] == 'a')
                    {
                        starts.Add(Tuple.Create(i, j));
                        grid[i, j] = 'a';
                    }
                    else if (rowData[j] == 'E')
                    {
                        end = Tuple.Create(i, j);
                        grid[i, j] = 'z';
                    }
                    else
                    {
                        grid[i, j] = rowData[j];
                    }
                }
            }

            List<Route> r = new List<Route>();

            foreach (Tuple<int, int> start in starts)
            {
                Grid g = new Grid()
                {
                    Map = grid,
                    MaxCol = cols,
                    MaxRow = rows,
                    Start = start,
                    End = end
                };
                r.AddRange(g.Traverse());

            }

            Route finalRoute = r.OrderBy(x => x.Path.Count).First();

            Assert.AreEqual(0, finalRoute.Path.Count - 1);
        }
    }
}