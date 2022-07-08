namespace HuntTheWumpus.Models.Hazards
{
    public class Wumpus : TriviaHazardBase
    {
        public override int TriviaBattleMax { get; } = 5;
        public override int TriviaBattleMin { get; } = 3;
        public override string Name { get; set; }
        public override string EnterSpeech { get; set; }
        public override string Warning { get; set; }
        public override string ImagePath { get; set; }
        public string SneakSpeech { get; set; }
        public int PlayerMoveCounter { get; set; }
        public int MoveCounter { get; set; }
        public bool IsDead { get; set; } = false;
        public WumpusState State { get; set; } = WumpusState.Sleep;
        public int TurnMoveCounter { get; set; } = 1;
        public bool ActiveWumpus { get; set; } = false;
        public Wumpus(string name, string warning, string enterSpeech) : base(name, warning, enterSpeech)
        {
            Name = name;
            EnterSpeech = enterSpeech;
            Warning = warning;
            ImagePath = "/Media/Images/" + Name.ToLower() + ".png";
            SneakSpeech = "The " + Name + " snuck up on you!";
        }

        public enum WumpusState
        {
            Sleep = 0,
            Awake = 1,
            Moving = 2,
        }
        public void Move(int moveUpTo)
        {
            State = WumpusState.Moving;
            var random = new Random();
            MoveCounter += random.Next(moveUpTo);
        }
        public void NewLocation(List<int> connectedRooms)
        {
            if (State == WumpusState.Sleep)
                return;

            if (MoveCounter == 0)
            {
                MoveCounter--;
                return;
            }
            else if (MoveCounter < 0)
            {
                MoveCounter = 0;
                State = WumpusState.Sleep;
                return;
            }

            var random = new Random();
            var moveTo = random.Next(connectedRooms.Count - 1);

            MoveCounter--;
            if (MoveCounter == 0)
                State = WumpusState.Awake;

            Location = connectedRooms[moveTo];
        }

        public void RoundMove(List<int> connectedRooms, List<int> rooms)
        {
            if (ActiveWumpus)
            {
                var random = new Random();
                var chanceToShunpo = random.Next(0, 100);
                if (chanceToShunpo < 6)
                {
                    var shunpo = random.Next(rooms.Count - 1);
                    Location = shunpo;
                }
                PlayerMoveCounter++;
            }

            if (PlayerMoveCounter > 4)
            {
                var random = new Random();
                var moveAt = random.Next(PlayerMoveCounter, 10);
                if (moveAt <= PlayerMoveCounter)
                {
                    PlayerMoveCounter = 0;
                    var moveCounts = random.Next(3);
                    TurnMoveCounter += moveCounts;
                }
            }

            for (var i = 0; i < TurnMoveCounter; i++)
                NewLocation(connectedRooms);

            TurnMoveCounter = 1;
        }
    }
}
