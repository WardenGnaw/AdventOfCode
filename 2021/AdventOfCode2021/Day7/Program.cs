using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using Xunit;

namespace AdventOfCode2021.Day7
{
    public class Main
    {
        public int Sum(int n)
        {
            return n * (n + 1) / 2;
        }

        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int[] hPos = data.Split(",").Select(int.Parse).ToArray();

            int min = hPos.Min();
            int max = hPos.Max();

            int bestTotalFuel = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                int currentFuelValue = 0;
                foreach (int pos in hPos)
                {
                    currentFuelValue += Math.Abs(pos - i);
                }
                if (currentFuelValue < bestTotalFuel)
                {
                    bestTotalFuel = currentFuelValue;
                }
            }

            Assert.Equal(0, bestTotalFuel);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int[] hPos = data.Split(",").Select(int.Parse).ToArray();

            int min = hPos.Min();
            int max = hPos.Max();

            int bestTotalFuel = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                int currentFuelValue = 0;
                foreach (int pos in hPos)
                {
                    currentFuelValue += Sum(Math.Abs(pos - i));
                }
                if (currentFuelValue < bestTotalFuel)
                {
                    bestTotalFuel = currentFuelValue;
                }
            }

            Assert.Equal(0, bestTotalFuel);
        }
    }
}