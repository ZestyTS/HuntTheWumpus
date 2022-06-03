using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public abstract class Hazard
    {
        public int Location { get; set; }
        public abstract string Warning { get; set; }
        public abstract string ImagePath { get; set; }
        public abstract string Name { get; set; }
        public enum HazardEnum
        {
            Nothing = 0,
            Wumpus = 1,
            Bat = 2,
            Pitfall = 3
        }

    }
}
