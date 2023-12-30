using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2021.Day11
{
    public class Octopus
    {
        public int X;
        public int Y;
        public int Energy;
        private List<Octopus> neighbors = new List<Octopus>();
        private bool hasFlashed = false;

        public Octopus(int x, int y, int initialEnergy)
        {
            X = x;
            Y = y;
            Energy = initialEnergy;
        }

        public void GatherNeighbors(Dictionary<Tuple<int, int>, Octopus> octopi)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (octopi.TryGetValue(Tuple.Create(X + x, Y + y), out Octopus? neighbor))
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }
        }

        public void Step()
         {
            hasFlashed = false;
            Energy += 1;
        }

        public void CheckForFlash()
        {
            if (!hasFlashed && Energy > 9)
            {
                hasFlashed = true;
                foreach (Octopus octopus in neighbors)
                {
                    octopus.Energy += 1;
                    octopus.CheckForFlash();
                }
            }
        }

        public int ResetEnergy()
        {
            if (Energy > 9)
            {
                Energy = 0;
                return 1;
            }
            return 0;
        }
    }

    public class Main
    {
        public Dictionary<Tuple<int, int>, Octopus> CreateOctopiGrid(string[] data)
        {
            Dictionary<Tuple<int, int>, Octopus> octopi = new();

            int i = 0;
            foreach (string s in data)
            {
                int j = 0;
                foreach (char c in s.ToCharArray())
                {
                    octopi.Add(Tuple.Create(i, j), new Octopus(i, j, int.Parse(c.ToString())));
                    j++;
                }
                i++;
            }

            foreach (Octopus octopus in octopi.Values)
            {
                octopus.GatherNeighbors(octopi);
            }

            return octopi;
        }

        private string PrintGrid(Dictionary<Tuple<int, int>, Octopus> octopi)
        {
            string output = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    output += octopi[Tuple.Create(i, j)].Energy;
                }
                output += "\r\n";
            }
            return output;
        }

        [Fact]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");

            Dictionary<Tuple<int, int>, Octopus> octopi = CreateOctopiGrid(data);

            ulong totalFlashes = 0;
            for (int step = 1; step <= 100; step++)
            {
                foreach (Octopus octopus in octopi.Values)
                {
                    octopus.Step();
                }
                foreach (Octopus octopus in octopi.Values)
                {
                    octopus.CheckForFlash();
                }
                int numFlashes = 0;
                foreach (Octopus octopus in octopi.Values)
                {
                    numFlashes += octopus.ResetEnergy();
                }
                Console.WriteLine("Step " + step);
                string grid = PrintGrid(octopi);
                totalFlashes += (ulong)numFlashes;
            }

            Assert.Equal((ulong)0, totalFlashes);
        }

        [Fact]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");

            Dictionary<Tuple<int, int>, Octopus> octopi = CreateOctopiGrid(data);

            ulong step = 0;
            while (true)
            {
                step++;
                foreach (Octopus octopus in octopi.Values)
                {
                    octopus.Step();
                }
                foreach (Octopus octopus in octopi.Values)
                {
                    octopus.CheckForFlash();
                }
                int numFlashes = 0;
                foreach (Octopus octopus in octopi.Values)
                {
                    numFlashes += octopus.ResetEnergy();
                }
                if (numFlashes == 100)
                {
                    break;
                }
            }

            Assert.Equal((ulong)0, step);
        }
    }
}