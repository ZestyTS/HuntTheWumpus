using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        //TODO: Figure out why it says you can get 100 gold in the game
        public int Gold { get; set; }
        public int Arrow { get; set; }
        public int Location { get; set; }
        public int Movement { get; set; }
        public bool IsDead { get; set; } = false;
        public Player(string name)
        {
            Location = 1;
            Name = name;
            Score = 0;
            Arrow = 3;
            Gold = 0;
            Movement = 0;
        }
        public void Move(int roomNumber)
        {
            Movement++;
            Location = roomNumber;
            Gold++;
        }
        public void Shoot()
        {
            Arrow--;
        }
        public void CalculateScore(bool wumpusDead)
        {
            Score = 100 - Movement + Gold - (5 * Arrow) + (wumpusDead ? 50 : 0);
        }
    }
}
