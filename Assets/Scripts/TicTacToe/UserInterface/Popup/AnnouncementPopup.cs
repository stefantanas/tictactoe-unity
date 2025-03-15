using TicTacToe.Game;
using TicTacToe.Game.Ui;
using TMPro;
using UnityEngine;

namespace TicTacToe.UserInterface.Popup
{
    public class AnnouncementPopup : BasePopup
    {
        private int _currentMoveCount;
        private PlayTimer _playTimer;
        
        [SerializeField] private TextMeshProUGUI announcementText;
        [SerializeField] private TextMeshProUGUI gameTimeText;
        

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
                    announcementText.text = "Player X Wins!";
                    break;
                case Mark.O:
                    announcementText.text = "Player O Wins!";
                    break;
                case Mark.None:
                default:
                    announcementText.text = "It's a draw!";
                    break;
            }
            
            var timePlayingStr = _playTimer.GetTimePlaying().ToString("mm':'ss'.'f");
            gameTimeText.text = $"Time: {timePlayingStr}";

            OpenPopup();
        }
    }
}