using HuntTheWumpus.Helper;
using HuntTheWumpus.Models;
using System.Media;
using static HuntTheWumpus.Models.Hazard;

namespace HuntTheWumpus.Controller
{
    public static class Game
    {
        public static Player Player { get; set; }
        public static Cave Cave { get; set; }
        public static List<int> TriviaQuestions { get; set; } = new List<int>();
        public static int CaveSize { get; set; }
        public static int StartingRoom { get; set; } = 1;
        private static int ThemeChoice { get; set; } = 1;
        private static GameAudio GameAudio { get; set; }
        private static SoundPlayer SoundPlayer { get; set; }
        public static void GameSetup()
        {
            GameAudio = new GameAudio(ThemeChoice);
            PlayAudio(GameAudio.Opening);

            GameMenu();
        }
        public static void GameMenu()
        {
            DisplayToConsoleAsTypeWriter("*********************************************************************************\n");
            DisplayToConsoleAsTypeWriter("                 Welcome To 'Hunt The " + (ThemeChoice == 1 ? "Wumpus" : "Baddie") + "' By Mr Fernandez\n");
            Console.WriteLine();
            DisplayToConsoleAsTypeWriter("                                   Game Menu\n");
            DisplayToConsoleAsTypeWriter("*********************************************************************************\n");
            Console.WriteLine();
            DisplayToConsoleAsTypeWriter("            [1]  Display Rules\n");
            DisplayToConsoleAsTypeWriter("            [2]  Start Game\n");
            DisplayToConsoleAsTypeWriter("            [3]  Select Theme\n");
            DisplayToConsoleAsTypeWriter("            [4]  View HighScores\n");
            DisplayToConsoleAsTypeWriter("            [5]  Quit\n");
            Console.WriteLine();
            DisplayToConsoleAsTypeWriter("Selection: ");

            var selection = UserInput.GetInteger(1, 5);
            switch (selection)
            {
                case 1:
                    DisplayRules();
                    break;
                case 2:
                    Start();
                    break;
                case 3:
                    ThemeSelector();
                    Console.Clear();
                    GameSetup();
                    break;
                case 4:
                    var highScores = new HighScore().GetHighScores();
                    var i = 1;
                    foreach (var highScore in highScores) {
                        Console.WriteLine("Position: " + i);
                        Console.WriteLine("Name: " + highScore.Name);
                        Console.WriteLine("Cave: " + highScore.CaveNumber);
                        Console.WriteLine("Score: " + highScore.Score);
                        Console.WriteLine("Arrows: " + highScore.Arrows);
                        Console.WriteLine("Gold: " + highScore.Gold);
                        Console.WriteLine("Turns: " + highScore.Turns);
                        Console.WriteLine("Wumpus Defeated: " + highScore.WumpusDefeated);
                        Console.WriteLine("DateTime: " + highScore.DateTime.ToString());
                        Console.WriteLine();
                        i++;
                    }
                    Console.WriteLine("Press a button to continue,");
                    Console.ReadKey();
                    Console.Clear();
                    GameSetup();
                    break;
                case 5:
                    Exit();
                    break;
            }
        }

        private static void DisplayRules()
        {
            throw new NotImplementedException();
        }

