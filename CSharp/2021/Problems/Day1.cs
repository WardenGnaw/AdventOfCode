namespace AdventOfCode2021.Day1
{
    public class Main
    {
        [Fact]
        public void Part1()
        {
            int increases = 0;
            int? last = null;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            foreach (string part in data.Split("\r\n"))
            {
                if (last == null)
                {
                    int.TryParse(part, out int parsed);
                    last = parsed;
                    continue;
                }
                int.TryParse(part, out int current);
                if (last < current)
                {
                    increases++;
                }

                last = current;
            }

            Console.WriteLine(increases);
        }

        [Fact]
        public void Part2()
        {
            int increases = 0;
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            string[] input = data.Split("\r\n");

            int last_sum = int.Parse(input[0]) + int.Parse(input[1]) + int.Parse(input[2]);

            for (int i = 3; i < input.Length; i++)
            {
                int current = int.Parse(input[i]);

                int current_sum = last_sum - int.Parse(input[i - 3]) + current;
                if (last_sum < current_sum)
                {
                    increases++;
                }

                last_sum = current_sum;
            }

            Console.WriteLine(increases);
        }
    }
}