namespace HuntTheWumpus.Models.Hazards
{
    internal class Pitfall : TriviaHazardBase
    {
        public override string Name { get; set; }
        public override string Warning { get; set; }
        public override string ImagePath { get; set; }
        public override string EnterSpeech { get; set; }
        public override int TriviaBattleMax { get; } = 3;
        public override int TriviaBattleMin { get; } = 2;

        public Pitfall(int theme) : base(theme)
        {
            Name = "Pitfall";
            Warning = "I feel a draft";
            EnterSpeech = "You walked into a room with a pit!";

            if (theme == 1)
            {
                Name = "Spilled Juice";
                Warning = "You can smell something";
                EnterSpeech = "You slipped on " + Name.ToLower() + "!";
            }

            ImagePath = "Media/Images/" + Name.ToLower() + "/.png";
        }
    }
}
