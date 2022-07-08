using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models.Hazards
{
    public abstract class TriviaHazardBase : HazardBase
    {
        public abstract int TriviaBattleMax { get; }
        public abstract int TriviaBattleMin { get; }
        protected TriviaHazardBase(int theme) : base(theme) { }

    }
}
