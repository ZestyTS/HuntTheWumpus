using HuntTheWumpus.Controller;
namespace UnitTest.ControllerTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void GameSetup()
        {
            //Silly way to test that the project can get to the GUI without errors
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