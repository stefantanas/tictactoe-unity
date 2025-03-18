namespace TicTacToe.Game
{
    /// <summary>
    ///     Manages the underlying 3x3 grid state for the game.
    ///     Stores which Mark (X, O, or None) occupies each cell and provides
    ///     methods to set marks, reset the board, and check win/draw conditions.
    /// </summary>
    public class BoardManager
    {
        private readonly Mark[,] _board = new Mark[3, 3];

        public BoardManager()
        {
            ResetBoard();
        }

        /// <summary>
        ///     Attempts to place the specified mark at the given (x, y) position.
        ///     Returns false if that cell is already occupied.
        /// </summary>
        public bool SetMark(int x, int y, Mark mark)
        {
            if (_board[x, y] != Mark.None) return false;
            _board[x, y] = mark;
            return true;
        }

        public Mark GetMark(int x, int y)
        {
            return _board[x, y];
        }

        /// <summary>
        ///     Clears the board, setting all cells to Mark.None.
        /// </summary>
        public void ResetBoard()
        {
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                _board[i, j] = Mark.None;
        }

        /// <summary>
        ///     Checks if the given mark has a winning row, column, or diagonal.
        ///     If a winning line is found, returns true and outputs the 3 cell positions in winLine.
        /// </summary>
        public bool CheckWinCondition(Mark mark, out (int, int)[] winLine)
        {
            // Check rows and columns
            for (var i = 0; i < 3; i++)
            {
                if (_board[i, 0] == mark && _board[i, 1] == mark && _board[i, 2] == mark)
                {
                    winLine = new[] { (i, 0), (i, 1), (i, 2) };
                    return true;
                }

                if (_board[0, i] == mark && _board[1, i] == mark && _board[2, i] == mark)
                {
                    winLine = new[] { (0, i), (1, i), (2, i) };
                    return true;
                }
            }

            // Check diagonals
            if (_board[0, 0] == mark && _board[1, 1] == mark && _board[2, 2] == mark)
            {
                winLine = new[] { (0, 0), (1, 1), (2, 2) };
                return true;
            }

            if (_board[0, 2] == mark && _board[1, 1] == mark && _board[2, 0] == mark)
            {
                winLine = new[] { (0, 2), (1, 1), (2, 0) };
                return true;
            }

            winLine = null;
            return false;
        }

        /// <summary>
        ///     Returns true if the board is full (i.e., all cells are occupied) and no winner has been found.
        /// </summary>
        public bool CheckDrawCondition()
        {
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                if (_board[i, j] == Mark.None)
                    return false;

            return true;
        }
    }
}