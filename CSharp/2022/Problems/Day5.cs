using System.Text;
using System.Text.RegularExpressions;

namespace Problems
{
    [TestClass]
    public class Day5
    {
        private class Crate
        {
            public char Letter;
            public Crate(char letter)
            {
                this.Letter = letter;
            }

            public override string ToString()
            {
                return "[" + Letter + "]";
            }
        }

        private class Crane
        {
            private List<Stack<Crate>> Stacks;
            private int Count;

            public Crane(List<Stack<string>> queue)
            {
                Stacks = new List<Stack<Crate>>();
                for (int i = 0; i < queue.Count; i++)
                {
                    Stacks.Add(new Stack<Crate>());
                    Stack<string> q = queue[i];
                    while (q.Any())
                    {
                        char c = q.Pop().Replace("[", string.Empty).Replace("]", string.Empty).Trim().ToCharArray()[0];
                        Crate crate = new Crate(c);
                        Stacks[i].Push(crate);
                    }
                }
                Count = queue.Count;
            }

            public void Move(int count, int start, int end)
            {
                for (int i = 0; i < count; i++)
                {
                    Crate c = Stacks[start - 1].Pop();
                    Stacks[end - 1].Push(c);
                }
            }

            public void Move9001(int count, int start, int end)
            {
                Stack<Crate> stack = new Stack<Crate>();
                for (int i = 0; i < count; i++)
                {
                    Crate c = Stacks[start - 1].Pop();
                    stack.Push(c);
                }
                while (stack.Any())
                {
                    Stacks[end - 1].Push(stack.Pop());
                }
            }

            public string GetTopRow()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Count; i++)
                {
                    if (Stacks[i].TryPeek(out Crate c))
                    {
                        sb.Append(c.Letter);
                    }
                }
                return sb.ToString();
            }
        }

        [TestMethod]
        public void Part1()
        {
            int count = -1;
            List<Stack<string>> queue = new List<Stack<string>>();
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day5.txt"));
            string[] datas = data.Split("\r\n");
            int i = 0;
            while (!string.IsNullOrWhiteSpace(datas[i]))
            {
                List<string> parts = datas[i].SplitInParts(4).ToList();
                if (count == -1)
                {
                    count = parts.Count;
                    for (int k = 0; k < count; k++)
                    {
                        queue.Add(new Stack<string>());
                    }
                }
                for (int j = 0; j < count; j++)
                {
                    if (string.IsNullOrWhiteSpace(parts[j]) || char.IsDigit(parts[j].Trim()[0]))
                    {
                        continue;
                    }
                    queue[j].Push(parts[j]);
                }

                i++;
            }
            Crane c = new Crane(queue);

            Regex regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            while (i < datas.Length)
            {
                Match m = regex.Match(datas[i]);
                if (m.Success)
                {
                    c.Move(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value));
                }
                i++;
            }

            Assert.Fail("" + c.GetTopRow());

        }

        [TestMethod]
        public void Part2()
        {
            int count = -1;
            List<Stack<string>> queue = new List<Stack<string>>();
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day5.txt"));
            string[] datas = data.Split("\r\n");
            int i = 0;
            while (!string.IsNullOrWhiteSpace(datas[i]))
            {
                List<string> parts = datas[i].SplitInParts(4).ToList();
                if (count == -1)
                {
                    count = parts.Count;
                    for (int k = 0; k < count; k++)
                    {
                        queue.Add(new Stack<string>());
                    }
                }
                for (int j = 0; j < count; j++)
                {
                    if (string.IsNullOrWhiteSpace(parts[j]) || char.IsDigit(parts[j].Trim()[0]))
                    {
                        continue;
                    }
                    queue[j].Push(parts[j]);
                }

                i++;
            }
            Crane c = new Crane(queue);

            Regex regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            while (i < datas.Length)
            {
                Match m = regex.Match(datas[i]);
                if (m.Success)
                {
                    c.Move9001(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value));
                }
                i++;
            }

            Assert.Fail("" + c.GetTopRow());
        }
    }

    static class StringExtensions
    {
        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

    }
}