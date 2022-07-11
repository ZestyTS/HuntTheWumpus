namespace HuntTheWumpus.Models
{
    public class HighScore
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
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var highScoreSplit = line.Split(',');
                    var name = highScoreSplit[0];
                    var score = Convert.ToInt32(highScoreSplit[1]);
                    var caveNum = Convert.ToInt16(highScoreSplit[2]);
                    var dateTime = DateTime.Parse(highScoreSplit[3]);
                    var turn = Convert.ToInt16(highScoreSplit[4]);
                    var gold = Convert.ToInt16(highScoreSplit[5]);
                    var arrow = Convert.ToInt16(highScoreSplit[6]);
                    var wumpusDefeated = Convert.ToBoolean(highScoreSplit[7]);
                    var highScore = new HighScore
                    {
                        Name = name,
                        Score = score,
                        CaveNumber = caveNum,
                        DateTime = dateTime,
                        Turns = turn,
                        Gold = gold,
                        Arrows = arrow,
                        WumpusDefeated = wumpusDefeated
                    };
                    highscores.Add(highScore);
                }
            }
            return highscores;
        }
    }
}
