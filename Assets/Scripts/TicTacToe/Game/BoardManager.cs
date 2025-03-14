namespace TicTacToe.Game
{
    public class BoardManager
    {
        private readonly Mark[,] _board = new Mark[3, 3];

        public BoardManager()
        {
            ResetBoard();
        }

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

        public void ResetBoard()
        {
            for (var i = 0; i < 3; i++)
            for (var j = 0; j < 3; j++)
                _board[i, j] = Mark.None;
        }

        public bool CheckWinCondition(Mark mark)
        {
            // Check rows and columns
            for (var i = 0; i < 3; i++)
            {
                if (_board[i, 0] == mark && _board[i, 1] == mark && _board[i, 2] == mark)
                    return true;
                if (_board[0, i] == mark && _board[1, i] == mark && _board[2, i] == mark)
                    return true;
            }

            // Check diagonals
            return (_board[0, 0] == mark && _board[1, 1] == mark && _board[2, 2] == mark) ||
                   (_board[0, 2] == mark && _board[1, 1] == mark && _board[2, 0] == mark);
        }

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