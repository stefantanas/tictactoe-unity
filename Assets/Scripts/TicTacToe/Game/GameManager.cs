using System;
using TicTacToe.Game.Board;
using UnityEngine;

namespace TicTacToe.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private WinLineDrawer winLineDrawer;

        private BoardManager _boardManager;
        private Mark _currentPlayer = Mark.X;
        private bool _isGameOver;
        private int _movesPlayed;

        private void Start()
        {
            _boardManager = new BoardManager();
            Field.OnFieldClicked += HandleFieldClick;
        }

        private void OnDestroy()
        {
            Field.OnFieldClicked -= HandleFieldClick;
        }

        public static event Action<bool, int, Mark> NotifyUiChanges;
        public static event Action<Mark> NotifyGameOver;

        private void HandleFieldClick(int x, int y)
        {
            // Ignore clicks if the game is over or the cell is already occupied.
            if (_isGameOver || !_boardManager.SetMark(x, y, _currentPlayer))
                return;

            // Update the visual representation of the clicked field.
            var field = FindField(x, y);
            field?.SetMark(_currentPlayer);
            _movesPlayed++;

            // Check for a winning line.
            if (_boardManager.CheckWinCondition(_currentPlayer, out var winLine))
            {
                _isGameOver = true;
                NotifyUiChanges?.Invoke(_isGameOver, _movesPlayed, _currentPlayer);
                NotifyGameOver?.Invoke(_currentPlayer);

                winLineDrawer.DrawWinLine(winLine);
                return;
            }

            // Check for a draw (i.e., board is full, with no winner).
            if (_boardManager.CheckDrawCondition())
            {
                _isGameOver = true;
                NotifyUiChanges?.Invoke(_isGameOver, _movesPlayed, _currentPlayer);
                NotifyGameOver?.Invoke(Mark.None);
                return;
            }

            // If no win or draw, switch the active player and notify the UI.
            SwitchTurn();
            NotifyUiChanges?.Invoke(_isGameOver, _movesPlayed, _currentPlayer);
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
            _movesPlayed = 0;
            _currentPlayer = Mark.X;

            NotifyUiChanges?.Invoke(_isGameOver, _movesPlayed, _currentPlayer);

            var fields = FindObjectsOfType<Field>();
            foreach (var field in fields) field.ResetField();

            winLineDrawer.ClearWinLine();
        }
    }
}