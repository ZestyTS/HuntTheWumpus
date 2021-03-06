using HuntTheWumpus.Models;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class GameAudioTest
    {
        [TestMethod]
        public void ConstructorEmptyString()
        {
            Assert.IsNotNull(new GameAudio(""));
        }

        [TestMethod]
        public void ConstructorNull()
        {
            Assert.IsNotNull(new GameAudio(null));
        }
        [TestMethod]
        public void ConstructorRealValues()
        {
            Assert.IsNotNull(new GameAudio("Wumpus"));
        }
        
        [TestMethod]
        public void PlayAudio()
        {
            var gameAudio = new GameAudio("Wumpus");
            try
            {
                gameAudio.PlayAudio(gameAudio.Opening);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
            
        }
        
    }
}