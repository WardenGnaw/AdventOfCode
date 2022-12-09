
using System.Diagnostics;

namespace Problems
{
    [TestClass]
    public class Day9
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Direction CharToDirection(char c)
        {
            switch (char.ToLower(c))
            {
                case 'u':
                    return Direction.Up;
                case 'l':
                    return Direction.Left;
                case 'd':
                    return Direction.Down;
                case 'r':
                    return Direction.Right;
                default:
                    throw new InvalidOperationException();
            }
        }

        public class Point
        {
            private int _x;
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

        }

        public class Rope
        {
            public HashSet<Tuple<int, int>> TailSeen { get; set; } = new HashSet<Tuple<int, int>>();
            public Point Head { get; set; } = new Point(0, 0);
            public List<Point> Points { get; set; } = new List<Point>();
            public Point Tail { get; set; } = new Point(0, 0);

            public Rope(int length)
            {
                for (int i = 0; i < length - 2; i++)
                {
                    Points.Add(new Point(0, 0));
                }
                // Add initial state
                TailSeen.Add(Tuple.Create(0, 0));
            }

            public void MoveHead(Direction direction, int distance)
            {
                for (int i = 0; i < distance; i++)
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            Head.Y += 1;
                            Head.X += 0;
                            break;
                        case Direction.Down:
                            Head.Y -= 1;
                            Head.X += 0;
                            break;
                        case Direction.Left:
                            Head.X -= 1;
                            Head.Y += 0;
                            break;
                        case Direction.Right:
                            Head.X += 1;
                            Head.Y += 0;
                            break;
                    }
                    Point b = Head;
                    Point a;
                    for (int j = 0; j < Points.Count; j++)
                    {
                        a = Points[j];
                        if (!IsTouching(a, b))
                        {
                            Point move = Move(a, b);
                            a.X += move.X;
                            a.Y += move.Y;
                        }
                        b = a;
                    }
                    a = Tail;
                    if (!IsTouching(a, b))
                    {
                        Point move = Move(a, b);
                        a.X += move.X;
                        a.Y += move.Y;
                    }
                    TailSeen.Add(Tuple.Create(Tail.X, Tail.Y));
                }
            }
            // 0, 0 -> 1, 1
            // 2, 1
            public Point Move(Point a, Point b)
            {
                // Same row different column
                if (a.X == b.X && a.Y != b.Y)
                {
                    if (a.Y < b.Y)
                    {
                        return new Point(0, 1);
                    }
                    else
                    {
                        return new Point(0, -1);

                    }
                }

                // Same column different row
                if (a.Y == b.Y && a.X != b.X)
                {
                    if (a.X < b.X)
                    {
                        return new Point(1, 0);
                    }
                    else
                    {
                        return new Point(-1, 0);

                    }
                }

                if (a.Y != b.Y && a.X != b.X)
                {
                    // Find the offset of +2
                    if (a.X + 2 == b.X && a.Y + 1 == b.Y)
                    {
                        return new Point(1, 1);
                    }
                    if (a.X + 2 == b.X && a.Y - 1 == b.Y)
                    {
                        return new Point(1, -1);
                    }
                    if (a.X - 2 == b.X && a.Y + 1 == b.Y)
                    {
                        return new Point(-1, 1);
                    }
                    if (a.X - 2 == b.X && a.Y - 1 == b.Y)
                    {
                        return new Point(-1, -1);
                    }


                    if (a.Y + 2 == b.Y && a.X + 1 == b.X)
                    {
                        return new Point(1, 1);
                    }
                    if (a.Y + 2 == b.Y && a.X - 1 == b.X)
                    {
                        return new Point(-1, 1);
                    }
                    if (a.Y - 2 == b.Y && a.X + 1 == b.X)
                    {
                        return new Point(1, -1);
                    }
                    if (a.Y - 2 == b.Y && a.X - 1 == b.X)
                    {
                        return new Point(-1, -1);
                    }

                    if (a.Y + 2 == b.Y && a.X + 2 == b.X)
                    {
                        return new Point(1, 1);
                    }
                    if (a.Y + 2 == b.Y && a.X - 2 == b.X)
                    {
                        return new Point(-1, 1);
                    }
                    if (a.Y - 2 == b.Y && a.X + 2 == b.X)
                    {
                        return new Point(1, -1);
                    }
                    if (a.Y - 2 == b.Y && a.X - 2 == b.X)
                    {
                        return new Point(-1, -1);
                    }
                }

                Debug.Assert(false, "Why are you moving");

                return new Point(0, 0);
            }

            public bool IsTouching(Point a, Point b)
            {
                // Same spot
                if (a.X == b.X && a.Y == b.Y)
                {
                    return true;
                }
                if (a.X == (b.X + 1) && a.Y == b.Y)
                {
                    return true;
                }
                if (a.X == (b.X - 1) && a.Y == b.Y)
                {
                    return true;
                }
                if (a.X == b.X && a.Y == (b.Y + 1))
                {
                    return true;
                }
                if (a.X == b.X && a.Y == (b.Y - 1))
                {
                    return true;
                }
                if (a.X == (b.X - 1) && a.Y == (b.Y - 1))
                {
                    return true;
                }
                if (a.X == (b.X + 1) && a.Y == (b.Y - 1))
                {
                    return true;
                }
                if (a.X == (b.X - 1) && a.Y == (b.Y + 1))
                {
                    return true;
                }
                if (a.X == (b.X + 1) && a.Y == (b.Y + 1))
                {
                    return true;
                }

                return false;
            }
        }

        [TestMethod]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day9.txt")).Trim().Split("\r\n");
            Rope rope = new Rope(2);
            for (int i = 0; i < data.Length; i++)
            {
                string[] message = data[i].Split(" ");
                Direction direction = CharToDirection(message[0].ToCharArray()[0]);
                int distance = int.Parse(message[1]);
                rope.MoveHead(direction, distance);
            }
            Assert.Fail("" + rope.TailSeen.Count);
        }

        [TestMethod]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day9.txt")).Trim().Split("\r\n");
            Rope rope = new Rope(10);
            for (int i = 0; i < data.Length; i++)
            {
                string[] message = data[i].Split(" ");
                Direction direction = CharToDirection(message[0].ToCharArray()[0]);
                int distance = int.Parse(message[1]);
                rope.MoveHead(direction, distance);
            }
            Assert.Fail("" + rope.TailSeen.Count);
        }
    }
}