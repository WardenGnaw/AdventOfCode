using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Day8
{
    public class Main
    {

        /*
         *  aaa
         * b   c
         * b   c
         *  ddd
         * e   f
         * e   f
         *  ggg
         */
        public int HashSetToInt(HashSet<char> hashSet)
        {
            if (hashSet.Count == 2)
                return 1;
            else if (hashSet.Count == 3)
                return 7;
            else if (hashSet.Count == 4)
                return 4;
            else if (hashSet.Count == 7)
                return 8;

            // We do not know...
            return -1;
        }

        public class Display
        {
            public List<HashSet<char>> inputs;
            public List<HashSet<char>> outputs;

            public Display(string input, string output)
            {
                inputs = new List<HashSet<char>>();
                outputs = new List<HashSet<char>>();
                foreach (string i in input.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    inputs.Add(i.ToHashSet());
                }
                foreach (string i in output.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                {
                    outputs.Add(i.ToHashSet());
                }
            }
        }

        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            List<Display> displays = new List<Display>();
            foreach (string part in data.Split("\r\n"))
            {
                string[] input = part.Split("|");
                displays.Add(new Display(input[0].Trim(), input[1].Trim()));
            }

            Dictionary<int, int> counts = new Dictionary<int, int>();

            foreach (Display d in displays)
            {
                foreach (HashSet<char> o in d.outputs)
                {
                    int val = HashSetToInt(o);
                    if (counts.ContainsKey(val))
                    {
                        counts[val] += 1;
                    }
                    else
                    {
                        counts[val] = 1;
                    }
                }
            }

            Assert.Equal(0, counts[1] + counts[4] + counts[7] + counts[8]);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            List<Display> displays = new List<Display>();
            foreach (string part in data.Split("\r\n"))
            {
                string[] input = part.Split("|");
                displays.Add(new Display(input[0].Trim(), input[1].Trim()));
            }

            int total = 0;

            foreach (Display d in displays)
            {
                Dictionary<int, HashSet<char>> numberToSet = new Dictionary<int, HashSet<char>>();
                Dictionary<HashSet<char>, int> setToNumber = new Dictionary<HashSet<char>, int>();
                List<HashSet<char>> five = new List<HashSet<char>>();
                List<HashSet<char>> six = new List<HashSet<char>>();

                Queue<HashSet<char>> queue = new Queue<HashSet<char>>(d.inputs);
                Queue<HashSet<char>> unknownQueue = new Queue<HashSet<char>>();

                // Find out 1, 4, 7, 8
                while (queue.Count > 0)
                {
                    HashSet<char> cur = queue.Dequeue();
                    int val = HashSetToInt(cur);
                    if (val >= 0)
                    {
                        if (!numberToSet.ContainsKey(val))
                        {
                            numberToSet.Add(val, cur);
                            setToNumber.Add(cur, val);
                        }
                        else
                        {
                            Debug.Assert(numberToSet[val].Equals(cur));
                        }
                    }
                    else
                    {
                        if (cur.Count == 5)
                        {
                            five.Add(cur);
                        }
                        else if (cur.Count == 6)
                        {
                            six.Add(cur);
                        }
                        else
                        {
                            unknownQueue.Enqueue(cur);
                        }
                    }
                }

                // This should be the top
                HashSet<char> top = numberToSet[7].Except(numberToSet[1]).ToHashSet();
                HashSet<char> bottom_L_segment = numberToSet[8].Except(numberToSet[7]).Except(numberToSet[4]).ToHashSet();

                // Find 2
                foreach (HashSet<char> cur in five)
                {
                    if (bottom_L_segment.Intersect(cur).Count() == 2)
                    {
                        numberToSet[2] = cur;
                        setToNumber[cur] = 2;
                        break;
                    }
                }

                HashSet<char> middle = numberToSet[2].Except(numberToSet[7]).Except(bottom_L_segment).ToHashSet();

                // 8 minus middle = 0
                HashSet<char> zero = numberToSet[8].Except(middle).ToHashSet();
                numberToSet[0] = zero;
                setToNumber[zero] = 0;

                // find 3 5
                foreach (HashSet<char> cur in five)
                {
                    if (setToNumber.ContainsKey(cur))
                    {
                        continue;
                    }

                    // 3 - 7 has 2 segments left
                    if (cur.Except(numberToSet[7]).Count() == 2)
                    {
                        if (numberToSet.ContainsKey(3))
                        {
                            Debug.Assert(numberToSet[3] == cur);
                        }
                        numberToSet[3] = cur;
                        setToNumber[cur] = 3;
                    }
                    else
                    {
                        if (numberToSet.ContainsKey(5))
                        {
                            Debug.Assert(numberToSet[5] == cur);
                        }
                        numberToSet[5] = cur;
                        setToNumber[cur] = 5;
                    }
                }


                // find 6 9
                foreach (HashSet<char> cur in six)
                {
                    if (setToNumber.ContainsKey(cur))
                    {
                        continue;
                    }

                    if (cur.Except(numberToSet[7]).Count() == 4)
                    {
                        if (numberToSet.ContainsKey(6))
                        {
                            Debug.Assert(cur.Except(numberToSet[6]).Count() == 0);
                        }
                        else
                        {
                            numberToSet[6] = cur;
                            setToNumber[cur] = 6;
                        }
                    }
                    else if (cur.Except(numberToSet[7]).Count() == 3)
                    {
                        if (cur.Except(numberToSet[0]).Count() == 0)
                        {
                            continue;
                        }
                        if (numberToSet.ContainsKey(9))
                        {
                            Debug.Assert(cur.Except(numberToSet[9]).Count() == 0);
                        }
                        else
                        {
                            numberToSet[9] = cur;
                            setToNumber[cur] = 9;
                        }
                    }
                }

                string output = string.Empty;

                foreach (HashSet<char> o in d.outputs)
                {
                    foreach (var kv in numberToSet)
                    {
                        if (o.SetEquals(kv.Value))
                        {
                            output = string.Format("{0}{1}", output, kv.Key);
                        }
                    }
                }

                total += int.Parse(output);
            }

            Assert.Equal(0, total);
        }
    }
}