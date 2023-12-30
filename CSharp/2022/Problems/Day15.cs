using System.Text.RegularExpressions;

namespace Problems
{
    [TestClass]
    public class Day15
    {
        private int Manhattan(int x0, int y0, int x1, int y1)
        {
            return Math.Abs(x0 - x1) + Math.Abs(y0 - y1);
        }

        [TestMethod]
        public void Part1()
        {
            List<Tuple<int, int>> sensors = new List<Tuple<int, int>>();
            HashSet<Tuple<int, int>> beacons = new HashSet<Tuple<int, int>>();
            List<int> distance = new List<int>();
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day15.txt")).Split("\r\n");
            Regex regex = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$");
            foreach (string line in data)
            {
                Match m = regex.Match(line);
                if (m.Success)
                {
                    int x0 = int.Parse(m.Groups[1].ToString());
                    int y0 = int.Parse(m.Groups[2].ToString());
                    int x1 = int.Parse(m.Groups[3].ToString());
                    int y1 = int.Parse(m.Groups[4].ToString());
                    sensors.Add(Tuple.Create(x0, y0));
                    beacons.Add(Tuple.Create(x1, y1));
                    int dis = Manhattan(x0, y0, x1, y1);
                    distance.Add(dis);

                    if (x0 < minX) minX = x0;
                    if (x1 < minX) minX = x1;
                    if (x0 > maxX) maxX = x0;
                    if (x1 > maxX) maxX = x1;
                    if (y0 < minY) minY = y0;
                    if (y1 < minY) minY = y1;
                    if (y0 > maxY) maxY = y0;
                    if (y1 > maxY) maxY = y1;
                }
            }

            HashSet<int> row = new HashSet<int>();

            for (int i = 0; i < sensors.Count; i++)
            {
                int x = distance[i] - Math.Abs(sensors[i].Item2 - 2000000);
                for (int j = sensors[i].Item1 - x; j < sensors[i].Item1 + x + 1; j++)
                {
                    if (beacons.Contains(Tuple.Create(j, 2000000)))
                    {
                        continue;
                    }
                    row.Add(j);
                }
            }
            Assert.AreEqual(4424278, row.Count);
        }

        [TestMethod]
        public void Part2()
        {
            List<Tuple<int, int>> sensors = new List<Tuple<int, int>>();
            HashSet<Tuple<int, int>> beacons = new HashSet<Tuple<int, int>>();
            List<int> distance = new List<int>();
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day15.txt")).Split("\r\n");
            Regex regex = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$");
            foreach (string line in data)
            {
                Match m = regex.Match(line);
                if (m.Success)
                {
                    int x0 = int.Parse(m.Groups[1].ToString());
                    int y0 = int.Parse(m.Groups[2].ToString());
                    int x1 = int.Parse(m.Groups[3].ToString());
                    int y1 = int.Parse(m.Groups[4].ToString());
                    sensors.Add(Tuple.Create(x0, y0));
                    beacons.Add(Tuple.Create(x1, y1));
                    int dis = Manhattan(x0, y0, x1, y1);
                    distance.Add(dis);

                    if (x0 < minX) minX = x0;
                    if (x1 < minX) minX = x1;
                    if (x0 > maxX) maxX = x0;
                    if (x1 > maxX) maxX = x1;
                    if (y0 < minY) minY = y0;
                    if (y1 < minY) minY = y1;
                    if (y0 > maxY) maxY = y0;
                    if (y1 > maxY) maxY = y1;
                }
            }

            HashSet<int> row = new HashSet<int>();

            for (int i = 0; i < sensors.Count; i++)
            {
                int x = distance[i] - Math.Abs(sensors[i].Item2 - 2000000);
                for (int j = sensors[i].Item1 - x; j < sensors[i].Item1 + x + 1; j++)
                {
                    if (beacons.Contains(Tuple.Create(j, 2000000)))
                    {
                        continue;
                    }
                    row.Add(j);
                }
            }
            Assert.AreEqual(4424278, row.Count);
        }
    }
}