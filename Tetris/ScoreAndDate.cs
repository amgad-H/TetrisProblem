using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    //A class housing the score and date of a player
    //It implements IComparable to compare player scores in order to order them
    internal class ScoreAndDate : IComparable<ScoreAndDate>
    {
        private int score;
        private DateTime date;

        public ScoreAndDate(int playerScore, DateTime scoreDate)
        {
            this.score = playerScore;
            this.date = scoreDate;
        }

        public int Score { get { return score; } }

        public string Date { get { return date.ToString("dd/MM/yyyy"); } }

        public override string ToString()
        {
            return $"{score};{date.ToString("dd/MM/yyyy")}";
        }

        public int CompareTo(ScoreAndDate? other)
        {
            return other != null ? other.score.CompareTo(this.score) : 0;
        }
    }
}
