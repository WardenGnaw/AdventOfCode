using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2021.Day14
{
    public class Main
    {
        public class Pair
        {
            public char Left;
            public char Right;
            public char Inject;

            public Pair(char left, char right, char inject)
            {
                Left = left;
                Right = right;
                Inject = inject;
            }

            // Used for part 2
            public List<string> GetOutput()
            {
                return new List<string>()
                {
                    string.Join(Left, Inject),
                    string.Join(Right, Inject)
                };
            }
        }

        public class Chain
        {
            public char Element;
            public Chain Next;

            public Chain(char e)
            {
                Element = e;
                Next = null;
            }
        }

        public void Step(Chain start, List<Pair> pairs)
        {
            Chain current = start;
            while (current.Next != null)
            {
                char left = current.Element;
                char right = current.Next.Element;

                char? inject = null;
                for (int i = 0; i < pairs.Count; i++)
                {
                    if (left == pairs[i].Left && right == pairs[i].Right)
                    {
                        inject = pairs[i].Inject;
                    }
                }

                if (inject != null)
                {
                    Chain next = current.Next;
                    Chain newNode = new Chain(inject.Value);
                    current.Next = newNode;
                    newNode.Next = next;
                    current = next;
                }
                else
                {
                    throw new Exception("NONE FOUND");
                }
            }
        }

        [Fact]
        public async void Part1()
        {
            string[] data = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            string template = data.Take(1).First();

            List<Pair> pairs = new List<Pair>();

            foreach (string pair in data.Skip(1))
            {
                if (string.IsNullOrEmpty(pair)) continue;
                string[] pairArr = pair.Split("->");
                string input = pairArr[0].Trim();
                pairs.Add(new Pair(input[0], input[1], pairArr[1].Trim()[0]));
            }

            Chain start = new Chain(template.Take(1).First());
            Chain last = start;
            foreach(char c in template.Skip(1))
            {
                Chain next = new Chain(c);
                last.Next = next;
                last = next;
            }

            int i = 0;
            while (i < 10)
            {
                Step(start, pairs);
                i++;
            }

            Dictionary<char, int> count = new();
            Chain cur = start;
            while (cur != null)
            {
                if (!count.ContainsKey(cur.Element))
                {
                    count[cur.Element] = 1;
                }
                else
                {
                    count[cur.Element] += 1;
                }
                cur = cur.Next;
            }

            int min = int.MaxValue;
            int max = int.MinValue;

            foreach(var c in count)
            {
                if (min > c.Value)
                {
                    min = c.Value;
                }
                if (max < c.Value)
                {
                    max = c.Value;
                }
            }

            Assert.Equal(0, max - min);
        }

        [Fact]
        public async void Part2()
        {
            string[] data = await File.ReadAllLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            string templateData = data.Take(1).First();

            Dictionary<string, List<string>> pairs = new Dictionary<string, List<string>>();

            foreach (string pair in data.Skip(1))
            {
                if (string.IsNullOrEmpty(pair)) continue;
                string[] pairArr = pair.Split("->");
                string input = pairArr[0].Trim();

                List<string> output = new List<string>
                {
                    string.Concat(input[0], pairArr[1].Trim()),
                    string.Concat(pairArr[1].Trim(), input[1])
                };
                pairs.Add(input, output);
            }

            Dictionary<string, ulong> template = new();

            for (int i = 1; i < templateData.Length; i++)
            {
                char left = templateData[i - 1];
                char right = templateData[i];

                string sequence = string.Concat(left, right);

                if (template.ContainsKey(sequence))
                {
                    template[sequence] += 1;
                }
                else
                {
                    template[sequence] = 1;
                }
            }

            int steps = 0;
            while (steps < 40)
            {
                Dictionary<string, ulong> newTemplate = new();
                foreach (var t in template)
                {
                    List<string> output = pairs[t.Key];
                    foreach (var o in output)
                    {
                        if (newTemplate.ContainsKey(o))
                        {
                            newTemplate[o] += t.Value;
                        }
                        else
                        {
                            newTemplate[o] = t.Value;
                        }
                    }
                }

                template = newTemplate;
                
                steps++;
            }

            Dictionary<char, ulong> count = new();
            List<KeyValuePair<string, ulong>> listTemplate = template.ToList();
            for (int k = 0; k < listTemplate.Count; k++)
            {
                var t = listTemplate[k];
                if (k == 0)
                {
                    var left = t.Key[0];

                    if (count.ContainsKey(left))
                    {
                        count[left] += (ulong)t.Value;
                    }
                    else
                    {
                        count.Add(left, (ulong)t.Value);
                    }
                }
                var right = t.Key[0];

                if (count.ContainsKey(right))
                {
                    count[right] += (ulong)t.Value;
                }
                else
                {
                    count.Add(right, (ulong)t.Value);
                }
            }

            ulong min = ulong.MaxValue;
            ulong max = ulong.MinValue;

            foreach (var c in count)
            {
                if (min > c.Value)
                {
                    min = c.Value;
                }
                if (max < c.Value)
                {
                    max = c.Value;
                }
            }

            Assert.Equal((ulong)0, max - min);
        }
    }
}