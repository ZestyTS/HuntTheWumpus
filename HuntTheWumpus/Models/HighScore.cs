﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    internal class HighScore
    {
        public DateTime DateTime { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public int CaveNumber { get; set; }
        public string Location { get; } = @"%LOCALAPPDATA%\ZestyTS\HuntTheWumpus\HighScore.txt";
        public int Turns { get; set; }
        public int Gold { get; set; }
        public int Arrows { get; set; }
        public bool WumpusDefeated { get; set; }

        public HighScore()
        {
            Location = Environment.ExpandEnvironmentVariables(Location);
            Directory.CreateDirectory(Location.Replace("\\HighScore.txt", ""));
            if (!File.Exists(Location))
                File.Create(Location);
        }

        public void Save()
        {
            using (var writer = new StreamWriter(Location))
            {
                writer.WriteLine(Name + "," + Score + "," + CaveNumber + "," + DateTime.Now.ToString() + ","
                    + Turns + "," + Gold + "," + Arrows + "," + WumpusDefeated);
            }

            ReorderFileByHighestScore();
        }
        public void ReorderFileByHighestScore()
        {
            var highScores = GetHighScores();

            File.WriteAllText(Location, string.Empty);
            highScores.Sort((x, y) => x.Score.CompareTo(y.Score));

            var tempHighScores = new List<HighScore>();
            var count = 0;
            foreach (var highScore in highScores)
            {
                if (count > 10)
                    break;

                highScore.Save();
                tempHighScores.Add(highScore);
                count++;
            }
            highScores = tempHighScores;
        }
        public List<HighScore> GetHighScores()
        {
            var highscores = new List<HighScore>();
            using (var reader = new StreamReader(Location))
            {
                while (!reader.EndOfStream)
                {
                    var highScoreSplit = reader.ReadLine().Split(',');
                    var highScore = new HighScore
                    {
                        Name = highScoreSplit[0],
                        Score = Convert.ToInt32(highScoreSplit[1]),
                        CaveNumber = Convert.ToInt16(highscores[2]),
                        DateTime = DateTime.Parse(highScoreSplit[3]),
                        Turns = Convert.ToInt16(highScoreSplit[4]),
                        Gold = Convert.ToInt16(highScoreSplit[5]),
                        Arrows = Convert.ToInt16(highScoreSplit[6]),
                        WumpusDefeated = Convert.ToBoolean(highScoreSplit[7])
                    };
                    highscores.Add(highScore);
                }
            }
            return highscores;
        }
    }
}
