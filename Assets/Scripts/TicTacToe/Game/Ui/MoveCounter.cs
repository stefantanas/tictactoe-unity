using TMPro;
using UnityEngine;

namespace TicTacToe.Game.Ui
{
    public class MoveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moveCountText;
        private int _currentMoveCount;

        private void Start()
        {
            moveCountText.text = "Moves: 0";
            GameManager.NotifyUiChanges += ProcessUiChanges;
        }

        private void OnDestroy()
        {
            GameManager.NotifyUiChanges -= ProcessUiChanges;
        }

        private void ProcessUiChanges(bool isGameOver, int newMoveCount, Mark currentPlayer)
        {
            _currentMoveCount = newMoveCount;
            moveCountText.text = $"Moves: {_currentMoveCount.ToString()}";
        }
    }
}