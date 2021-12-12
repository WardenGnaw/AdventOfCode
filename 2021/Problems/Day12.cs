using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Day12
{
    public abstract class Cave
    {
        public abstract string Name { get; }
        public List<Cave> Paths { get; } = new List<Cave>();
    }

    public class Start : Cave
    {
        public override string Name { get => "start"; }
    }

    public class End : Cave
    {
        public override string Name { get => "end"; }
    }

    public class SmallCave : Cave 
    {
        public override string Name { get; }

        public SmallCave(string name)
        {
            Name = name;
        }
    }

    public class BigCave : Cave
    {
        public override string Name { get; }

        public BigCave(string name)
        {
            Name = name;
        }
    }

    public class CavePath
    {
        public List<Cave> Paths { get; set; }
        public Dictionary<string, int> seenCaves { get; set; }
    }

    public class Main
    {
        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Dictionary<string, Cave> caves = new();
            caves.Add("start", new Start());
            caves.Add("end", new End());

            foreach (string s in data.Split("\r\n"))
            {
                string[] chain = s.Split('-');
                string left = chain[0];

                Cave leftCave;
                if (left.All(char.IsLower))
                {
                    if (!caves.TryGetValue(left, out leftCave))
                    {
                        leftCave = new SmallCave(left);
                        caves.Add(left, leftCave);
                    }
                }
                else if (left.All(char.IsUpper))
                    {
                    if (!caves.TryGetValue(left, out leftCave))
                    {
                        leftCave = new BigCave(left);
                        caves.Add(left, leftCave);
                    }
                }
                else
                {
                    caves.TryGetValue(left, out leftCave);
                }

                string right = chain[1];
                Cave rightCave;
                if (right.All(char.IsLower))
                    {
                    if (!caves.TryGetValue(right, out rightCave))
                    {
                        rightCave = new SmallCave(right);
                        caves.Add(right, rightCave);
                    }
                }
                else if (right.All(char.IsUpper))
                {
                    if (!caves.TryGetValue(right, out rightCave))
                    {
                        rightCave = new BigCave(right);
                        caves.Add(right, rightCave);
                    }
                }
                else
                {
                    caves.TryGetValue("right", out rightCave);
                }

                if (leftCave != null && rightCave != null)
                {
                    leftCave.Paths.Add(rightCave);
                    if (rightCave is not End)
                    {
                        rightCave.Paths.Add(leftCave);
                    }
                }
                else
                {
                    throw new Exception("Invalid line: " + s);
                }
            }

            Queue<CavePath> queue = new();
            CavePath p = new CavePath()
            {
                Paths = new List<Cave>() { caves["start"] },
                seenCaves = new()
            };
            queue.Enqueue(p);

            List<CavePath> endPaths = new List<CavePath>();

            while (queue.Count > 0)
            {
                CavePath path = queue.Dequeue();

                Cave lastPathCave = path.Paths.Last();
                if (lastPathCave is End)
                {
                    endPaths.Add(path);
                }
                else
                {
                    foreach (Cave c in lastPathCave.Paths)
                    {
                        if (c is Start)
                        {
                            continue;
                        }
                        if (c is SmallCave && path.seenCaves.ContainsKey(c.Name))
                        {
                            continue;
                        }

                        List<Cave> nextPaths = new List<Cave>(path.Paths);
                        nextPaths.Add(c);

                        Dictionary<string, int> nextSeenCaves = new Dictionary<string, int>(path.seenCaves);
                        nextSeenCaves.Add(c.Name, 1);
                        queue.Enqueue(new CavePath()
                        {
                            Paths = nextPaths,
                            seenCaves = nextSeenCaves
                        });
                    }
                }
            }


            Assert.Equal(0, endPaths.Count);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            Dictionary<string, Cave> caves = new();
            caves.Add("start", new Start());
            caves.Add("end", new End());

            foreach (string s in data.Split("\r\n"))
            {
                string[] chain = s.Split('-');
                string left = chain[0];

                Cave leftCave;
                if (left.All(char.IsLower))
                {
                    if (!caves.TryGetValue(left, out leftCave))
                    {
                        leftCave = new SmallCave(left);
                        caves.Add(left, leftCave);
                    }
                }
                else if (left.All(char.IsUpper))
                {
                    if (!caves.TryGetValue(left, out leftCave))
                    {
                        leftCave = new BigCave(left);
                        caves.Add(left, leftCave);
                    }
                }
                else
                {
                    caves.TryGetValue(left, out leftCave);
                }

                string right = chain[1];
                Cave rightCave;
                if (right.All(char.IsLower))
                {
                    if (!caves.TryGetValue(right, out rightCave))
                    {
                        rightCave = new SmallCave(right);
                        caves.Add(right, rightCave);
                    }
                }
                else if (right.All(char.IsUpper))
                {
                    if (!caves.TryGetValue(right, out rightCave))
                    {
                        rightCave = new BigCave(right);
                        caves.Add(right, rightCave);
                    }
                }
                else
                {
                    caves.TryGetValue("right", out rightCave);
                }

                if (leftCave != null && rightCave != null)
                {
                    leftCave.Paths.Add(rightCave);
                    if (rightCave is not End)
                    {
                        rightCave.Paths.Add(leftCave);
                    }
                }
                else
                {
                    throw new Exception("Invalid line: " + s);
                }
            }

            Queue<CavePath> queue = new();
            CavePath p = new CavePath()
            {
                Paths = new List<Cave>() { caves["start"] },
                seenCaves = new()
            };
            queue.Enqueue(p);

            List<CavePath> endPaths = new List<CavePath>();

            while (queue.Count > 0)
            {
                CavePath path = queue.Dequeue();

                Cave lastPathCave = path.Paths.Last();
                if (lastPathCave is End)
                {
                    endPaths.Add(path);
                }
                else
                {
                    foreach (Cave c in lastPathCave.Paths)
                    {
                        if (c is Start)
                        {
                            continue;
                        }

                        List<Cave> nextPaths = new List<Cave>(path.Paths);
                        nextPaths.Add(c);

                        Dictionary<string, int> nextSeenCaves = new Dictionary<string, int>(path.seenCaves);
                        if (c is SmallCave)
                        {
                            if (nextSeenCaves.TryGetValue(c.Name, out int value))
                            {
                                // Make sure you are the first small cave to be visited twice.
                                if (path.seenCaves.Values.Where(x => x > 1).Count() > 0)
                                {
                                    continue;
                                }
                                nextSeenCaves[c.Name] = 2;
                            }
                            else
                            {
                                nextSeenCaves.Add(c.Name, 1);
                            }
                        }
                        queue.Enqueue(new CavePath()
                        {
                            Paths = nextPaths,
                            seenCaves = nextSeenCaves
                        });
                    }
                }
            }

            Assert.Equal(0, endPaths.Count);
        }
    }
}