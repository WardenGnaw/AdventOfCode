namespace AdventOfCode2024;

[TestClass]
public class Day8
{
    [TestMethod]
    public async Task Part1Async()
    {
        Dictionary<char, HashSet<Tuple<int, int>>> antennas = new();                                               

        string[] input = await File.ReadAllLinesAsync("input/day8.txt");
        char[][] data = input.Select(x => x.ToCharArray()).ToArray();

        int rowMax = data.Length;
        int colMax = data[0].Length;

        for (int i = 0 ; i < rowMax; i++)
        {
            for (int j = 0; j < colMax; j++)
            {
                if (data[i][j] != '.')
                {
                    char antenna = data[i][j];
                    if (!antennas.TryGetValue(antenna, out HashSet<Tuple<int, int>>? set))
                    {
                        set = new HashSet<Tuple<int, int>>();
                        antennas[antenna] = set;
                    }
                    set.Add(Tuple.Create(i, j));
                }
            }
        }

        HashSet<Tuple<int, int>> antinodes = new();

        foreach (char antenna in antennas.Keys)
        {
            HashSet<Tuple<int, int>> points = antennas[antenna];

            foreach (var point1 in points) { 
                foreach (var point2 in points) { 
                    if (!point1.Equals(point2)) { 
                        int x_distance = point2.Item1 - point1.Item1; 
                        int y_distance = point2.Item2 - point1.Item2;

                        int antinode_x = point1.Item1 - x_distance;
                        int antinode_y = point1.Item2 - y_distance;
                        if (antinode_x >= 0 && antinode_x < rowMax && antinode_y >= 0 && antinode_y < colMax)
                        {
                            antinodes.Add(Tuple.Create(antinode_x, antinode_y));
                        }
                    } 
                } 
            }
        }

        Assert.AreEqual(antinodes.Count, 351);
    }

    [TestMethod]
    public async Task Part2Async()
    {        
        Dictionary<char, HashSet<Tuple<int, int>>> antennas = new();
        string[] input = await File.ReadAllLinesAsync("input/day8.txt");
        char[][] data = input.Select(x => x.ToCharArray()).ToArray();

        int rowMax = data.Length;
        int colMax = data[0].Length;

        for (int i = 0 ; i < rowMax; i++)
        {
            for (int j = 0; j < colMax; j++)
            {
                if (data[i][j] != '.')
                {
                    char antenna = data[i][j];
                    if (!antennas.TryGetValue(antenna, out HashSet<Tuple<int, int>>? set))
                    {
                        set = new HashSet<Tuple<int, int>>();
                        antennas[antenna] = set;
                    }
                    set.Add(Tuple.Create(i, j));
                }
            }
        }

        HashSet<Tuple<int, int>> antinodes = new();

        foreach (char antenna in antennas.Keys)
        {
            HashSet<Tuple<int, int>> points = antennas[antenna];

            foreach (var point1 in points) { 
                foreach (var point2 in points) { 
                    if (!point1.Equals(point2)) {
                        int x_distance = point2.Item1 - point1.Item1; 
                        int y_distance  = point2.Item2 - point1.Item2;

                        int antinode_x = point1.Item1 - x_distance;
                        int antinode_y = point1.Item2 - y_distance;
                        while (antinode_x >= 0 && antinode_x < rowMax && antinode_y >= 0 && antinode_y < colMax)
                        {
                            antinodes.Add(Tuple.Create(antinode_x, antinode_y));

                            antinode_x = antinode_x - x_distance;
                            antinode_y = antinode_y - y_distance;
                        }
                    } 
                } 
            }
        }

        HashSet<Tuple<int, int>> result = antinodes;

        foreach(var i in antennas)
        {
            if (i.Value.Count > 1)
            {
                result.UnionWith(i.Value);
            }
        }

        Assert.AreEqual(result.Count, 1259);
    }
}