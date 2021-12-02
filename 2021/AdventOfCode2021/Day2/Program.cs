using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AdventOfCode2021.Day2
{
    public class Main
    {
        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int position = 0;
            int depth = 0;

            foreach (string line in data.Split("\r\n"))
            {
                string[] parts = line.Split(' ');
                switch (parts[0])
                {
                    case "forward":
                        position += int.Parse(parts[1]);
                        break;
                    case "down":
                        depth -= int.Parse(parts[1]);
                        break;
                    case "up":
                        depth += int.Parse(parts[1]);
                        break;
                    default:
                        break;
                }
            }

            Assert.Equal(0, position * depth);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int position = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in data.Split("\r\n"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(' ');
                switch (parts[0])
                {
                    case "forward":
                        position += int.Parse(parts[1]);
                        depth += (aim * int.Parse(parts[1]));
                        break;
                    case "down":
                        aim += int.Parse(parts[1]);
                        break;
                    case "up":
                        aim -= int.Parse(parts[1]);
                        break;
                    default:
                        break;
                }
            }

            Assert.Equal(0, position * depth);
        }
    }
}