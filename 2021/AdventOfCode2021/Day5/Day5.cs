using AdventOfCode2021.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace AdventOfCode2021.Day5
{
    public class Main
    {
        class Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        class Line
        {
            public Point p0;
            public Point p1;

            public Line(Point p0, Point p1)
            {
                this.p0 = p0;
                this.p1 = p1;
            }
        }

        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            List<Line> lines = new List<Line>();

            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (string line in data.Split("\r\n"))
            {
                string[] parts = line.Split("->");
                string[] p0 = parts[0].Split(",");
                string[] p1 = parts[1].Split(",");
                int x0 = int.Parse(p0[0]);
                int y0 = int.Parse(p0[1]);
                int x1 = int.Parse(p1[0]);
                int y1 = int.Parse(p1[1]);

                if (x0 > maxX)
                {
                    maxX = x0;
                }
                if (x1 > maxX)
                {
                    maxX = x1;
                }
                if (y0 > maxY)
                {
                    maxY = y0;
                }
                if (y1 > maxY)
                {
                    maxY = y1;
                }

                lines.Add(new Line(new Point(x0, y0), new Point(x1, y1)));
            }
            // For now, only consider horizontal and vertical lines: lines where either x1 = x2 or y1 = y2.
            List<Line> filteredXLines = lines.Where(x => x.p0.X == x.p1.X).ToList();
            List<Line> filteredYLines = lines.Where(x => x.p0.Y == x.p1.Y).ToList();
            int[,] grid = new int[maxX + 1, maxY + 1];

            foreach (Line line in filteredXLines)
            {
                int x = line.p0.X;
                int startY;
                int endY;
                if (line.p0.Y < line.p1.Y)
                {
                    startY = line.p0.Y;
                    endY = line.p1.Y;
                }
                else
                {
                    startY = line.p1.Y;
                    endY = line.p0.Y;
                }
                for (int i = startY; i <= endY; i++)
                {
                    grid[x, i] += 1;
                }
            }

            foreach (Line line in filteredYLines)
            {
                int y = line.p0.Y;
                int startX;
                int endX;
                if (line.p0.X < line.p1.X)
                {
                    startX = line.p0.X;
                    endX = line.p1.X;
                }
                else
                {
                    startX = line.p1.X;
                    endX = line.p0.X;
                }
                for (int i = startX; i <= endX; i++)
                {
                    grid[i, y] += 1;
                }
            }

            int count = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Assert.Equal(0, count);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));
            List<Line> lines = new List<Line>();

            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (string line in data.Split("\r\n"))
            {
                string[] parts = line.Split("->");
                string[] p0 = parts[0].Split(",");
                string[] p1 = parts[1].Split(",");
                int x0 = int.Parse(p0[0]);
                int y0 = int.Parse(p0[1]);
                int x1 = int.Parse(p1[0]);
                int y1 = int.Parse(p1[1]);

                if (x0 > maxX)
                {
                    maxX = x0;
                }
                if (x1 > maxX)
                {
                    maxX = x1;
                }
                if (y0 > maxY)
                {
                    maxY = y0;
                }
                if (y1 > maxY)
                {
                    maxY = y1;
                }

                lines.Add(new Line(new Point(x0, y0), new Point(x1, y1)));
            }
            List<Line> filteredXLines = lines.Where(x => x.p0.X == x.p1.X).ToList();
            List<Line> filteredYLines = lines.Where(x => x.p0.Y == x.p1.Y).ToList();
            List<Line> filteredAngledLines = lines.Where(x => x.p0.X != x.p1.X && x.p0.Y != x.p1.Y).ToList();
            int[,] grid = new int[maxX + 1, maxY + 1];

            foreach (Line line in filteredXLines)
            {
                int x = line.p0.X;
                int startY;
                int endY;
                if (line.p0.Y < line.p1.Y)
                {
                    startY = line.p0.Y;
                    endY = line.p1.Y;
                }
                else
                {
                    startY = line.p1.Y;
                    endY = line.p0.Y;
                }
                for (int i = startY; i <= endY; i++)
                {
                    grid[x, i] += 1;
                }
            }

            foreach (Line line in filteredYLines)
            {
                int y = line.p0.Y;
                int startX;
                int endX;
                if (line.p0.X < line.p1.X)
                {
                    startX = line.p0.X;
                    endX = line.p1.X;
                }
                else
                {
                    startX = line.p1.X;
                    endX = line.p0.X;
                }
                for (int i = startX; i <= endX; i++)
                {
                    grid[i, y] += 1;
                }
            }
            foreach (Line line in filteredAngledLines)
            {
                int x0 = line.p0.X;
                int y0 = line.p0.Y;
                int x1 = line.p1.X;
                int y1 = line.p1.Y;
                int xDiff = x0 < x1 ? 1 : -1;
                int yDiff = y0 < y1 ? 1 : -1;
                for (int i = x0, j = y0; i != x1 && j != y1; i += xDiff, j += yDiff)
                {
                    grid[i, j] += 1;
                }
                grid[x1, y1] += 1;
            }

            int count = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] >= 2)
                    {
                        count++;
                    }
                }
            }

            Assert.Equal(0, count);
        }
    }
}