using System.Text;
using System.Text.RegularExpressions;

namespace Problems
{
    [TestClass]
    public class Day6
    {
        [TestMethod]
        public void Part1()
        {
            HashSet<char> seen = new HashSet<char>();
            Queue<char> queue = new Queue<char>();
            int count = -1;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day6.txt")).Trim();
            char[] chars = data.ToCharArray();
            int i = 0;
            for (; i < chars.Length; i++)
            {
                if (seen.Contains(chars[i]))
                {
                    while(queue.Any() && queue.Peek() != chars[i])
                    {
                        seen.Remove(queue.Dequeue());
                    }
                    queue.Dequeue();
                    queue.Enqueue(chars[i]);
                }
                else
                {
                    seen.Add(chars[i]);
                    queue.Enqueue(chars[i]);
                }

                if (queue.Count == 4)
                {
                    break;
                }
            }
            
            Assert.Fail("" + (i+1));
        }

        [TestMethod]
        public void Part2()
        {
            HashSet<char> seen = new HashSet<char>();
            Queue<char> queue = new Queue<char>();
            int count = -1;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day6.txt")).Trim();
            char[] chars = data.ToCharArray();
            int i = 0;
            for (; i < chars.Length; i++)
            {
                if (seen.Contains(chars[i]))
                {
                    while (queue.Any() && queue.Peek() != chars[i])
                    {
                        seen.Remove(queue.Dequeue());
                    }
                    queue.Dequeue();
                    queue.Enqueue(chars[i]);
                }
                else
                {
                    seen.Add(chars[i]);
                    queue.Enqueue(chars[i]);
                }

                if (queue.Count == 14)
                {
                    break;
                }
            }

            Assert.Fail("" + (i + 1));
        }
    }
}