using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdventOfCode2021.Day3
{
    public class Main
    {
        private int[] MajorityBits(List<string> bitStrings)
        {
            int[] list = null;

            foreach (string part in bitStrings)
            {
                if (list == null)
                {
                    list = new int[part.Length];
                }

                for (int i = 0; i < part.Length; i++)
                {
                    char c = part[i];
                    if (char.IsDigit(c))
                    {
                        if (c == '1')
                        {
                            list[i] += 1;
                        }
                        else
                        {
                            list[i] -= 1;
                        }
                    }
                }
            }

            return list;
        }

        public List<string> FilterBitStrings(List<string> bitStrings, int index, int val)
        {
            List<string> ret = new List<string>();
            foreach (string bitString in bitStrings)
            {
                if (bitString[index] == (Convert.ToChar(val) + '0'))
                {
                    ret.Add(bitString);
                }
            }

            return ret;
        }

        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            int[] list = null;

            foreach (string part in data.Split("\r\n"))
            {
                if (list == null)
                {
                    list = new int[part.Length];
                }

                for (int i = 0; i < part.Length; i++)
                {
                    char c = part[i];
                    if (char.IsDigit(c))
                    {
                        if (c == '1')
                        {
                            list[i] += 1;
                        }
                        else
                        {
                            list[i] -= 1;
                        }
                    }
                }
            }

            string gamma = string.Empty;
            string epsilon = string.Empty;

            foreach (int i in list)
            {
                gamma += i > 0 ? "1" : "0";
                epsilon += i < 0 ? "1" : "0";
            }

            Assert.Equal(0, Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            List<string> strData = data.Split("\r\n").ToList();
            int bitLength = strData[0].Length;
            int idx = 0;
            while (strData.Count > 1 && idx < bitLength)
            {
                int[] list = MajorityBits(strData);
                int val;
                if (list[idx] == 0)
                {
                    val = 1;
                }
                else
                {
                    val = list[idx] > 0 ? 1 : 0;
                }
                strData = FilterBitStrings(strData, idx, val);
                idx++;
            }
            int oxygen_rating = Convert.ToInt32(strData[0], 2);

            strData = data.Split("\r\n").ToList();
            bitLength = strData[0].Length;
            idx = 0;
            while (strData.Count > 1 && idx < bitLength)
            {
                int[] list = MajorityBits(strData);
                int val;
                if (list[idx] == 0)
                {
                    val = 0;
                }
                else
                {
                    val = list[idx] < 0 ? 1 : 0;
                }
                strData = FilterBitStrings(strData, idx, val);
                idx++;
            }

            int CO2_rating = Convert.ToInt32(strData[0], 2);

            Assert.Equal(0, oxygen_rating * CO2_rating);
        }
    }
}