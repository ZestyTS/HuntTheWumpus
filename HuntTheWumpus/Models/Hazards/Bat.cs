namespace HuntTheWumpus.Models.Hazards
{
    internal class Bat : HazardBase
    {
        public override string Name { get; set; }
        public override string Warning { get; set; }
        public override string ImagePath { get; set; }
        public override string EnterSpeech { get; set; }

        public Bat(int theme) : base(theme)
        {
            //Default theme: Wumpus
            Name = "Bats";
            Warning = "Bats Nearby";
            EnterSpeech = "You walked into a room with bats and got sweeped away!";

            //Baddie Theme
            if (theme == 2)
            {
                Name = "Fake Baddies";
                Warning = "You can hear some annoying voices.";
                EnterSpeech = "You walked into a room with " + Name.ToLower() + "! They screamed like a banshee so you ran away!";
            }

            ImagePath = "Media/Images/" + Name.ToLower() + "png";
        }
    }
}
