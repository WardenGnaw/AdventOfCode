using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2021.Day15
{
    public class Main
    {
        public class Chiton
        {
            public int RiskLevel;

            public Chiton(int riskLevel)
            {
                RiskLevel = riskLevel;
            }
        }

        private Dictionary<Tuple<int, int>, int> indexToRisk = new();

        public int MinRisk(Chiton[,] chitons, int x, int y, int size)
        {
            if (indexToRisk.ContainsKey(Tuple.Create(x, y)))
            {
                return indexToRisk[Tuple.Create(x, y)];
            }

            int risk = chitons[x, y].RiskLevel;

            if (x + 1 < size && y + 1 < size)
            {
                int down = MinRisk(chitons, x + 1, y, size);
                int right = MinRisk(chitons, x, y + 1, size);

                risk += Math.Min(down, right);
            }
            else if (x + 1 < size)
            {
                risk += MinRisk(chitons, x + 1, y, size);
            }
            else if (y + 1 < size)
            {
                risk += MinRisk(chitons, x, y + 1, size);
            }


            if (!indexToRisk.ContainsKey(Tuple.Create(x, y)))
            {
                indexToRisk[Tuple.Create(x, y)] = risk;
            }

            return risk;
        }

        [Fact]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");
            Chiton[,] chitons = new Chiton[data.Length, data.Length];

            int i = 0;
            foreach (string d in data)
            {
                int j = 0;
                int[] values = d.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
                foreach (int v in values)
                {
                    chitons.SetValue(new Chiton(v), i, j);
                    j++;
                }
                i++;
            }

            int down = MinRisk(chitons, 1, 0, data.Length);
            int right = MinRisk(chitons, 0, 1, data.Length);

            Assert.Equal(0, Math.Min(down, right));
        }

        private Dictionary<Tuple<int, int>, int> grid = new();

        public int MinRisk2(Chiton[,] chitons, int x, int y, int size, int expansion)
        {
            int actualX = x % size;
            int actualY = y % size;

            if (indexToRisk.ContainsKey(Tuple.Create(x, y)))
            {
                return indexToRisk[Tuple.Create(x, y)];
            }

            int riskValue = chitons[actualX, actualY].RiskLevel + x / size + y / size;

            int sumRisk;
            switch (riskValue)
            {
                case 1:
                    sumRisk = 1;
                    break;
                case 2:
                    sumRisk = 2;
                    break;
                case 3:
                    sumRisk = 3;
                    break;
                case 4:
                    sumRisk = 4;
                    break;
                case 5:
                    sumRisk = 5;
                    break;
                case 6:
                    sumRisk = 6;
                    break;
                case 7:
                    sumRisk = 7;
                    break;
                case 8:
                    sumRisk = 8;
                    break;
                case 9:
                    sumRisk = 9;
                    break;
                case 10:
                    sumRisk = 1;
                    break;
                case 11:
                    sumRisk = 2;
                    break;
                case 12:
                    sumRisk = 3;
                    break;
                case 13:
                    sumRisk = 4;
                    break;
                case 14:
                    sumRisk = 5;
                    break;
                case 15:
                    sumRisk = 6;
                    break;
                case 16:
                    sumRisk = 7;
                    break;
                case 17:
                    sumRisk = 8;
                    break;
                default:
                    throw new Exception("Past Max Value");
            };
            grid[Tuple.Create(x, y)] = sumRisk;

            Debug.Assert(x < 500 && y < 500);

            if (x + 1 < (size * expansion) && y + 1 < (size * expansion))
            {
                int down = MinRisk2(chitons, x + 1, y, size, expansion);
                int right = MinRisk2(chitons, x, y + 1, size, expansion);

                sumRisk += Math.Min(down, right);
            }
            else if (x + 1 < (size * expansion))
            {
                sumRisk += MinRisk2(chitons, x + 1, y, size, expansion);
            }
            else if (y + 1 < (size * expansion))
            {
                sumRisk += MinRisk2(chitons, x, y + 1, size, expansion);
            }


            if (!indexToRisk.ContainsKey(Tuple.Create(x, y)))
            {
                indexToRisk[Tuple.Create(x, y)] = sumRisk;
            }

            return sumRisk;
        }

        [Fact]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt")).Split("\r\n");
            Chiton[,] chitons = new Chiton[data.Length, data.Length];

            int i = 0;
            foreach (string d in data)
            {
                int j = 0;
                int[] values = d.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
                foreach (int v in values)
                {
                    chitons.SetValue(new Chiton(v), i, j);
                    j++;
                }
                i++;
            }

            int down = MinRisk2(chitons, 1, 0, data.Length, 5);
            int right = MinRisk2(chitons, 0, 1, data.Length, 5);

            //for (int j = 0; j < data.Length * 5; j++)
            //{
            //    for (int k = 0; k < data.Length * 5; k++)
            //    {
            //        if (grid.ContainsKey(Tuple.Create(j, k)))
            //        {
            //            Debug.Write(grid[Tuple.Create(j, k)]);
            //        }
            //        else
            //        {
            //            Debug.Write(chitons[j, k].RiskLevel);
            //        }
            //    }
            //    Debug.WriteLine("");
            //}

            Assert.Equal(0, Math.Min(down, right));
        }
    }
}