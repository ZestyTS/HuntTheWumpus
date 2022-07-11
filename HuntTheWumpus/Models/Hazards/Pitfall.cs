namespace HuntTheWumpus.Models.Hazards
{
    public class Pitfall : TriviaHazardBase
    {
        public override string Name { get; set; }
        public override string Warning { get; set; }
        public override string ImagePath { get; set; }
        public override string EnterSpeech { get; set; }
        public override int TriviaBattleMax { get; } = 3;
        public override int TriviaBattleMin { get; } = 2;

        public Pitfall(string name, string warning, string enterSpeech) : base(name, warning, enterSpeech)
        {
            Name = name;
            Warning = warning;
            EnterSpeech = enterSpeech;
            ImagePath = "Media/Images/" + Name.ToLower() + "/.png";
        }
    }
}
