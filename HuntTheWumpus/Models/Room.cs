using System;
using System.Collections.Generic;
using System.Linq;
namespace HuntTheWumpus.Models
{
    public class Room
    {
        public int Name { get; set; }
        public bool Bat { get; set; } = false;
        public bool Pit { get; set; } = false;
        public bool Wumpus { get; set; } = false;

        public Room(int name)
        {
            Name = name;
        }
    }
}
