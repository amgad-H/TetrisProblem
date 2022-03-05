using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
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
            return other.score.CompareTo(this.score);
        }
    }
}
