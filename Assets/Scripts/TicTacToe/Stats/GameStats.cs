using System;
using TicTacToe.Game;

namespace TicTacToe.Stats
{
    [Serializable]
    public class GameStats
    {
        public int TotalGames;
        public int WinsPlayer1;
        public int WinsPlayer2;
        public int Draws;
        public float TotalPlayTime; // In seconds

        public float AveragePlayTime => TotalGames > 0 ? TotalPlayTime / TotalGames : 0f;

        public void RegisterGame(Mark winner, float playTime)
        {
            TotalGames++;
            TotalPlayTime += playTime;

            if (winner == Mark.X)
                WinsPlayer1++;
            else if (winner == Mark.O)
                WinsPlayer2++;
            else
                Draws++;
        }
    }
}