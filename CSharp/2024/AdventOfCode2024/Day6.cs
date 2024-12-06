using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AdventOfCode2024;

[TestClass]
public class Day6
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [TestMethod]
    public async Task Part1Async()
    {
        string input = await File.ReadAllTextAsync("input/day6.txt");
        char[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
        HashSet<Tuple<int, int>> seen = new();
        HashSet<Tuple<int, int>> obstructions = new();
        int step = 0;
        Tuple<int, int> position = Tuple.Create(-1, -1);

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                if (data[i][j] == '^')
                {
                    position = Tuple.Create(i, j);
                }
                else if (data[i][j] == '#')
                {
                obstructions.Add(Tuple.Create(i, j));
                }
            }
        }

        Direction direction = Direction.Up;
        while (true)
        {
            Tuple<int, int> next = Tuple.Create(-1, -1);
            seen.Add(position);
            switch (direction)
            {
                case Direction.Up:
                    next = Tuple.Create(position.Item1 - 1, position.Item2);
                    break;
                case Direction.Down:
                    next = Tuple.Create(position.Item1 + 1, position.Item2);
                    break;
                case Direction.Left:
                    next = Tuple.Create(position.Item1, position.Item2 - 1);
                    break;
                case Direction.Right:
                    next = Tuple.Create(position.Item1, position.Item2 + 1);
                    break;
            }
            if (next.Item1 >= data.Length || next.Item2 >= data[0].Length || next.Item1 < 0 || next.Item2 < 0)
            {
                break;
            }
            else if (obstructions.Contains(next))
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Right;
                        next = Tuple.Create(position.Item1, next.Item2 + 1);
                        break;
                    case Direction.Down:
                        direction = Direction.Left;
                        next = Tuple.Create(position.Item1, next.Item2 - 1);
                        break;
                    case Direction.Left:
                        direction = Direction.Up;
                        next = Tuple.Create(next.Item1 - 1, position.Item2);
                        break;
                    case Direction.Right:
                        direction = Direction.Down;
                        next = Tuple.Create(next.Item1 + 1, position.Item2);
                        break;
                }
            }
            step++;
            position = next;
        }    

        Assert.AreEqual(seen.Count, 4789);
    }

    [TestMethod]
    public async Task Part2Async()
    {
        string input = await File.ReadAllTextAsync("input/day6.txt");
        char[][] data = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray()).ToArray();
        HashSet<Tuple<int, int>> seen = new();
        HashSet<Tuple<int, int>> obstructions = new();
        int step = 0;
        Tuple<int, int> start = Tuple.Create(-1, -1);

        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                if (data[i][j] == '^')
                {
                    start = Tuple.Create(i, j);
                }
                else if (data[i][j] == '#')
                {
                    obstructions.Add(Tuple.Create(i, j));
                }
            }
        }

        Tuple<int, int> position = start;
        Direction direction = Direction.Up;
        while (true)
        {
            Tuple<int, int> next = Tuple.Create(-1, -1);
            seen.Add(position);
            switch (direction)
            {
                case Direction.Up:
                    next = Tuple.Create(position.Item1 - 1, position.Item2);
                    break;
                case Direction.Down:
                    next = Tuple.Create(position.Item1 + 1, position.Item2);
                    break;
                case Direction.Left:
                    next = Tuple.Create(position.Item1, position.Item2 - 1);
                    break;
                case Direction.Right:
                    next = Tuple.Create(position.Item1, position.Item2 + 1);
                    break;
            }
            if (next.Item1 >= data.Length || next.Item2 >= data[0].Length || next.Item1 < 0 || next.Item2 < 0)
            {
                break;
            }
            else if (obstructions.Contains(next))
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Right;
                        next = Tuple.Create(position.Item1, next.Item2 + 1);
                        break;
                    case Direction.Down:
                        direction = Direction.Left;
                        next = Tuple.Create(position.Item1, next.Item2 - 1);
                        break;
                    case Direction.Left:
                        direction = Direction.Up;
                        next = Tuple.Create(next.Item1 - 1, position.Item2);
                        break;
                    case Direction.Right:
                        direction = Direction.Down;
                        next = Tuple.Create(next.Item1 + 1, position.Item2);
                        break;
                }
            }
            step++;
            position = next;
        }

        int newPosCount = 0;
        seen.Remove(start);

        foreach (Tuple<int, int> newBlockade in seen)
        {
            Dictionary<Tuple<int, int>, int> visitedCount = new();
            bool isLoop = false;
            position = start;
            direction = Direction.Up;
            while (true)
            {
                Tuple<int, int> next = Tuple.Create(-1, -1);
                if (visitedCount.TryGetValue(position, out int value))
                {
                    visitedCount[position] = value + 1;
                }
                else
                {
                    visitedCount[position] = 1;
                }
                if (value >= 20)
                {
                    isLoop = true;
                    break;
                }
                switch (direction)
                {
                    case Direction.Up:
                        next = Tuple.Create(position.Item1 - 1, position.Item2);
                        break;
                    case Direction.Down:
                        next = Tuple.Create(position.Item1 + 1, position.Item2);
                        break;
                    case Direction.Left:
                        next = Tuple.Create(position.Item1, position.Item2 - 1);
                        break;
                    case Direction.Right:
                        next = Tuple.Create(position.Item1, position.Item2 + 1);
                        break;
                }
                if (next.Item1 >= data.Length || next.Item2 >= data[0].Length || next.Item1 < 0 || next.Item2 < 0)
                {
                    break;
                }
                
                while (obstructions.Contains(next) || next.Equals(newBlockade))
                {
                    switch (direction)
                    {
                        case Direction.Up:
                            direction = Direction.Right;
                            next = Tuple.Create(position.Item1, next.Item2 + 1);
                            break;
                        case Direction.Down:
                            direction = Direction.Left;
                            next = Tuple.Create(position.Item1, next.Item2 - 1);
                            break;
                        case Direction.Left:
                            direction = Direction.Up;
                            next = Tuple.Create(next.Item1 - 1, position.Item2);
                            break;
                        case Direction.Right:
                            direction = Direction.Down;
                            next = Tuple.Create(next.Item1 + 1, position.Item2);
                            break;
                    }
                }
                position = next;
            }

            if (isLoop)
            {
                newPosCount++;
            }
        }

        Assert.AreEqual(newPosCount, 4789);
    }
}