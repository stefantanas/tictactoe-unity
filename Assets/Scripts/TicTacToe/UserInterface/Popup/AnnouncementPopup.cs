using TicTacToe.Game;
using TicTacToe.Game.Ui;
using TicTacToe.General;
using TicTacToe.Stats;
using TMPro;
using UnityEngine;

namespace TicTacToe.UserInterface.Popup
{
    public class AnnouncementPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI announcementText;
        [SerializeField] private TextMeshProUGUI gameTimeText;
        private int _currentMoveCount;
        private PlayTimer _playTimer;


        protected override void Start()
        {
            announcementText.text = "";
            _playTimer = FindObjectOfType<PlayTimer>();
            GameManager.NotifyGameOver += ProcessGameOverAnnouncement;
            base.Start();
        }

        private void OnDestroy()
        {
            GameManager.NotifyGameOver -= ProcessGameOverAnnouncement;
        }

        private void ProcessGameOverAnnouncement(Mark winningPlayer)
        {
            switch (winningPlayer)
            {
                case Mark.X:
                    announcementText.text = "Player 1 Wins!";
                    break;
                case Mark.O:
                    announcementText.text = "Player 2 Wins!";
                    break;
                case Mark.None:
                default:
                    announcementText.text = "It's a draw!";
                    break;
            }

            var playTime = _playTimer.GetTimePlaying().TotalSeconds;
            GameStatsManager.RegisterGameResult(winningPlayer, (float)playTime);

            var timePlayingStr = _playTimer.GetTimePlaying().ToString("mm':'ss'.'f");
            gameTimeText.text = $"Time: {timePlayingStr}";

            Invoke(nameof(OpenPopup), 2);
        }

        public void BackToMainMenu()
        {
            CustomSceneManager.Instance.SwitchToPlayScene();
        }
    }
}