using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    internal class Wumpus : Hazard
    {
        public int PlayerMoveCounter { get; set; }
        public int MoveCounter { get; set; }
        public bool IsDead { get; set; } = false;
        public WumpusState State { get; set; } = WumpusState.Sleep;
        public int TurnMoveCounter { get; set; } = 1;
        public bool ActiveWumpus { get; set; } = false;
        public int TriviaBattleMax { get; } = 5;
        public int TriviaBattleMin { get; } = 3;
        public override string Name { get; set; } = "Wumpus";
        public string Song { get; set; } = string.Empty;
        public override string Warning { get; set; } = "I smell a Wumpus!";
        public override string ImagePath { get; set; } = "/Media/Images/wumpus.png";

        public Wumpus()
        {
            Song = "~Me me me me me~\n~I am the Great Mighty Wumpus and I'm going to test your trivia~\n~You might think you're all that with that puny brain of yours~\n~You are in my lair so it's time to see how you fair~\n";
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
            var moveTo = random.Next(connectedRooms.Count-1);

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
                    var shunpo = random.Next(rooms.Count-1);
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

            for(var i = 0; i < TurnMoveCounter; i++)
                NewLocation(connectedRooms);

            TurnMoveCounter = 1;
        }

    }
}
