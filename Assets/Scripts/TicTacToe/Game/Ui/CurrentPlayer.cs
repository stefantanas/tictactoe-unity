using TMPro;
using UnityEngine;

namespace TicTacToe.Game.Ui
{
    public class CurrentPlayer : MonoBehaviour
    {
        private string _currentPlayerName;
        [SerializeField] private TextMeshProUGUI currentPlayerText;
        
        private void Start()
        {
            currentPlayerText.text = "To Play: Player X";
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
                _currentPlayerName = currentPlayer == Mark.X ? "Player X" : "Player O";
                currentPlayerText.text = $"To Play: {_currentPlayerName}";
            }
        }
    }
}