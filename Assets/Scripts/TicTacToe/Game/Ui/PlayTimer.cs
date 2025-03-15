using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace TicTacToe.Game.Ui
{
    public class PlayTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerTxt;

        private float _sectionCurrentTime;
        private bool _isTimerGoing;
        private string _timePlayingStr;
        private TimeSpan _timePlaying;

        private Coroutine _coUpdateTimer;

        private void Start()
        {
            _isTimerGoing = false;
            timerTxt.text = "Time: 00:00.0";
            StartTimer();
            GameManager.NotifyUiChanges += ProcessUiChanges;
        }

        private void OnDestroy()
        {
            GameManager.NotifyUiChanges -= ProcessUiChanges;
        }

        private void ProcessUiChanges(bool isGameOver, int newMoveCount, Mark currentPlayer)
        {
            if (isGameOver)
            {
                StopTimer();
            }
            else if (newMoveCount == 0)
            {
                RestartTimer();
            }
        }

        public void StartTimer()
        {
            _isTimerGoing = true;
            if (_coUpdateTimer == null) _coUpdateTimer = StartCoroutine(UpdateTimer());
        }

        public void StopTimer()
        {
            _isTimerGoing = false;
            if (_coUpdateTimer != null) StopCoroutine(_coUpdateTimer);
            _coUpdateTimer = null;
        }

        private void RestartTimer()
        {
            _isTimerGoing = false;
            if (_coUpdateTimer != null) StopCoroutine(_coUpdateTimer);

            _sectionCurrentTime = 0f;
            _isTimerGoing = true;
            _coUpdateTimer = StartCoroutine(UpdateTimer());
        }

        private IEnumerator UpdateTimer()
        {
            while (_isTimerGoing)
            {
                _sectionCurrentTime += Time.deltaTime;

                _timePlaying = TimeSpan.FromSeconds(_sectionCurrentTime);
                _timePlayingStr = _timePlaying.ToString("mm':'ss'.'f");
                timerTxt.text = $"Time: {_timePlayingStr}";

                yield return null;
            }
        }
    }
}