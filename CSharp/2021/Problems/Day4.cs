namespace AdventOfCode2021.Day4
{
    class BingoBoard
    {
        int[,] board = new int[5,5];
        bool[,] marked = new bool[5, 5];
        Dictionary<int, Tuple<int, int>> valueToIndex = new Dictionary<int, Tuple<int,int>>();
        public int sumUnmarked = 0;

        public BingoBoard()
        {

        }

        public void LoadBoard(List<string> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] strNums = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < strNums.Length; j++)
                {
                    int value = int.Parse(strNums[j]);
                    board[i, j] = value;
                    sumUnmarked += value;
                    valueToIndex.Add(value, Tuple.Create(i, j));
                }
            }
        }

        public bool Mark(int value)
        {
            if (valueToIndex.TryGetValue(value, out Tuple<int, int> index))
            {
                sumUnmarked -= value;
                marked[index.Item1, index.Item2] = true;
                return CheckWin(index.Item1, index.Item2);
            }

            return false;
        }

        public bool CheckWin(int x, int y)
        {
            bool row_win = true;
            for (int x_pos = 0; x_pos < 5; x_pos++)
            {
                row_win &= marked[x_pos, y];
                if (!row_win)
                {
                    break;
                }
            }

            bool col_win = true;
            for (int y_pos = 0; y_pos < 5; y_pos++)
            {
                col_win &= marked[x, y_pos];
                if (!col_win)
                {
                    break;
                }
            }

            return row_win || col_win;
        }
    }

    public class Main
    {
        [Fact]
        public void Part1()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            List<int> pulledNumber = new List<int>();
            List<BingoBoard> boards = new List<BingoBoard>();
            string[] datas = data.Split("\r\n");

            foreach (string num in datas[0].Split(","))
            {
                pulledNumber.Add(int.Parse(num));
            }


            for (int i = 1; i < datas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(datas[i]))
                {
                    continue;
                }
                List<string> bingoData = new List<string>();
                for (int j = i; j < i + 5; j++)
                {
                    bingoData.Add(datas[j]);
                }
                BingoBoard board = new BingoBoard();
                board.LoadBoard(bingoData);
                boards.Add(board);
                i = i + 4;
            }

            foreach (int val in pulledNumber)
            {
                foreach (BingoBoard board in boards)
                {
                    if (board.Mark(val))
                    {
                        throw new Exception("" + (board.sumUnmarked * val)); // LOL
                    }
                }
            }

            Assert.Equal(0, 0);
        }

        [Fact]
        public void Part2()
        {
            string data = IO.ReadFile(Path.Combine(Directory.GetCurrentDirectory(), "input1.txt"));

            List<int> pulledNumber = new List<int>();
            List<BingoBoard> boards = new List<BingoBoard>();
            string[] datas = data.Split("\r\n");

            foreach (string num in datas[0].Split(","))
            {
                pulledNumber.Add(int.Parse(num));
            }


            for (int i = 1; i < datas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(datas[i]))
                {
                    continue;
                }
                List<string> bingoData = new List<string>();
                for (int j = i; j < i + 5; j++)
                {
                    bingoData.Add(datas[j]);
                }
                BingoBoard board = new BingoBoard();
                board.LoadBoard(bingoData);
                boards.Add(board);
                i = i + 4;
            }

            foreach (int val in pulledNumber)
            {
                if (boards.Count == 1)
                {
                    if (boards[0].Mark(val))
                    {
                        Assert.Equal(1, val * boards[0].sumUnmarked);
                    }
                    else
                    {
                        continue;
                    }
                }

                List<BingoBoard> candidateBoards = new List<BingoBoard>(boards);
                foreach (BingoBoard board in candidateBoards)
                {
                    if (board.Mark(val))
                    {
                        boards.Remove(board);
                    }
                }
            }

            Assert.Equal(0, 0);
        }
    }
}