        private static void StartRequirements()
        {
            Console.WriteLine();
            Console.Write("What is your name?: ");
            Player = new Player(UserInput.GetString());

            Console.Write("Which Cave would like to start in? (1 - 5): ");
            Cave = new Cave(UserInput.GetInteger(1, 5));
            Console.WriteLine();
        }
        private static void Start()
        {
            var bat = new Bat();
            var pitfall = new Pitfall();
            var wumpus = new Wumpus();
            PlayAudio(GameAudio.Ambients[0]);

            if (ThemeChoice == 2)
            {
                wumpus.Name = "Baddie";
                wumpus.Warning = "I can sense my Baddie is nearby!";
                wumpus.Song = "~Why why why why why~\n~Must you always chase me down when you know that when I'm ready I'll go to you~\n~It's clear you don't know anything so how about a little game~\nIf you win I'll let you go, if you lose it'll be your end, but it's time to see how you fair~\n";

                bat.Name = "Fake Baddies";
                bat.Warning = "You can hear some annoying voices.";

                pitfall.Name = "Spilled Juice";
                pitfall.Warning = "You can smell something.";
            }

            TriviaQuestions = new List<int>();
            StartRequirements();

            CaveSize = Cave.GetRoomNumbers(Cave.Rooms).Count;
            Player.Location = 1;

            var gameLocation = new GameLocation(Cave.Rooms)
            {
                PlayerLocation = 1
            };

            Cave.Rooms[gameLocation.BatLocations[0]].Bat = true;
            Cave.Rooms[gameLocation.BatLocations[1]].Bat = true;
            Cave.Rooms[gameLocation.WumpusLocation].Wumpus = true;
            Cave.Rooms[gameLocation.PitfallLocations[0]].Pit = true;
            Cave.Rooms[gameLocation.PitfallLocations[1]].Pit = true;
            wumpus.Location = gameLocation.WumpusLocation;

            Console.Clear();
            while (!Player.IsDead)
            {
                if (SoundPlayer.SoundLocation != GameAudio.Ambients[0])
                    PlayAudio(GameAudio.Ambients[0]);

                var neighbors = Cave.GetNeighbors(Player.Location);
                var connected = Cave.GetConnections(Player.Location);
                var connectedNums = Cave.GetRoomNumbers(connected);

                wumpus.RoundMove(Cave.GetRoomNumbers(Cave.GetConnections(gameLocation.WumpusLocation)), Cave.GetRoomNumbers(Cave.Rooms));
                gameLocation.WumpusLocation = wumpus.Location;

                var warning = gameLocation.BuildWarningString(neighbors, bat, pitfall, wumpus);
                if (!string.IsNullOrEmpty(warning))
                    Console.WriteLine(warning + "\n");

                Console.WriteLine("You are in Room " + Player.Location);

                Console.WriteLine("Adjacent Rooms: " + gameLocation.BuildNearByRooms(neighbors));
                Console.WriteLine("Connected Rooms: " + gameLocation.BuildNearByRooms(connected));

                if (gameLocation.WumpusLocation == gameLocation.PlayerLocation)
                {
                    
                    Console.WriteLine("The " + wumpus.Name + " snuck up on you!");
                    if (!SurviveWumpusAttack(wumpus))
                    {
                        Save(Player, Cave.Number, wumpus.IsDead);
                        Loser(wumpus.Name);
                        GameEnd(Player);
                    }
                }

                var input = UserInput.UserAction(Player.Arrow);
                var target = -1;
                switch (input)
                {
                    case "S":
                        Console.Write("Which Room?: ");
                        target = UserInput.GetTarget(connectedNums);
                        Player.Shoot();

                        if (gameLocation.DidArrowHitWumpus(target))
                        {
                            Save(Player, Cave.Number, wumpus.IsDead);
                            WinRar(wumpus.Name);
                            GameEnd(Player);
                        }
                        else
                        {
                            if (wumpus.State == Wumpus.WumpusState.Sleep)
                                wumpus.Move(2);
                        }
                        break;
                    case "M":
                        Console.Write("Which Room?: ");
                        target = UserInput.GetTarget(connectedNums);
                        gameLocation.PlayerLocation = Player.Location;

                        Player.Move(target);
                        Console.WriteLine();
                        TriviaHint();

                        var currentRoom = Cave.Rooms[gameLocation.PlayerLocation-1];
                        var hazard = gameLocation.CheckIfRoomHasHazard(currentRoom);

                        HazardAction(hazard, wumpus, pitfall, bat);
                        gameLocation.PlayerLocation = Player.Location;
                        break;
                    case "A":
                        if (WonTrivia(2, 3, false))
                            Player.Arrow += 2;
                        break;
                    case "P":
                        if (WonTrivia(2, 3, false))
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
                            foreach(var room in connected)
                            {
                                foreach(var neighbor in Cave.GetNeighbors(room.Name))
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
                                "The " + wumpus.Name + " is in room " + gameLocation.WumpusLocation,
                                "The " + wumpus.Name + " is " + (wumpus2Away == true ? "" : "not ") + " two rooms away",
                                "The " + wumpus.Name + " is currently " + (wumpus.State != Wumpus.WumpusState.Moving ? "not " : "") + "moving around",
                                "The " + wumpus.Name + " is still alive"
                            };
                            foreach (var room in Cave.Rooms)
                                if (room.Bat)
                                    batLocations.Add(room.Name);
                                else if (room.Pit)
                                    pitLocations.Add(room.Name);

                            var ran = new Random();
                            var ranNum = ran.Next(0,4);
                            switch (ranNum)
                            {
                                case 0:
                                    Console.WriteLine("A " + bat.Name + " can be found in room " + batLocations[ran.Next(0, 1)]);
                                    break;
                                case 1:
                                    Console.WriteLine("A " + pitfall.Name + " can be found in room " + pitLocations[ran.Next(0,1)]);
                                    break;
                                case 2:
                                    Console.WriteLine(playerInfo[ran.Next(0, 5)]);
                                    break;
                                case 3:
                                    Console.WriteLine(wumpusInfo[ran.Next(0, 3)]);
                                    break;
                                case 4:
                                    TriviaHint();
                                    break;
                            }
                        }
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void ThemeSelector()
        {
            Console.WriteLine();
            DisplayToConsoleAsTypeWriter("            [1]  Wumpus\n");
            DisplayToConsoleAsTypeWriter("            [2]  Baddie\n");
            Console.WriteLine();
            DisplayToConsoleAsTypeWriter("Selection: ");

            var selection = UserInput.GetInteger(1, 2);
            ThemeChoice = selection;
            Console.WriteLine("");
        }

        private static void Exit()
        {
            Console.WriteLine("Buhbye!");
            Environment.Exit(0);
        }

        private static void DisplayToConsoleAsTypeWriter(string output)
        {
            foreach (var character in output)
            {
                Console.Write(character);
                Thread.Sleep(15);
            }
        }
        private static void WinRar(string name)
        {
            PlayAudio(GameAudio.Win);
            DisplayToConsoleAsTypeWriter("\n");
            DisplayToConsoleAsTypeWriter("Congrats! You have defeated the " + name + "!\n");

        }

        private static void Loser(string name)
        {
            PlayAudio(GameAudio.Lose);
            Player.IsDead = true;
            DisplayToConsoleAsTypeWriter("\n");
            DisplayToConsoleAsTypeWriter("Good job! The " + name + " has defeated you!\n");
        }

        private static void Save(Player player, int caveNumber, bool wumpusDead)
        {
            player.CalculateScore(wumpusDead);
            var highScore = new HighScore
            {
                Score = player.Score,
                CaveNumber = caveNumber,
                Name = player.Name,
                Arrows = player.Arrow,
                Gold = player.Gold,
                Turns = player.Movement,
                WumpusDefeated = wumpusDead
            };

            highScore.Save();
        }

        private static void GameEnd(Player player)
        {
            Player.IsDead = true;
            DisplayToConsoleAsTypeWriter("Score: " + player.Score + "\n");
            DisplayToConsoleAsTypeWriter("Name: " + player.Name + "\n\n");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            GameSetup();

        }

        private static void WumpusSinging(Wumpus wumpus)
        {
            Console.WriteLine();
            Console.Write(wumpus.Name + ": ");
            DisplayToConsoleAsTypeWriter(wumpus.Song);
            Console.WriteLine();
        }

        private static bool SurviveWumpusAttack(Wumpus wumpus)
        {
            PlayAudio(GameAudio.Ambients[1]);

            WumpusSinging(wumpus);
            return WonTrivia(wumpus.TriviaBattleMin, wumpus.TriviaBattleMax);
        }

        private static bool SurvivePitfall(Pitfall pitfall)
        {
            PlayAudio(GameAudio.Ambients[1]);

            Console.WriteLine();

            if (pitfall.Name == "pitfall")
                Console.WriteLine("You walked into a room with a " + pitfall.Name + "!");
            else
                Console.WriteLine("You slipped on " + pitfall.Name + "!");

            var winTrivia = WonTrivia(pitfall.TriviaBattleMin, pitfall.TriviaBattleMax);
            if (winTrivia)
                Player.Location = StartingRoom;

            return winTrivia; 
        }
        private static void BatAttack(Bat bat) 
        {
            PlayAudio(GameAudio.Ambients[1]);

            Console.WriteLine();

            if (bat.Name == "Bats")
                Console.WriteLine("You walked into a room with bats and got sweeped away!");
            else
                Console.WriteLine("You walked into a room with a " + bat.Name + "! They screamed like a banshee so you ran away!");

            var random = new Random();
            int next;
            for (var i = 0; i < Cave.Rooms.Count; i++)
            {  
                next = random.Next(CaveSize);
                if (!Cave.Rooms[next].Bat && !Cave.Rooms[next].Pit && next != Player.Location)
                {
                    Cave.Rooms[Player.Location].Bat = false;
                    Cave.Rooms[next].Bat = true;
                    break;
                }
            }
            for (var i = 0; i < Cave.Rooms.Count; i++)
            {
                next = random.Next(CaveSize);
                if (Player.Location != next)
                {
                    Player.Location = next;
                    break;
                }
            }
        }

        private static bool WonTrivia(int min, int max, bool forced = true)
        {
            var triviaDict = new Trivia().SetupTriviaBattle(max, TriviaQuestions);
            var trivias = new List<Trivia>();

            foreach(var trivia in triviaDict)
            {
                TriviaQuestions.Add(trivia.Key);
                trivias.Add(trivia.Value);
            }


            return TriviaBattle(min, trivias, forced);
        }

        private static void HazardAction(HazardEnum hazardEnum, Wumpus wumpus, Pitfall pitfall, Bat bat)
        {
            bool isDead = false;

            if (hazardEnum == HazardEnum.Nothing)
                return;

            else if (hazardEnum == HazardEnum.Wumpus)
                isDead = !SurviveWumpusAttack(wumpus);

            else if (hazardEnum == HazardEnum.Pitfall)
                isDead = !SurvivePitfall(pitfall);

            else if (hazardEnum == HazardEnum.Bat)
                BatAttack(bat);

            if (isDead)
            {
                Save(Player, Cave.Number, wumpus.IsDead);
                Loser(wumpus.Name);
                GameEnd(Player);
            }
        }

        private static bool TriviaBattle(int correct, List<Trivia> trivias, bool forced = true)
        {
            Console.WriteLine();
            var amountRight = 0;
            foreach(var trivia in trivias)
            {
                if (correct <= amountRight)
                    return true;

                if (forced && Player.Gold == 0)
                    return false;

                DisplayToConsoleAsTypeWriter(trivia.Question + "\n");

                foreach(var answerKey in trivia.AnswerKey)
                    Console.WriteLine(answerKey);

                var input = UserInput.GetInteger(1, 4);
                if (input == trivia.Answer)
                    amountRight++;

                Console.WriteLine();
                if (forced)
                    Player.Gold--;
            }
            return false;
        }

        private static void TriviaHint()
        {
            Console.WriteLine("You've stepped on a piece of paper, it reads:");
            var trivias = new Trivia().GetTrivias();
            var random = new Random();
            var num = random.Next(trivias.Count);
            var trivia = trivias[num];
            Console.WriteLine(trivia.Question);
            Console.WriteLine(trivia.AnswerKey[trivia.Answer-1]);
        }

        private static void PlayAudio(string soundLocation)
        {
            if (SoundPlayer == null)
                SoundPlayer = new SoundPlayer(soundLocation);
            else
            {
                SoundPlayer.Stop();
                SoundPlayer.SoundLocation = soundLocation;
            }

            SoundPlayer.PlayLooping();

        }

        private static void ImageDisplayer()
        {
            /*
            Screen screen = Screen.PrimaryScreen;
            using (Form form = new Form())
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.Bounds = screen.Bounds;
                PictureBox pb = new PictureBox();
                pb.Dock = DockStyle.Fill;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                form.Controls.Add(pb);
                form.Show();
                string[] imageFiles = Directory.GetFiles("images", "*.jpg");
                while (true)
                {
                    foreach (string imageFile in imageFiles)
                    {
                        pb.Load(imageFile);
                        form.Refresh();
                        Thread.Sleep(2000);
                    }
                }
            }
             */
        }
    }
}
