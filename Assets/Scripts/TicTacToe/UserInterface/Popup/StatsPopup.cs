using TicTacToe.General;
using TicTacToe.Stats;
using TMPro;
using UnityEngine;

namespace TicTacToe.UserInterface.Popup
{
    public class StatsPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI totalGamesText;
        [SerializeField] private TextMeshProUGUI winsPlayer1Text;
        [SerializeField] private TextMeshProUGUI winsPlayer2Text;
        [SerializeField] private TextMeshProUGUI drawsText;
        [SerializeField] private TextMeshProUGUI avgGameTimeText;

        protected override void Start()
        {
            base.Start();
            LoadStats();
        }

        private void LoadStats()
        {
            var stats = GameStatsManager.LoadStats();
            totalGamesText.text = $"{stats.TotalGames}";
            winsPlayer1Text.text = $"{stats.WinsPlayer1}";
            winsPlayer2Text.text = $"{stats.WinsPlayer2}";
            drawsText.text = $"{stats.Draws}";
            avgGameTimeText.text = $"{stats.AveragePlayTime:F2} sec";
        }
    }
}