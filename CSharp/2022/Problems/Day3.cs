namespace Problems
{
    [TestClass]
    public class Day3
    {
        [TestMethod]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day3.txt"));
            List<char> shared = new List<char>();
            foreach (string part in data.Split("\r\n"))
            {
                char[] parts = part.Trim().ToCharArray();
                HashSet<char> bag1 = new HashSet<char>();
                HashSet<char> bag2 = new HashSet<char>();
                for (int i = 0; i < parts.Length / 2; i++)
                {
                    bag1.Add(parts[i]);
                }
                for (int i = parts.Length / 2; i < parts.Length; i++)
                {
                    bag2.Add(parts[i]);
                }
                shared.AddRange(bag1.Intersect(bag2).ToList());
            }
            int total = 0;
            foreach (char c in shared)
            {
                if (char.IsLower(c))
                {
                    total += (c - 'a' + 1);
                }
                else if (char.IsUpper(c))
                {
                    total += (c - 'A' + 27);
                }
            }
            Assert.Fail("" + total);

        }

        [TestMethod]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day3.txt"));
            List<char> shared = new List<char>();
            List<string> input = data.Split("\r\n").ToList();
            for (int i = 0; i < input.Count; i += 3)
            {
                List<HashSet<char>> bags = new List<HashSet<char>>()
                {
                    new HashSet<char>(),
                    new HashSet<char>(),
                    new HashSet<char>()
                };
                for (int j = i; j < i + 3; j++)
                {
                    char[] parts = input[j].Trim().ToCharArray();
                    HashSet<char> bag = bags[j - i];
 
                    for (int k = 0; k < parts.Length; k++)
                    {
                        bag.Add(parts[k]);
                    }
                }
                shared.AddRange(bags[0].Intersect(bags[1]).Intersect(bags[2]).ToList());
            }
            int total = 0;
            foreach (char c in shared)
            {
                if (char.IsLower(c))
                {
                    total += (c - 'a' + 1);
                }
                else if (char.IsUpper(c))
                {
                    total += (c - 'A' + 27);
                }
            }
            Assert.Fail("" + total);
        }
    }
}