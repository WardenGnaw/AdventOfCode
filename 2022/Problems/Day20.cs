using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Problems
{
    [TestClass]
    public class Day20
    {
        public class NumberNode
        {
            public int Value { get; set; }
            public NumberNode Previous { get; set; }
            public NumberNode Next { get; set; }
        }

        [TestMethod]
        public void Part1_Sample()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day20_sample.txt")).Split("\r\n");
            List<NumberNode> nodes = new List<NumberNode>();
            NumberNode start = null;
            NumberNode last = null;
            foreach (string line in data)
            {
                if (start == null)
                {
                    start = new NumberNode()
                    {
                        Value = int.Parse(line)
                    };
                    last = start;
                }
                else
                {
                    NumberNode current = new NumberNode()
                    {
                        Value = int.Parse(line),
                        Previous = last
                    };
                    last.Next = current;
                    last = current;
                }
                nodes.Add(last);
            }
            last.Next = start;
            start.Previous = last;

            NumberNode zeroNode = null;

            foreach (NumberNode node in nodes)
            {
                bool isForward = node.Value >= 0;
                if (node.Value == 0)
                {
                    zeroNode = node;
                    continue;
                }
                if (isForward)
                {
                    int count = node.Value;
                    while (count > 0)
                    { 
                        NumberNode next = node.Next;
                        NumberNode prev = node.Previous;

                        prev.Next = next;
                        next.Previous = prev;
                        next.Next.Previous = node;

                        node.Previous = next;
                        node.Next = next.Next;

                        next.Next = node;

                        count--;
                    }
                }
                else
                {
                    int count = -node.Value;
                    while (count > 0)
                    {
                        NumberNode next = node.Next;
                        NumberNode prev = node.Previous;

                        prev.Next = next;
                        next.Previous = prev;
                        prev.Previous.Next = node;

                        node.Previous = prev.Previous;
                        node.Next = prev;

                        prev.Previous = node;

                        count--;
                    }
                }
            }
            NumberNode cur = zeroNode;
            int cords = 0;
            for (int i = 0; i <= 3000; i++)
            {
                if (i != 0 && i % 1000 == 0)
                {
                    cords += cur.Value;
                }
                cur = cur.Next;
            }

            Assert.AreEqual(3, cords);
        }

        [TestMethod]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day20.txt")).Split("\r\n");
            List<NumberNode> nodes = new List<NumberNode>();
            NumberNode start = null;
            NumberNode last = null;
            foreach (string line in data)
            {
                if (start == null)
                {
                    start = new NumberNode()
                    {
                        Value = int.Parse(line)
                    };
                    last = start;
                }
                else
                {
                    NumberNode current = new NumberNode()
                    {
                        Value = int.Parse(line),
                        Previous = last
                    };
                    last.Next = current;
                    last = current;
                }
                nodes.Add(last);
            }
            last.Next = start;
            start.Previous = last;

            NumberNode zeroNode = null;

            foreach (NumberNode node in nodes)
            {
                bool isForward = node.Value >= 0;
                if (node.Value == 0)
                {
                    zeroNode = node;
                    continue;
                }
                if (isForward)
                {
                    int count = node.Value;
                    while (count > 0)
                    {
                        NumberNode next = node.Next;
                        NumberNode prev = node.Previous;

                        prev.Next = next;
                        next.Previous = prev;
                        next.Next.Previous = node;

                        node.Previous = next;
                        node.Next = next.Next;

                        next.Next = node;

                        count--;
                    }
                }
                else
                {
                    int count = -node.Value;
                    while (count > 0)
                    {
                        NumberNode next = node.Next;
                        NumberNode prev = node.Previous;

                        prev.Next = next;
                        next.Previous = prev;
                        prev.Previous.Next = node;

                        node.Previous = prev.Previous;
                        node.Next = prev;

                        prev.Previous = node;

                        count--;
                    }
                }
            }
            NumberNode cur = zeroNode;
            int cords = 0;
            for (int i = 0; i <= 3000; i++)
            {
                if (i != 0 && i % 1000 == 0)
                {
                    cords += cur.Value;
                }
                cur = cur.Next;
            }

            Assert.AreEqual(9866, cords);
        }

        [TestMethod]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day20.txt")).Split("\r\n");
            Assert.AreEqual(4424278, 0);
        }
    }
}