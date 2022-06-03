using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    internal class Bat : Hazard
    {
        public override string Name { get; set; } = "Bats";
        public override string Warning { get; set; } = "Bats Nearby.";
        public override string ImagePath { get; set; } = "Media/Images/bath.png";
    }
}
