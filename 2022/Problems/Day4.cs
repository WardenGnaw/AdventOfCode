namespace Problems
{
    [TestClass]
    public class Day4
    {
        [TestMethod]
        public void Part1()
        {
            int count = 0;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day4.txt"));
            foreach (string assignments in data.Split("\r\n"))
            {
                string[] ranges = assignments.Trim().Split(",");
                string[] range1 = ranges[0].Split("-");
                string[] range2 = ranges[1].Split("-");
                int min1 = int.Parse(range1[0]);
                int max1 = int.Parse(range1[1]);
                int min2 = int.Parse(range2[0]);
                int max2 = int.Parse(range2[1]);
                if ((min1 <= min2 && max1 >= max2) ||
                    (min2 <= min1 && max2 >= max1))
                {
                    count++;
                }
            }
            Assert.Fail("" + count);

        }

        [TestMethod]
        public void Part2()
        {
            int count = 0;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day4.txt"));
            foreach (string assignments in data.Split("\r\n"))
            {
                string[] ranges = assignments.Trim().Split(",");
                string[] range1 = ranges[0].Split("-");
                string[] range2 = ranges[1].Split("-");
                int min1 = int.Parse(range1[0]);
                int max1 = int.Parse(range1[1]);
                int min2 = int.Parse(range2[0]);
                int max2 = int.Parse(range2[1]);
                HashSet<int> assignment1 = new HashSet<int>();
                HashSet<int> assignment2 = new HashSet<int>();
                for (int i = min1; i <= max1; i++)
                {
                    assignment1.Add(i);
                }
                for (int i = min2; i <= max2; i++)
                {
                    assignment2.Add(i);
                }
                IEnumerable<int> overlaps = assignment1.Intersect(assignment2);
                if (overlaps.Any())
                {
                    count++;
                }
            }
            Assert.Fail("" + count);
        }
    }
}