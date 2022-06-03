using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    internal class Pitfall : Hazard
    {
        public override string Name { get; set; } = "Pitfall";
        public override string Warning { get; set; } = "I feel a draft.";
        public override string ImagePath { get; set; } = "Media/Images/pitfall.png";
        public int TriviaBattleMax { get; } = 3;
        public int TriviaBattleMin { get; } = 2;
    }
}
