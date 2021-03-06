using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Tetris
{
    internal class ScoreManager
    {
        private Dictionary<string,ScoreAndDate> scoreMap = new Dictionary<string, ScoreAndDate>();

        public ScoreManager()
        {
            ExtractDataFromTable();
        }

        //Gets path of rang list
        public string GetPath()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\net6.0-windows\data\Scores.csv");
            string sFilePath = Path.GetFullPath(sFile);
            if (!File.Exists(sFilePath))
            {
                var myFile = File.Create(sFilePath);
                myFile.Close();
            }

            return sFilePath;
        }

        //A handy method that gets number of "Anonymous" players
        //Used to create new anon names
        public int GetCountOfAnonPlayers()
        {
            int count = 1;
            foreach(string player in scoreMap.Keys)
            {
                if (player.Contains("Anonymous")){
                    count++;
                }
            }
            return count;
        }

        public Dictionary<string, ScoreAndDate> GetScoreDictionary()
        {
            return scoreMap;
        }

        //Adds fresh player to the score board
        public void AddPlayer(string playerName)
        {
            AddScore(playerName, 0);
        }

        //Adds score to a player
        public void AddScore(string playerName,int score)
        {
            ScoreAndDate scoreAndDate = new ScoreAndDate(score, DateTime.Now);
            if (scoreMap.ContainsKey(playerName))
            {
                ScoreAndDate valueOfKey = scoreMap[playerName];
                if (valueOfKey != null)
                {
                    if (score > valueOfKey.Score)
                    {
                        scoreMap.Remove(playerName);
                        scoreMap.Add(playerName, scoreAndDate);
                    }
                }
            }
            else
            {
                scoreMap.Add(playerName, scoreAndDate);
            }
            scoreMap = (scoreMap.OrderBy(key => key.Value)).ToDictionary(x => x.Key, x => x.Value);
        }

        //Fills scoreMap with latest score board data
        private void ExtractDataFromTable()
        {
            string[] csv = File.ReadAllLines(GetPath());
            foreach (string csvLine in csv)
            {
                string[] lineInfo = csvLine.Split(";");
                ScoreAndDate scoreAndDate = null;
                if (int.TryParse(lineInfo[1], out int n) && DateTime.TryParse(lineInfo[2], out DateTime d))
                {
                    scoreAndDate = new ScoreAndDate(Convert.ToInt32(lineInfo[1]), Convert.ToDateTime(lineInfo[2]));
                }
                scoreMap.Add(lineInfo[0], scoreAndDate);
            }
        }

        //Saves the scores in an Excel
        public void SaveScores()
        {
            String csv = String.Join(
                Environment.NewLine,
                scoreMap.Select(p => $"{p.Key};{p.Value};"));

            System.IO.File.WriteAllText(GetPath(), csv);
        }
    }
}
