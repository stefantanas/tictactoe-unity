using TMPro;
using UnityEngine;

namespace TicTacToe.Game.Ui
{
    public class CurrentPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPlayerText;
        private string _currentPlayerName;

        private void Start()
        {
            currentPlayerText.text = "To Play: Player 1";
            GameManager.NotifyUiChanges += ProcessUiChanges;
        }

        private void OnDestroy()
        {
            GameManager.NotifyUiChanges -= ProcessUiChanges;
        }

        private void ProcessUiChanges(bool isGameOver, int newMoveCount, Mark currentPlayer)
        {
            if (currentPlayer != Mark.None)
            {
                _currentPlayerName = currentPlayer == Mark.X ? "Player 1" : "Player 2";
                currentPlayerText.text = $"To Play: {_currentPlayerName}";
            }
        }
    }
}