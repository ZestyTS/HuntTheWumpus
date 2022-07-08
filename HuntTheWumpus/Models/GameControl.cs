using HuntTheWumpus.Models.Hazards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public class GameControl
    {
        public GameAudio GameAudio { get; set; }
        public HighScore HighScore { get; set; } = new HighScore();
        public Bat Bat { get; set; }
        public Wumpus Wumpus { get; set; }
        public Pitfall Pitfall { get; set; }
        public Theme Theme { get; set; }
        public GameControl(Theme theme)
        {
            Theme = theme;
            Wumpus = theme.GetWumpusObject();
            Bat = theme.GetBatObject();
            Pitfall = theme.GetPitfallObject();
            GameAudio = new GameAudio(theme.Name);
        }
        public void PlayOpeningAudio()
        {
            GameAudio.PlayAudio(GameAudio.Opening);
        }
        public void PlayAmbientAudio(int index)
        {
            GameAudio.PlayAudio(GameAudio.Ambients[index]);
        }
        public void PlayEndingAudio(bool playerDead = false)
        {
            GameAudio.PlayAudio(playerDead ? GameAudio.Lose : GameAudio.Win);
        }

        public List<string> GetLeaderboard()
        {
            var highScores = new List<string>();
            var i = 1;
            foreach(var highScore in HighScore.GetHighScores())
            {
                highScores.Add("Position: " + i + "\n");
                highScores.Add("Name: " + highScore.Name + "\n");
                highScores.Add("Cave: " + highScore.CaveNumber + "\n");
                highScores.Add("Score: " + highScore.Score + "\n");
                highScores.Add("Arrows: " + highScore.Arrows + "\n");
                highScores.Add("Gold: " + highScore.Gold + "\n");
                highScores.Add("Turns: " + highScore.Turns + "\n");
                highScores.Add("Wumpus Defeated: " + highScore.WumpusDefeated + "\n");
                highScores.Add("DateTime: " + highScore.DateTime.ToString() + "\n");
                highScores.Add("\n");
                i++;
            }
            return highScores;
        }

        public List<string> DisplayGameMenu()
        {
            var lines = new List<string>() {
                "*********************************************************************************\n",
                "                 Welcome To '" + Theme.GameName + "' By ZestyTS\n",
                "\n",
                "                                   Game Menu\n",
                "*********************************************************************************\n",
                "\n",
                "            [1]  Display Rules\n",
                "            [2]  Start Game\n",
                "            [3]  Select Theme\n",
                "            [4]  View HighScores\n",
                "            [5]  Quit\n",
                "\n",
                "Selection: "
            };

            return lines;
        }

    }
}
