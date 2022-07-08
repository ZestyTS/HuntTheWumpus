namespace HuntTheWumpus.Models.Hazards
{
    public abstract class HazardBase
    {
        public int Location { get; set; }
        public abstract string Warning { get; set; }
        public abstract string ImagePath { get; set; }
        public abstract string Name { get; set; }
        public abstract string EnterSpeech { get; set; }
        public enum HazardEnum
        {
            Nothing = 0,
            Wumpus = 1,
            Bat = 2,
            Pitfall = 3
        }
        public HazardBase(int theme) { }
    }
}
