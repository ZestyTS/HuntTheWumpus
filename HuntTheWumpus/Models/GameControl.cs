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
        public Trivia Trivia { get; set; } = new Trivia();
        public Cave Cave { get; set; } = new Cave(-1);
        public Player Player { get; set; } = new Player("");
        public GameLocation GameLocation { get; set; } = new GameLocation(new List<Room>());
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
        public List<Trivia> SetupTriviaBattle(int max = 3)
        {
            var triviaDict = Trivia.SetupTriviaBattle(max);
            var trivias = new List<Trivia>();

            foreach (var trivia in triviaDict)
            {
                Trivia.UsedTriviaQuestions.Add(trivia.Key);
                trivias.Add(trivia.Value);
            }
            return trivias;
        }
        public Trivia GetTriviaHint()
        {
            var trivias = Trivia.GetTrivias();
            var random = new Random();
            var num = random.Next(trivias.Count);
            var trivia = trivias[num];
            return trivia;
        }

        public string GetSecret()
        {
            var batLocations = new List<int>();
            var pitLocations = new List<int>();
            var playerInfo = new List<string>()
            {
                "You are in room " + Player.Location,
                "You currently have " + Player.Gold + " gold",
                "You currently have " + Player.Arrow + " arrows",
                "You have not died, yet",
                "You are in Cave " + Cave.Number
            };
            var wumpus2Away = false;
            foreach (var room in Cave.GetConnections(Player.Location))
            {
                foreach (var neighbor in Cave.GetNeighbors(room.Name))
                {
                    if (neighbor.Wumpus)
                    {
                        wumpus2Away = true;
                        break;
                    }
                }
                if (wumpus2Away)
                    break;
            }

            var wumpusInfo = new List<string>()
            {
                "The " + Wumpus.Name + " is in room " + Wumpus.Location,
                "The " + Wumpus.Name + " is " + (wumpus2Away == true ? "" : "not ") + " two rooms away",
                "The " + Wumpus.Name + " is currently " + (Wumpus.State != Wumpus.WumpusState.Moving ? "not " : "") + "moving around",
                "The " + Wumpus.Name + " is still alive"
            };
            foreach (var room in Cave.Rooms)
                if (room.Bat)
                    batLocations.Add(room.Name);
                else if (room.Pit)
                    pitLocations.Add(room.Name);

            var ran = new Random();
            var ranNum = ran.Next(0, 4);

            return ranNum switch
            {
                0 => "A " + Bat.Name + " can be found in room " + batLocations[ran.Next(0, 1)],
                1 => "A " + Pitfall.Name + " can be found in room " + pitLocations[ran.Next(0, 1)],
                2 => playerInfo[ran.Next(0, 5)],
                3 => wumpusInfo[ran.Next(0, 3)],
                4 => "null", //silly way to make an easy check on the other side
                _ => "There are no secrets available!",
            };
        }
        public void BatAttack(int caveSize)
        {
            var random = new Random();
            int next;
            for (var i = 0; i < Cave.Rooms.Count; i++)
            {
                next = random.Next(caveSize);
                if (!Cave.Rooms[next].Bat && !Cave.Rooms[next].Pit && next != Player.Location)
                {
                    Cave.Rooms[Player.Location].Bat = false;
                    Cave.Rooms[next].Bat = true;
                    break;
                }
            }
            for (var i = 0; i < Cave.Rooms.Count; i++)
            {
                next = random.Next(caveSize);
                if (Player.Location != next)
                {
                    Player.Location = next;
                    break;
                }
            }
        }
        public void GameEndSave()
        {
            Player.CalculateScore(Wumpus.IsDead);
            var highScore = new HighScore
            {
                Score = Player.Score,
                CaveNumber = Cave.Number,
                Name = Player.Name,
                Arrows = Player.Arrow,
                Gold = Player.Gold,
                Turns = Player.Movement,
                WumpusDefeated = Wumpus.IsDead
            };

            highScore.Save();
            highScore.ReorderFileByHighestScore();
        }
        public void GameSetup()
        {
            Player.Location = 1;
            GameLocation = new GameLocation(Cave.Rooms);
            GameLocation.PlayerLocation = 1;

            Cave.Rooms[GameLocation.BatLocations[0]].Bat = true;
            Cave.Rooms[GameLocation.BatLocations[1]].Bat = true;
            Cave.Rooms[GameLocation.WumpusLocation].Wumpus = true;
            Cave.Rooms[GameLocation.PitfallLocations[0]].Pit = true;
            Cave.Rooms[GameLocation.PitfallLocations[1]].Pit = true;
            Wumpus.Location = GameLocation.WumpusLocation;
        }
    }
}
