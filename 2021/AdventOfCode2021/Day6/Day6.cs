using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode2021.Day6
{
    public class Main 
    {
        class LanternFish
        {
            public const int NEW_LANTERN_FISH = 6;
            public int State;

            public LanternFish(int state)
            {
                State = state;
            }

            public bool Next()
            {

                if (--State < 0)
                {
                    State = NEW_LANTERN_FISH;
                    return true;
                }

                return false;
            }
}       

        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int[] initialState = data.Split(",").Select(x => int.Parse(x)).ToArray();

            List<LanternFish> fishies = new List<LanternFish>(initialState.Length);
            foreach (int state in initialState)
            {
                fishies.Add(new LanternFish(state));
            }

            for (int i = 0; i < 256; i++)
            {
                List<LanternFish> newFish = new List<LanternFish>();
                foreach (LanternFish fish in fishies)
                {
                    if (fish.Next())
                    {
                        newFish.Add(new LanternFish(8));
                    }
                }
                fishies.AddRange(newFish);
            }

            Assert.Equal(0, fishies.Count);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            ulong[] initialState = data.Split(",").Select(x => ulong.Parse(x)).ToArray();

            Dictionary<ulong, ulong> fishies = new Dictionary<ulong, ulong>();
            for (ulong i = 0; i < 9; i++)
            {
                fishies.Add(i, (ulong)initialState.Where(state => state == i).Count());
            }

            ulong lastZeroFishies = 0;
            // Loop
            for (ulong i = 0; i < 256; i++)
            {
                for (ulong j = 1; j < 9; j++)
                {
                    fishies[j - 1] = fishies[j];
                }
                fishies[8] = lastZeroFishies;
                fishies[6] += lastZeroFishies;
                lastZeroFishies = fishies[0];
            }

            ulong count = 0;

            foreach (var fish in fishies)
            {
                count += fish.Value;
            }

            Console.WriteLine(count);
        }
    }
}