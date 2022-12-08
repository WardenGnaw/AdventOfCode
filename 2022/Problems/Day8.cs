
namespace Problems
{
    [TestClass]
    public class Day8
    {
        private bool IsVisible(int[,] grid, int x, int y, int height)
        {
            int row = grid.GetLength(0);
            int col = grid.GetLength(1);

            // Edges are always visable
            if (x == 0 || y == 0 || (x == row) || y == (col - 1))
            {
                return true;
            }

            // Top
            bool isVisibleTop = true;
            for (int i = 0; i < x; i++)
            {
                if (grid[i, y] >= height)
                {
                    isVisibleTop = false;
                    break;
                }
            }

            // Right
            bool isVisibleRight = true;
            for (int i = y + 1; i < col; i++)
            {
                if (grid[x, i] >= height)
                {
                    isVisibleRight = false;
                    break;
                }
            }

            // Left
            bool isVisibleLeft = true;
            for (int i = 0; i < y; i++)
            {
                if (grid[x, i] >= height)
                {
                    isVisibleLeft = false;
                    break;
                }
            }

            // Bottom
            bool isVisibleBottom = true;
            for (int i = x + 1; i < row; i++)
            {
                if (grid[i, y] >= height)
                {
                    isVisibleBottom = false;
                    break;
                }
            }

            return isVisibleTop || isVisibleBottom || isVisibleLeft || isVisibleRight;
        }

        private int ScenicScore(int[,] grid, int x, int y, int height)
        {
            int row = grid.GetLength(0);
            int col = grid.GetLength(1);

            // Edges are always visable
            if (x == 0 || y == 0 || (x == row) || y == (col - 1))
            {
                return 0;
            }

            // Top
            int topSeen = 0;
            for (int i = x - 1; i >= 0; i--)
            {
                if (grid[i, y] >= height)
                {
                    topSeen += 1;
                    break;
                }
                topSeen += 1;
            }

            // Right
            int rightSeen = 0;
            for (int i = y + 1; i < col; i++)
            {
                if (grid[x, i] >= height)
                {
                    rightSeen += 1;
                    break;
                }
                rightSeen += 1;
            }

            // Left
            int leftSeen = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                if (grid[x, i] >= height)
                {
                    leftSeen += 1;
                    break;
                }
                leftSeen += 1;
            }

            // Bottom
            int bottomSeen = 0;
            for (int i = x + 1; i < row; i++)
            {
                if (grid[i, y] >= height)
                {
                    bottomSeen += 1;
                    break;
                }
                bottomSeen += 1;
            }

            return topSeen * rightSeen * leftSeen * bottomSeen;
        }

        [TestMethod]
        public void Part1()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day8.txt")).Trim().Split("\r\n");
            int row = 0;
            int col = data.Length;
            int[,]? grid = null;
            for (int i = 0; i < data.Length; i++)
            {
                char[] line = data[i].ToCharArray();
                row = line.Length;
                if (grid == null)
                {
                    grid = new int[row, col];
                }
                for (int j = 0; j < line.Length; j++)
                {
                    grid[i, j] = int.Parse(line[j].ToString());
                }
            }

            int count = 0;

            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    if (IsVisible(grid, x, y, grid[x, y]))
                    {
                        count += 1;
                    }
                }
            }
            
            Assert.Fail("" + count);
        }

        [TestMethod]
        public void Part2()
        {
            string[] data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input\\day8.txt")).Trim().Split("\r\n");
            int row = 0;
            int col = data.Length;
            int[,]? grid = null;
            for (int i = 0; i < data.Length; i++)
            {
                char[] line = data[i].ToCharArray();
                row = line.Length;
                if (grid == null)
                {
                    grid = new int[row, col];
                }
                for (int j = 0; j < line.Length; j++)
                {
                    grid[i, j] = int.Parse(line[j].ToString());
                }
            }

            List<int> scenicScores = new List<int>();
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    int score = ScenicScore(grid, x, y, grid[x, y]);
                    scenicScores.Add(score);
                }
            }

            Assert.Fail("" + scenicScores.OrderByDescending(x => x).Take(1).Sum());
        }
    }
}