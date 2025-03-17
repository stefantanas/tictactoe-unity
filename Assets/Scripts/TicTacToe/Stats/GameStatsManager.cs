using TicTacToe.Game;
using TicTacToe.General;
using UnityEngine;

namespace TicTacToe.Stats
{
    public class GameStatsManager
    {
        private const string StatsKey = "GameStats";
        private static readonly PlayerPrefsManager PlayerPrefsManager = new();

        private static GameStats _cachedStats;

        public static GameStats LoadStats()
        {
            if (_cachedStats == null) _cachedStats = PlayerPrefsManager.Load<GameStats>(StatsKey) ?? new GameStats();

            return _cachedStats;
        }

        public static void SaveStats(GameStats stats)
        {
            _cachedStats = stats;
            PlayerPrefsManager.Save(StatsKey, stats);
            PlayerPrefs.Save();
        }

        public static void RegisterGameResult(Mark winner, float playTime)
        {
            var stats = LoadStats();
            stats.RegisterGame(winner, playTime);
            SaveStats(stats);
        }
    }
}