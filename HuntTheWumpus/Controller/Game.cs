using HuntTheWumpus.Helper;
using HuntTheWumpus.Models;
using HuntTheWumpus.Models.Hazards;
using NAudio.Wave;
using static HuntTheWumpus.Models.Hazards.HazardBase;

namespace HuntTheWumpus.Controller
{
    public class Game
    {
        private int ThemeChoice { get; set; } = 1;
        private GameControl GameControl { get; set; } = new GameControl(new Theme(1));
        private WaveOutEvent? OutputDevice { get; set; } = null;
        public void GameSetup()
        {
            GameControl = new GameControl(new Theme(ThemeChoice));
            if (OutputDevice != null)
            {
                OutputDevice.Stop();
                OutputDevice.Dispose();
                OutputDevice = null;
            }
            GameControl.PlayOpeningAudio();
            OutputDevice = GameControl.GameAudio.OutputDevice;
            GameMenu();
        }
        private void GameMenu()
        {
            foreach(var line in GameControl.DisplayGameMenu())
                DisplayToConsoleAsTypeWriter(line);

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
                    foreach (var leaderBoard in GameControl.GetLeaderboard())
                        Console.WriteLine(leaderBoard);

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

        private void DisplayRules()
        {
            throw new NotImplementedException();
        }

        private void StartRequirements()
        {
            Console.WriteLine();
            Console.Write("What is your name?: ");
            GameControl.Player = new Player(UserInput.GetString());

            Console.Write("Which Cave would like to start in? (1 - 5): ");
            GameControl.Cave = new Cave(UserInput.GetInteger(1, 5));
            Console.WriteLine();
        }
        private void Start()
        {
            GameControl.PlayAmbientAudio(0);
            StartRequirements();


            Console.Clear();
            while (!GameControl.Player.IsDead)
            {
                GameControl.PlayAmbientAudio(0);

                var neighbors = GameControl.Cave.GetNeighbors(GameControl.Player.Location);
                var connected = GameControl.Cave.GetConnections(GameControl.Player.Location);
                var connectedNums = GameControl.Cave.GetRoomNumbers(connected);

                GameControl.Wumpus.RoundMove(GameControl.Cave.GetRoomNumbers(GameControl.Cave.GetConnections(GameControl.GameLocation.WumpusLocation)), GameControl.Cave.GetRoomNumbers(GameControl.Cave.Rooms));
                GameControl.GameLocation.WumpusLocation = GameControl.Wumpus.Location;

                var warning = GameControl.GameLocation.BuildWarningString(neighbors, GameControl.Bat, GameControl.Pitfall, GameControl.Wumpus);
                if (!string.IsNullOrEmpty(warning))
                    Console.WriteLine(warning + "\n");

                Console.WriteLine("You are in Room " + GameControl.Player.Location);

                Console.WriteLine("Adjacent Rooms: " + GameControl.GameLocation.BuildNearByRooms(neighbors));
                Console.WriteLine("Connected Rooms: " + GameControl.GameLocation.BuildNearByRooms(connected));

                if (GameControl.GameLocation.WumpusLocation == GameControl.GameLocation.PlayerLocation)
                {
                    Console.WriteLine("The " + GameControl.Wumpus.Name + " snuck up on you!");
                    if (!SurviveWumpusAttack())
                        GameOver();
                }

                var input = UserInput.UserAction(GameControl.Player.Arrow);
                int target;
                switch (input)
                {
                    case "S":
                        Console.Write("Which Room?: ");
                        target = UserInput.GetTarget(connectedNums);
                        GameControl.Player.Shoot();

                        if (GameControl.GameLocation.DidArrowHitWumpus(target))
                        {
                            GameControl.Wumpus.IsDead = true;
                            GameOver();
                        }
                        else
                        {
                            if (GameControl.Wumpus.State == Wumpus.WumpusState.Sleep)
                                GameControl.Wumpus.Move(2);
                        }
                        break;
                    case "M":
                        Console.Write("Which Room?: ");
                        target = UserInput.GetTarget(connectedNums);
                        GameControl.GameLocation.PlayerLocation = GameControl.Player.Location;

                        GameControl.Player.Move(target);
                        Console.WriteLine();
                        TriviaHint();

                        var currentRoom = GameControl.Cave.Rooms[GameControl.GameLocation.PlayerLocation-1];
                        var hazard = GameControl.GameLocation.CheckIfRoomHasHazard(currentRoom);

                        HazardAction(hazard);
                        GameControl.GameLocation.PlayerLocation = GameControl.Player.Location;
                        break;
                    case "A":
                        if (DoTriviaBattle(GameControl.SetupTriviaBattle()))
                            GameControl.Player.Arrow += 2;
                        break;
                    case "P":
                        if (DoTriviaBattle(GameControl.SetupTriviaBattle()))
                        {
                            var secret = GameControl.GetSecret();
                            if (secret == "null")
                                TriviaHint();
                        }
                        break;
                }
                Console.WriteLine();
            }
        }

        private void ThemeSelector()
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

        private void Exit()
        {
            Console.WriteLine("Buhbye!");
            Environment.Exit(0);
        }

        private void DisplayToConsoleAsTypeWriter(string output)
        {
            foreach (var character in output)
            {
                Console.Write(character);
                Thread.Sleep(15);
            }
        }
        private void WinRar()
        {
            EndingText();
        }

        private void Loser()
        {
            GameControl.Player.IsDead = true;
            EndingText();
        }

        private void EndingText()
        {
            GameControl.PlayEndingAudio(GameControl.Player.IsDead);
            DisplayToConsoleAsTypeWriter(GameControl.Player.IsDead ? GameControl.Theme.Winner : GameControl.Theme.Loser);
        }

        private void GameEnd()
        {
            GameControl.Player.IsDead = true;
            DisplayToConsoleAsTypeWriter("Score: " + GameControl.Player.Score + "\n");
            DisplayToConsoleAsTypeWriter("Name: " + GameControl.Player.Name + "\n\n");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            GameSetup();
        }

        private void GameOver()
        {
            GameControl.GameEndSave();
            if (GameControl.Wumpus.IsDead)
                WinRar();
            else
                Loser();
            GameEnd();
        }

        private void WumpusSinging()
        {
            Console.WriteLine();
            Console.Write(GameControl.Wumpus.Name + ": ");
            DisplayToConsoleAsTypeWriter(GameControl.Wumpus.EnterSpeech);
            Console.WriteLine();
        }

        private bool SurviveWumpusAttack()
        {
            GameControl.PlayAmbientAudio(1);
            WumpusSinging();

            var triviaQuestions = GameControl.SetupTriviaBattle(GameControl.Wumpus.TriviaBattleMax);
            return DoTriviaBattle(triviaQuestions, true, GameControl.Wumpus.TriviaBattleMin);
        }

        private bool SurvivePitfall()
        {
            GameControl.PlayAmbientAudio(1);

            Console.WriteLine();
            Console.WriteLine(GameControl.Pitfall.EnterSpeech);

            var triviaQuestions = GameControl.SetupTriviaBattle(GameControl.Pitfall.TriviaBattleMax);
            return DoTriviaBattle(triviaQuestions, true); 
        }
        private void BatAttack() 
        {
            GameControl.PlayAmbientAudio(1);

            Console.WriteLine();
            Console.WriteLine(GameControl.Bat.EnterSpeech);

            GameControl.BatAttack(GameControl.Cave.Size);
        }

        private void HazardAction(HazardEnum hazardEnum)
        {
            bool isDead = false;

            if (hazardEnum == HazardEnum.Nothing)
                return;

            else if (hazardEnum == HazardEnum.Wumpus)
                isDead = !SurviveWumpusAttack();

            else if (hazardEnum == HazardEnum.Pitfall)
                isDead = !SurvivePitfall();

            else if (hazardEnum == HazardEnum.Bat)
                BatAttack();

            if (isDead)
            {
                GameControl.Player.IsDead = true;
                GameOver();
            }
        }

        private bool DoTriviaBattle(List<Trivia> trivias, bool forced = false, int min = 2)
        {
            Console.WriteLine();
            var amountRight = 0;
            for(var i = 0; i < trivias.Count; i++)
            {
                if (min <= amountRight)
                    return true;

                if (forced && GameControl.Player.Gold == 0)
                    return false;

                if ((i > min) && (trivias.Count - i > amountRight))
                    return false;

                DisplayToConsoleAsTypeWriter(trivias[i].Question + "\n");

                foreach(var answerKey in trivias[i].AnswerKey)
                    Console.WriteLine(answerKey);

                var input = UserInput.GetInteger(1, 4);
                if (input == trivias[i].Answer)
                    amountRight++;

                Console.WriteLine();
                if (forced)
                    GameControl.Player.Gold--;
            }
            return false;
        }

        private void TriviaHint()
        {
            Console.WriteLine("You've stepped on a piece of paper, it reads:");

            var trivia = GameControl.GetTriviaHint();

            Console.WriteLine(trivia.Question);
            Console.WriteLine(trivia.AnswerKey[trivia.Answer-1]);
        }
    }
}
