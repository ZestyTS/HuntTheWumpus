using HuntTheWumpus.Models.Hazards;

namespace HuntTheWumpus.Models
{
    public class Theme
    {
        public int Selector { get; set; }
        public string WumpusName { get; set; } = string.Empty;
        public string WumpusWarning { get; set; } = string.Empty;
        public string WumpusEnter { get; set; } = string.Empty;
        public string BatName { get; set; } = string.Empty;
        public string BatWarning { get; set; } = string.Empty;
        public string BatEnter { get; set; } = string.Empty;
        public string PitName { get; set; } = string.Empty;
        public string PitWarning { get; set; } = string.Empty;
        public string PitEnter { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Winner { get; set; } = string.Empty;
        public string Loser { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
        //It's possible to set up a Theme without any of the values being set
        //This is like that on purpose so it's possible to extend the object
        //Even if it is very unconventional
        public Theme(int theme)
        {
            Selector = theme;
            if (Selector == 1)
                SetupWumpusTheme();
            else if(Selector == 2)
                SetupBaddieTheme();
        }
        private void SetupWumpusTheme()
        {
            //Theme name
            Name = "Wumpus";

            //Wumpus
            WumpusName = "Wumpus";
            WumpusWarning = "I smell a Wumpus!";
            WumpusEnter = "~Me me me me me~\n~I am the Great Mighty Wumpus and I'm going to test your trivia~\n~You might think you're all that with that puny brain of yours~\n~You are in my lair so it's time to see how you fair~\n";

            //Bat
            BatName = "Bats";
            BatWarning = "Bats Nearby";
            BatEnter = "You walked into a room with bats and got sweeped away!";

            //Pit
            PitName = "Pitfall";
            PitWarning = "I feel a draft";
            PitEnter = "You walked into a room with a pit!";

            //GameOver
            Winner = "\nCongrats! You have defeated the Wumpus!\n";
            Loser = "\nGood job! The Wumpus has defeated you!\n";

            //GameName
            GameName = "Hunt The Wumpus";
        }
        private void SetupBaddieTheme()
        {
            //Theme Name
            Name = "Baddie";

            //Wumpus
            WumpusName = "Baddie";
            WumpusWarning = "I can sense my Baddie is nearby!";
            WumpusEnter = "~Why why why why why~\n~Must you always chase me down when you know that when I'm ready I'll go to you~\n~It's clear you don't know anything so how about a little game~\nIf you win I'll let you go, if you lose it'll be your end, but it's time to see how you fair~\n";

            //Bat
            BatName = "Fake Baddies";
            BatWarning = "You can hear some annoying voices.";
            BatEnter = "You walked into a room with " + BatName.ToLower() + "! They screamed like a banshee so you ran away!";

            //Pitfall
            PitName = "Spilled Juice";
            PitWarning = "You can smell something";
            PitEnter = "You slipped on " + Name.ToLower() + "!";

            //GameOver
            Winner = "\nCongrats! Your Baddie has decided to take you back!\n";
            Loser = "\nGood job! Your Baddie has left you saying they're out of your league!\n";

            //GameName
            GameName = "Find The Baddie";
        }
        public Wumpus GetWumpusObject()
        {
            return new Wumpus(WumpusName, WumpusWarning, WumpusEnter);
        }
        public Bat GetBatObject()
        {
            return new Bat(BatName, BatWarning, BatEnter);
        }
        public Pitfall GetPitfallObject()
        {
            return new Pitfall(PitName, PitWarning, PitEnter);
        }
    }
}
