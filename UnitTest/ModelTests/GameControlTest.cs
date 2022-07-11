using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class GameControlTest
    {
        //This class will return failures unless real songs are added
        //The .wav files that are part of the projects are blank text
        //files with their extension being renamed from txt to wav
        //so they are not real audio files, thus this class fails
        private readonly GameControl GameControl = new(new Theme(1));
        [TestMethod]
        public void ConstructorRealValues()
        {
            var theme = new Theme(1);

            Assert.IsNotNull(new GameControl(theme));
        }
        
        [TestMethod]
        public void PlayOpeningAudio()
        {
            try
            {
                GameControl.PlayOpeningAudio();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void PlayAmbientAudio0()
        {
            try
            {
                GameControl.PlayAmbientAudio(0);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void PlayAmbientAudio1()
        {
            try
            {
                GameControl.PlayAmbientAudio(1);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException))]
        public void PlayAmbientAudio2()
        {
            GameControl.PlayAmbientAudio(2);
        }
        [TestMethod]
        public void PlayEndingAudioPlayerDead()
        {
            try
            {
                GameControl.PlayEndingAudio(true);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void PlayEndingAudioPlayerAlive()
        {
            try
            {
                GameControl.PlayEndingAudio();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void GetLeaderboard()
        {
            Assert.IsNotNull(GameControl.GetLeaderboard());
        }
        [TestMethod]
        public void DisplayGameMenu()
        {
            Assert.IsNotNull(GameControl.DisplayGameMenu());
        }
        [TestMethod]
        public void SetupTriviaBattle()
        {
            Assert.IsNotNull(GameControl.SetupTriviaBattle());
        }
        [TestMethod]
        public void GetTriviaHint()
        {
            Assert.IsInstanceOfType(GameControl.GetTriviaHint(), typeof(Trivia));
        }
        [TestMethod]
        public void GetSecret()
        {
            GameControl.Cave = new Cave(1);
            GameControl.GameSetup();
            Assert.IsNotNull(GameControl.GetSecret());
        }
        [TestMethod]
        public void BatAttack()
        {
            GameControl.Cave = new Cave(1);
            GameControl.GameSetup();
            var bats = GameControl.GameLocation.BatLocations;
            GameControl.BatAttack(30);

            var newLocs = GameControl.GameLocation.BatLocations;

            var sameSpot = 0;
            foreach (var bat in bats)
                foreach (var newLoc in newLocs)
                    if (bat == newLoc)
                        sameSpot++;

            Assert.IsTrue(sameSpot < 4);
        }
        [TestMethod]
        public void GameEndSaveWithoutPlayerName()
        {
            try
            {
                GameControl.GameEndSave();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
        [TestMethod]
        public void GameEndSaveRealData()
        {
            GameControl.Player.Name = "UnitTest";
            GameControl.GameEndSave();
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void GameSetup()
        {
            GameControl.Cave = new Cave(1);
            GameControl.Player.Location = 10;
            GameControl.GameSetup();
            Assert.IsTrue(GameControl.Player.Location == 1);
        }
    }
}