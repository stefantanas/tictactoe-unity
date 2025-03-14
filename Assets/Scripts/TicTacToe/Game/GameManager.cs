using TicTacToe.Game.Board;
using UnityEngine;

namespace TicTacToe.Game
{
    public class GameManager : MonoBehaviour
    {
        private BoardManager _boardManager;
        private Mark _currentPlayer = Mark.X;
        private bool _isGameOver;

        private void Start()
        {
            _boardManager = new BoardManager();
            Field.OnFieldClicked += HandleFieldClick;
        }

        private void OnDestroy()
        {
            Field.OnFieldClicked -= HandleFieldClick;
        }

        private void HandleFieldClick(int x, int y)
        {
            if (_isGameOver || !_boardManager.SetMark(x, y, _currentPlayer))
                return;

            // Find the clicked field and update its sprite
            var field = FindField(x, y);
            field?.SetMark(_currentPlayer);

            if (_boardManager.CheckWinCondition(_currentPlayer))
            {
                _isGameOver = true;
                Debug.Log($"{_currentPlayer} wins!");
                return;
            }
            else if (_boardManager.CheckDrawCondition())
            {
                _isGameOver = true;
                Debug.Log("It's a draw!");
                return;
            }

            SwitchTurn();
        }

        private void SwitchTurn()
        {
            _currentPlayer = _currentPlayer == Mark.X ? Mark.O : Mark.X;
        }

        private Field FindField(int x, int y)
        {
            var fields = FindObjectsOfType<Field>();
            foreach (var field in fields)
                if (field.GetX() == x && field.GetY() == y)
                    return field;
            return null;
        }

        public void ResetGame()
        {
            _boardManager.ResetBoard();
            _isGameOver = false;
            _currentPlayer = Mark.X;

            var fields = FindObjectsOfType<Field>();
            foreach (var field in fields)
                field.ResetField();
        }
    }
}