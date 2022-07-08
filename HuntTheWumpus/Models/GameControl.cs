using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public class GameControl
    {
        public string Winner { get; set; }
        public string Loser { get; set; }
        public GameAudio GameAudio { get; set; }
        public HighScore HighScore { get; set; } = new HighScore();
        public int ThemeChoice { get; set; }
        public GameControl(int theme)
        {
            ThemeChoice = theme;
            GameAudio = new GameAudio(ThemeChoice);
            Winner = "\nCongrats! You have defeated the Wumpus!\n";
            Loser = "\nGood job! The Wumpus has defeated you!\n";

            //Baddie Theme
            if (theme == 2)
            {
                Winner = "\nCongrats! Your Baddie has decided to take you back!\n";
                Loser = "\nGood job! Your Baddie has left you saying they're out of your league!\n";
            }

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
            var gameName = "Hunt The Wumpus";
            if (ThemeChoice == 2)
                gameName = "Find The Baddie";

            var lines = new List<string>() {
                "*********************************************************************************\n",
                "                 Welcome To '" + gameName + "' By ZestyTS\n",
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
