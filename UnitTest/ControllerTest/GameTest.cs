using HuntTheWumpus.Controller;
namespace UnitTest.Controller
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameSetup()
        {
            try
            {
                var game = new Game();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }
    }
}